using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DoctorTrainer.DTO;
using DoctorTrainer.DTO.User;
using DoctorTrainer.Entity;
using DoctorTrainer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DoctorTrainer.Controllers;

[ApiController]
public class LoginController : ControllerBase
{
    private readonly PasswordHasher<string> _passwordHasher;

    private readonly UserService _userService;

    public LoginController(UserService userService)
    {
        _passwordHasher = new PasswordHasher<string>();
        _userService = userService;

        // todo: temporary adding users (remove after DB will be created)
        _userService.AddUser(
            new User()
            {
                Id = 1, PasswordHash = _passwordHasher.HashPassword(null, "admin"), Username = "admin",
                Role = UserRole.Admin.ToString()
            });
        _userService.AddUser(
            new User()
            {
                Id = 2, PasswordHash = _passwordHasher.HashPassword(null, "admin"), Username = "expert",
                Role = UserRole.Expert.ToString()
            });
        _userService.AddUser(new User()
        {
            Id = 3, PasswordHash = _passwordHasher.HashPassword(null, "user"), Username = "user",
            Role = UserRole.User.ToString()
        });
    }

    [HttpPost]
    [Route("/api/login")]
    [AllowAnonymous]
    public JsonResult Login(LoginRequest request)
    {
        var user = Authenticate(request);
        if (user == null)
        {
            return new JsonResult(NotFound());
        }

        var token = Generate(user);
        return new JsonResult(token);
    }

    // todo: add logout

    private string Generate(User user)
    {
        var securityKey = new SymmetricSecurityKey(
            // todo: store the key in a seure way
            Encoding.UTF8.GetBytes("DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4")
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            "https://localhost:7146/",
            "https://localhost:7146/",
            claims,
            expires: DateTime.Now.AddMinutes(30), // todo: make token validity time configurable
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private User? Authenticate(LoginRequest request)
    {
        User? current = _userService.FindByUsername(request.Username);
        if (current == null) return null;
        var hashPassword = _passwordHasher.HashPassword(null, request.Password);
        PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(null, current.PasswordHash, request.Password);
        
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return null;
        }

        //    Users.FirstOrDefault(
        //    u => u.Username == request.Username && u.Password == request.Password
        //);
        return current;
    }
}