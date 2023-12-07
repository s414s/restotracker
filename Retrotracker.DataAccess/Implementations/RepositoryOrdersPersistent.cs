using Retrotracker.Domain;

namespace Retrotracker.DataAccess;

public class RepositoryOrdersPersistent : RepositoryPersistent<Order>, IRepository<Order>
{
    public RepositoryOrdersPersistent(string jsonFileName) : base(jsonFileName) { }
}