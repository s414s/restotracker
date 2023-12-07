using Retrotracker.Domain;

namespace Retrotracker.DataAccess
{
    public class RepositoryUsersPersistent : RepositoryPersistent<User>, IRepository<User>
    {
        public RepositoryUsersPersistent(string jsonFileName) : base(jsonFileName) { }

        public override User? GetByID(string username)
        {
            return GetAll().FirstOrDefault(x => x.Username == username);
        }
    }
}