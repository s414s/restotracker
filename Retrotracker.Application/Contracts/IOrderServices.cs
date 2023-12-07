using Retrotracker.Domain;

namespace Retrotracker.Application;

public interface IOrderServices
{
    bool CreateOrder();
    bool ChangeState(State state);
}