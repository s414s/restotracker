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
            _usersRepo.SaveChanges();
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
            var result = _usersRepo.Delete(user.MapToDomainEntity());
            _usersRepo.SaveChanges();
            return result;
        }
        catch (Exception)
        {
            // TODO - log error
            return false;
        }
    }

    public UserDTO Update(UserDTO user)
    {
        try
        {
            _usersRepo.Update(user.MapToDomainEntity());
            _usersRepo.SaveChanges();
            return user;
        }
        catch (Exception)
        {
            // TODO - log error
            return new UserDTO();
        }
    }

    public UserDTO? SignIn(string username, string password)
    {
        try
        {
            var user = _usersRepo.GetByID(username);
            if (user?.Password != password)
            {
                return null;
            }
            return UserDTO.MapFromDomainEntity(user);
        }
        catch (Exception)
        {
            // TODO - log error
            return null;
        }
    }
}
