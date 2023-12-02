using System.Security.Claims;
using DoctorTrainer.Entity;

namespace DoctorTrainer.TokenGenerators;

public class AccessTokenGenerator : TokenGenerator
{
    private readonly IConfiguration _configuration;

    public AccessTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string Generate(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        return GenerateToken(
            _configuration["SigningKey"],
            "https://localhost:7171/",
            "https://localhost:7171/",
            double.Parse(_configuration["TokenTimeoutMinutes"]),
            claims
        );
    }

}

