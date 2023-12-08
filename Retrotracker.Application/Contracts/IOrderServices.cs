using Retrotracker.Domain;

namespace Retrotracker.Application;

public interface IOrderServices
{
    bool Create();
    List<OrderDTO> GetAll(string state);
    bool ChangeState(string state);
}