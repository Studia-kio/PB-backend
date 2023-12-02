using System.Security.Claims;
using DoctorTrainer.DTO;
using DoctorTrainer.DTO.User;
using DoctorTrainer.Entity;
using DoctorTrainer.Service;
using DoctorTrainer.TokenGenerators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrainer.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly PasswordHasher<string> _passwordHasher;

    private readonly IConfiguration _configuration;
    private readonly UserService _userService;
    private readonly RefreshTokenService _refreshTokenService;

    private AccessTokenGenerator _accessTokenGenerator;
    private RefreshTokenGenerator _refreshTokenGenerator;
    private RefreshTokenValidator _refreshTokenValidator;

    public UserController(UserService userService, RefreshTokenService refreshTokenService,
        IConfiguration configuration)
    {
        _passwordHasher = new PasswordHasher<string>();
        _userService = userService;
        _refreshTokenService = refreshTokenService;
        _configuration = configuration;
        _accessTokenGenerator = new AccessTokenGenerator(_configuration);
        _refreshTokenGenerator = new RefreshTokenGenerator(_configuration);
        _refreshTokenValidator = new RefreshTokenValidator(_configuration);
    }

    [HttpPost]
    [Route("/api/login")]
    [AllowAnonymous]
    public IActionResult Login(LoginRequest request)
    {
        User? currentUser = _userService.FindByUsername(request.Username);
        if (currentUser == null)
        {
            return NotFound("User not found.");
        }

        PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(
            null, currentUser.PasswordHash, request.Password
        );

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return BadRequest("Incorrect user password.");
        }

        return GenerateTokensAndRespond(currentUser);
    }

    [HttpPost]
    [Route("/api/register")]
    [AllowAnonymous]
    public IActionResult Register(RegisterRequest request)
    {
        if (!request.Password.Equals(request.PasswordRetype))
        {
            return BadRequest();
        }

        User? potentialConflict = _userService.FindByUsername(request.Username);
        if (potentialConflict != null)
        {
            return Conflict(new
            {
                Message = $"User with username {request.Username} already exists."
            });
        }

        int userId = _userService.AddUser(new User()
        {
            Username = request.Username,
            PasswordHash = _passwordHasher.HashPassword(null, request.Password),
            Role = UserRole.User.ToString()
        });

        return Created("", new
        {
            UserId = userId,
            Message = "User registered successfully."
        });
    }

    [HttpGet]
    [Route("/api/users")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAllUsers()
    {
        var users = new UsersGetResponse()
        {
            Users = _userService.FindAll().Select(u => new UserGetResponse()
            {
                Id = u.Id,
                Username = u.Username,
                Role = u.Role
            }).ToList()
        };
        return Ok(users);
    }

    [HttpPut]
    [Route("/api/users/{id}/role")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateUserRole(int id, UserRoleRequest request)
    {
        User? user = _userService.FindById(id);
        if (user == null)
        {
            return NotFound(new
            {
                Message = $"User with id {id} not found."
            });
        }

        if (user.Role.Equals(request.Role))
        {
            return Conflict(new { Message = $"User with id {id} already has {request.Role} role." });
        }

        user.Role = request.Role;
        _userService.Update(user);

        return Accepted("", new { Message = $"User role successfully changed to {request.Role}." });
    }

    [HttpPost]
    [Route("/api/users/token-refresh")]
    [Authorize]
    public IActionResult RefreshToken(RefreshRequest request)
    {
        bool refreshTokenValid = _refreshTokenValidator.Validate(request.RefreshToken);
        if (!refreshTokenValid)
        {
            return BadRequest("Invalid refresh token.");
        }

        UserToken? token = _refreshTokenService.FindByToken(request.RefreshToken);
        if (token == null)
        {
            return NotFound("Invalid refresh token.");
        }

        _refreshTokenService.Delete(token);

        User? user = _userService.FindById(token.UserId);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        return GenerateTokensAndRespond(user);
    }

    private IActionResult GenerateTokensAndRespond(User user)
    {
        string accessToken = _accessTokenGenerator.Generate(user);
        string refreshToken = _refreshTokenGenerator.Generate();
        UserToken newToken = new UserToken()
        {
            UserId = user.Id,
            Token = refreshToken
        };
        _refreshTokenService.Save(newToken);

        return Created("", new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    [Authorize]
    [HttpPost("/api/users/logout")]
    public IActionResult Logout()
    {
        string username = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        User? user = _userService.FindByUsername(username);
        
        if (user == null)
        {
            return NotFound("User not found.");
        }

        _refreshTokenService.DeleteAllByUser(user.Id);
        return Accepted("", new { Message = "User successfully logged out." });
    }

    [HttpDelete]
    [Route("/api/users/{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteUser(int id)
    {
        User? user = _userService.FindById(id);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (user.Role == "Admin")
        {
            return BadRequest("Cannot delete another admin.");
        }
        
        _userService.Delete(user);
        return Accepted("User successfully deleted.");
    }
    
}