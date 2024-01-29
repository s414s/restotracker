namespace Retrotracker.Application;

public interface IUserServices
{
    bool Create(UserDTO newUser);
    UserDTO Update(UserDTO user);
    bool Delete(UserDTO user);
    UserDTO? SignIn(string username, string password);
}