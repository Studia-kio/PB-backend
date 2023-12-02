using DoctorTrainer.DTO;
using DoctorTrainer.Entity;
using DoctorTrainer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrainer;

[ApiController]
public class DataInitializerController : ControllerBase
{
    private readonly PasswordHasher<string> _passwordHasher;

    private readonly UserService _userService;
    private readonly IConfiguration _configuration;

    public DataInitializerController(UserService userService, IConfiguration configuration)
    {
        _passwordHasher = new PasswordHasher<string>();
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("/api/init")]
    [AllowAnonymous]
    public JsonResult Initialize()
    {
        _userService.AddUser(
            new User()
            {
                Id = 1, PasswordHash = _passwordHasher.HashPassword(null, _configuration["AdminPassword"]), Username = _configuration["AdminLogin"],
                Role = UserRole.Admin.ToString()
            });
        _userService.AddUser(
            new User()
            {
                Id = 2, PasswordHash = _passwordHasher.HashPassword(null, "expert"), Username = "expert",
                Role = UserRole.Expert.ToString()
            });
        _userService.AddUser(new User()
        {
            Id = 3, PasswordHash = _passwordHasher.HashPassword(null, "user"), Username = "user",
            Role = UserRole.User.ToString()
        });
        return new JsonResult("");
    }
    
}