using DoctorTrainer.Data;
using DoctorTrainer.Entity;

namespace DoctorTrainer.Repository;

public class UserTokenRepository
{
    private readonly ApplicationDbContext _context;

    public UserTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public UserToken? FindByToken(string token) =>
        _context.UserTokens.FirstOrDefault(t => t.Token.Equals(token));

    public List<UserToken> FindAllByUserId(int userId) => 
        _context.UserTokens.Where(t => t.UserId == userId).ToList();

    public void Save(UserToken token)
    {
        _context.UserTokens.Add(token);
        _context.SaveChanges();
    }

    public void DeleteUserToken(UserToken token)
    {
        _context.UserTokens.Remove(token);
        _context.SaveChanges();
    }

}