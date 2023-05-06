using DoctorTrainer.Entity;
using DoctorTrainer.Repository;

namespace DoctorTrainer.Service;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<User> FindAll()
    {
        return _userRepository.FindAll();
    }

    public User? FindById(int id)
    {
        return _userRepository.FindById(id);
    }
    
    public User? FindByUsername(string username)
    {
        return _userRepository.FindByUsername(username);
    }

    public void AddUser(User user)
    {
        _userRepository.Save(user);
    }

    public void Update(User user)
    {
        _userRepository.Update(user);
    }

    public void Delete(User user)
    {
        _userRepository.Delete(user);
    }
}