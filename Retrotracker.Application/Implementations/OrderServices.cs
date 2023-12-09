using Retrotracker.Domain;

namespace Retrotracker.Application;

public class OrderServices : IOrderServices
{
    private readonly IRepository<Order> _ordersRepo;

    public OrderServices(IRepository<Order> ordersRepo)
    {
        _ordersRepo = ordersRepo;
    }

    public bool UpdateState(string orderId, string state)
    {
        throw new NotImplementedException();
    }

    public bool Create(List<DishDTO> dishes, int table)
    {
        var allDishes = dishes.Select(x => x.MapToDomainEntity()).ToList();
        var newOrder = new Order(allDishes, table);
        _ordersRepo.Add(newOrder);
        return true;
    }

    public List<OrderDTO> GetAll(string? state)
    {
        var allOrders = _ordersRepo.GetAll();
        if (state is not null)
        {
            allOrders = allOrders.Where(x => x.State == state);
        }
        return allOrders.Select(x => OrderDTO.MapFromDomainEntity(x)).ToList();
    }
}
