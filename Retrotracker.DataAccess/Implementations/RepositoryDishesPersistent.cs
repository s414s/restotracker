using Retrotracker.Domain;
using System.Text.Json;

namespace Retrotracker.DataAccess;
public class RepositoryDishesPersistent : IRepository<Dish>
{
    private readonly string _storageFileName = "dishesStorage.json";
    private readonly string _path;
    private List<Dish> _allItems = new();
    private readonly IRepositoryIngredients _repositoryIngredients;
    public RepositoryDishesPersistent(IRepositoryIngredients ingredientsRepo)
    {
        _repositoryIngredients = ingredientsRepo;
        _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalStorage", _storageFileName);
        GetDeserializedItems();
    }

    public Dish Add(Dish entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Dish entity)
    {
        return _allItems.Remove(entity);
    }

    public IEnumerable<Dish> GetAll()
    {
        return _allItems;
    }

    public Dish? GetByID(string id)
    {
        return _allItems.FirstOrDefault(x => x.Id == id);
    }

    public Dish Update(Dish entity)
    {
        var dataEntity = DishDataEntity.MapFromDomainEntity(entity);
        var dataEntityIndex = _allItems.FindIndex(x => x.Id == entity.Id);
        if (dataEntityIndex != -1)
        {
            _allItems[dataEntityIndex] = entity;
        }
        else
        {
            _allItems.Add(entity);
        }
        return entity;
    }

    public void SaveChanges()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        List<DishDataEntity> data = _allItems.Select(x => DishDataEntity.MapFromDomainEntity(x)).ToList() ?? new();
        var payloadAsString = JsonSerializer.Serialize(data, options);
        File.WriteAllText(_path, payloadAsString);
    }

    private void GetDeserializedItems()
    {
        if (!File.Exists(_path))
        {
            throw new NullReferenceException($"The path for the file {_path} does not exist");
        }
        string payload = File.ReadAllText(_path);
        List<DishDataEntity>? deserializeItems = JsonSerializer.Deserialize<List<DishDataEntity>>(payload) ?? new List<DishDataEntity>();

        List<Dish> domainEntities = new();
        foreach (var item in deserializeItems)
        {
            var ingredients = GetIngredientsFromDish(item).ToList();
            domainEntities.Add(item.MapToDomainEntity(ingredients));
        }
        _allItems = domainEntities;
    }

    private IEnumerable<Ingredient> GetIngredientsFromDish(DishDataEntity dataEntity)
    {
        return _repositoryIngredients.GetByIDs(dataEntity.IngredientsIDs);
    }

}