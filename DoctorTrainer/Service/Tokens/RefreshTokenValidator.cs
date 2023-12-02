using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DoctorTrainer.TokenGenerators;

public class RefreshTokenValidator
{
    private readonly IConfiguration _configuration;
    
    public RefreshTokenValidator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public bool Validate(string refreshToken)
    {
        TokenValidationParameters validationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7171/",
            ValidAudience = "https://localhost:7171/",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["RefreshTokenSigningKey"]))
        };
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        try
        {
            handler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}