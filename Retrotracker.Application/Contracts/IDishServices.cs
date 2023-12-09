using Retrotracker.Domain;

namespace Retrotracker.Application;

public interface IDishServices
{
    bool Create();
    DishDTO Get(string id);
    List<DishDTO> GetAll();
}