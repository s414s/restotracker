using Retrotracker.Domain;

namespace Retrotracker.Application;

public class OrderServices : IOrderServices
{
    private readonly IRepository<Order> _ordersRepo;

    public OrderServices(IRepository<Order> ordersRepo)
    {
        _ordersRepo = ordersRepo;
    }

    public bool ChangeState(string state)
    {
        throw new NotImplementedException();
    }

    public bool Create()
    {
        throw new NotImplementedException();
    }

    public List<OrderDTO> GetAll(string state)
    {
        throw new NotImplementedException();
    }
}
