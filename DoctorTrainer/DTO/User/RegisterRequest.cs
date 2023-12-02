namespace DoctorTrainer.DTO.User;

public class RegisterRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string PasswordRetype { get; set; }
}