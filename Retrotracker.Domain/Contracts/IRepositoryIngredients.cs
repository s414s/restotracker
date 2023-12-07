namespace Retrotracker.Domain;

public interface IRepositoryIngredients : IRepository<Ingredient>
{
    IEnumerable<Ingredient> GetByIDs(List<string> ids);
}