using Retrotracker.Domain;

namespace Retrotracker.DataAccess;

public class RepositoryOrdersPersistent : RepositoryPersistent<Order>, IRepository<Order>
{
    public RepositoryOrdersPersistent() : base("ordersStorage.json") { }
    public IEnumerable<Order> GetByState(string state)
    {
        return GetAll().Where(x => x.State == state);
    }
}