using Retrotracker.Domain;

namespace Retrotracker.Application;

public interface IOrderServices
{
    bool Create(List<DishDTO> dishes, int table);
    bool Delete(OrderDTO order);
    List<OrderDTO> GetAll(string? state);
    bool UpdateState(OrderDTO order, string state);
}