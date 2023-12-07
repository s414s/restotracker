using Retrotracker.Domain;

namespace Retrotracker.Application;

public interface IUserServices
{
    bool Create();
    User Get(string username);
    User Update(User user);
    bool Delete(User user);
}