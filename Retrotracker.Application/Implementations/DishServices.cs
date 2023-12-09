using Retrotracker.Domain;

namespace Retrotracker.Application;
public class DishServices : IDishServices
{
    private readonly IRepository<Dish> _dishesRepo;
    public DishServices(IRepository<Dish> dishesRepo)
    {
        _dishesRepo = dishesRepo;
    }

    public List<DishDTO> GetAll()
    {
        try
        {
            return _dishesRepo.GetAll().Select(x => DishDTO.MapFromDomainEntity(x)).ToList();
        }
        catch (Exception)
        {
            return new List<DishDTO>();
        }
    }
}