using Retrotracker.Domain;

namespace Retrotracker.Application;

public interface IOrderServices
{
    bool Create();
    List<OrderDTO> GetAll(State state);
    bool ChangeState(State state);
}