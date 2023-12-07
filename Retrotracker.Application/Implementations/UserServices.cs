using Retrotracker.Domain;

namespace Retrotracker.Application;

public class UserServices : IUserServices
{
    private readonly IRepository<User> _usersRepo;

    public UserServices(IRepository<User> usersRepo)
    {
        _usersRepo = usersRepo;
    }

    public bool Create(UserDTO newUser)
    {
        try
        {
            var result = _usersRepo.Add(newUser.MapToDomainEntity());
            return result is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Delete(UserDTO user)
    {
        try
        {
            return _usersRepo.Delete(user.MapToDomainEntity());
        }
        catch (Exception)
        {
            return false;
        }
    }

    public UserDTO Update(UserDTO user)
    {
        try
        {
            _usersRepo.Update(user.MapToDomainEntity());
            return user;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public UserDTO? SignIn(AuthUserDTO userCredentials)
    {
        try
        {
            var user = _usersRepo.GetByID(userCredentials.Username);
            if (user?.Password != userCredentials.Password)
            {
                return null;
            }
            return UserDTO.MapFromDomainEntity(user);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
