using Retrotracker.Domain;

namespace Retrotracker.DataAccess;
public class RepositoryUsersPersistent : RepositoryPersistent<User>, IRepository<User>
{
    public RepositoryUsersPersistent() : base("usersStorage.json") { }

    public override User? GetByID(string username)
    {
        return GetAll().FirstOrDefault(x => x.Username == username);
    }
}