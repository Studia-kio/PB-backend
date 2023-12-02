using DoctorTrainer.Data;
using DoctorTrainer.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DoctorTrainer.Repository;

public class UserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public User? FindById(int id) =>
        _context.Users
            .FirstOrDefault(s => s.Id == id);

    public User? FindByUsername(string username) =>
        _context.Users
            .FirstOrDefault(s => s.Username.Equals(username));

    public List<User> FindAll() =>
        _context.Users
            .ToList();

    public int Save(User user)
    {
        EntityEntry<User> saved = _context.Users.Add(user);
        _context.SaveChanges();
        return saved.Entity.Id;
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
    
}