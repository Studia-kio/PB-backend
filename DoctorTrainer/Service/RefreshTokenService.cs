using DoctorTrainer.Entity;
using DoctorTrainer.Repository;

namespace DoctorTrainer.Service;

public class RefreshTokenService
{
    private readonly UserTokenRepository _userTokenRepository;

    public RefreshTokenService(UserTokenRepository userTokenRepository)
    {
        _userTokenRepository = userTokenRepository;
    }
    
    public void Save(UserToken token)
    {
        _userTokenRepository.Save(token);
    }

    public UserToken? FindByToken(string token)
    {
        return _userTokenRepository.FindByToken(token);
    }

    public void DeleteAllByUser(int userId)
    {
        List<UserToken> tokens = _userTokenRepository.FindAllByUserId(userId);
        tokens.ForEach(t => _userTokenRepository.DeleteUserToken(t));
    }

    public void Delete(UserToken token)
    {
        _userTokenRepository.DeleteUserToken(token);
    }

}