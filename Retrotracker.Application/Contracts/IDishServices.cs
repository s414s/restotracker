using Retrotracker.Domain;

namespace Retrotracker.Application;

public interface IDishServices
{
    bool Create();
    Dish Get(string id);
}