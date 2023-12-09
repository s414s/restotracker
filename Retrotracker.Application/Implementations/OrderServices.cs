using Retrotracker.Domain;

namespace Retrotracker.Application;
public class OrderServices : IOrderServices
{
    private readonly IRepository<Order> _ordersRepo;
    public OrderServices(IRepository<Order> ordersRepo)
    {
        _ordersRepo = ordersRepo;
    }

    public bool UpdateState(OrderDTO orderTarget, string state)
    {
        try
        {
            var orderId = orderTarget.Id;
            var order = _ordersRepo.GetByID(orderId);
            if (order is null)
            {
                return false;
            }
            order.State = state;
            _ordersRepo.Add(order);
            _ordersRepo.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            // TODO - log error
            return false;
        }
    }

    public bool Create(List<DishDTO> dishes, int table)
    {
        try
        {
            var allDishes = dishes.Select(x => x.MapToDomainEntity()).ToList();
            var newOrder = new Order(allDishes, table);
            _ordersRepo.Add(newOrder);
            _ordersRepo.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            // TODO - log error
            return false;
        }
    }

    public bool Delete(OrderDTO order)
    {
        try
        {
            return _ordersRepo.Delete(order.MapToDomainEntity());
        }
        catch (Exception)
        {
            // TODO - log error
            return false;
        }
    }

    public List<OrderDTO> GetAll(string? state)
    {
        try
        {
            var allOrders = _ordersRepo.GetAll();
            if (state is not null)
            {
                allOrders = allOrders.Where(x => x.State == state);
            }
            return allOrders.Select(x => OrderDTO.MapFromDomainEntity(x)).ToList();

        }
        catch (Exception)
        {
            // TODO - log error
            return new List<OrderDTO>();
        }
    }
}
