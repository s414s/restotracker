using Retrotracker.Domain;

namespace Retrotracker.Application;

public interface IOrderServices
{
    bool Create();
    bool ChangeState(State state);
}