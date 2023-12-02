namespace DoctorTrainer.TokenGenerators;

public class RefreshTokenGenerator : TokenGenerator
{
    private readonly IConfiguration _configuration;

    public RefreshTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string Generate()
    {
        return GenerateToken(
            _configuration["RefreshTokenSigningKey"],
            "https://localhost:7171/",
            "https://localhost:7171/",
            double.Parse(_configuration["RefreshTokenTimeoutMinutes"])
        );
    }
    
}