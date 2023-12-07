using Retrotracker.Domain;

namespace Retrotracker.DataAccess;
public class RepositoryIngredientsPersistent : RepositoryPersistent<Ingredient>, IRepositoryIngredients
{
    public RepositoryIngredientsPersistent(string jsonFileName) : base(jsonFileName) { }

    public IEnumerable<Ingredient> GetByIDs(List<string> ids)
    {
        return GetAll().Where(x => ids.Contains(x.Id));
    }
}