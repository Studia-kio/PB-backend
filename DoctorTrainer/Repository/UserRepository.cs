using DoctorTrainer.DTO;
using DoctorTrainer.Entity;
using Microsoft.AspNetCore.Identity;

namespace DoctorTrainer.Repository;

public class UserRepository
{
    // todo: connect to database later on
    private List<User> _users;

    public UserRepository()
    {
        _users = new List<User>();
    }

    public User? FindById(int id) =>
        _users.FirstOrDefault(s => s.Id == id);

    public User? FindByUsername(string username) =>
        _users.FirstOrDefault(s => s.Username.Equals(username));

    public List<User> FindAll() =>
        _users.ToList();

    public void Save(User user)
    {
        _users.Add(user);
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }

    public void Delete(User user)
    {
        _users.Remove(user);
    }
    
}