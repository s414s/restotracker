using Retrotracker.Domain;

namespace Retrotracker.Application;

public interface IOrderServices
{
    bool Create(List<DishDTO> dishes, int table);
    List<OrderDTO> GetAll(string? state);
    bool ChangeState(string state);
}