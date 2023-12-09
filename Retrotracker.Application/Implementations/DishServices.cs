using Retrotracker.Domain;

namespace Retrotracker.Application;

public class DishServices : IDishServices
{
    private readonly IRepository<Dish> _dishesRepo;

    public DishServices(IRepository<Dish> dishesRepo)
    {
        _dishesRepo = dishesRepo;
    }

    public bool Create()
    {
        throw new NotImplementedException();
    }

    public DishDTO Get(string id)
    {
        throw new NotImplementedException();
    }

    public List<DishDTO> GetAll()
    {
        try
        {
            return _dishesRepo.GetAll().Select(x => DishDTO.MapFromDomainEntity(x)).ToList();
        }
        catch (System.Exception)
        {
            return new List<DishDTO>();
        }
    }
}
