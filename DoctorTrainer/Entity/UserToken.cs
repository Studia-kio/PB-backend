namespace DoctorTrainer.Entity;

public class UserToken
{
    public long Id { get; set; }
    public string Token { get; set; }
    public int UserId { get; set; }
}