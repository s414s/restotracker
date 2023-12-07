using Retrotracker.Domain;
using System.Text.Json;

namespace Retrotracker.DataAccess;

public class RepositoryIngredientsPersistent : IRepositoryIngredients
{
    private readonly string _storageFileName = "ingredientsStorage.json";
    private readonly string _path;
    public RepositoryIngredientsPersistent()
    {
        _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalStorage", _storageFileName);
    }

    public Ingredient Add(Ingredient entity)
    {
        var allIngredients = GetAll().ToList();

        if (allIngredients.FindIndex(x => x.Name == entity.Name) != -1)
        {
            return entity;
        }

        allIngredients.Add(entity);
        SaveData(allIngredients);
        return entity;
    }

    public bool Delete(Ingredient entity)
    {
        var allIngredients = GetAll().ToList();
        var result = allIngredients.Remove(entity);
        if (result)
        {
            SaveData(allIngredients);
        }
        return result;
    }

    public IEnumerable<Ingredient> GetAll()
    {
        return GetDeserializedItems();
    }

    public Ingredient? GetByID(string id)
    {
        return GetAll().FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Ingredient> GetByIDs(List<string> ids)
    {
        return GetAll().Where(x => ids.Contains(x.Id));
    }

    public Ingredient Update(Ingredient entity)
    {
        var allIngredients = GetAll().ToList();
        var ingredientIndex = allIngredients.FindIndex(x => x.Id == entity.Id);

        if (ingredientIndex != -1)
        {
            allIngredients[ingredientIndex] = entity;
        }
        else
        {
            allIngredients.ToList().Add(entity);
        }

        SaveData(allIngredients);
        return entity;
    }

    private void SaveData(IEnumerable<Ingredient> ingredients)
    {
        var payloadAsString = JsonSerializer.Serialize(ingredients);
        File.WriteAllText(_path, payloadAsString);
    }

    private IEnumerable<Ingredient> GetDeserializedItems()
    {
        string payload = File.ReadAllText(_path);
        List<Ingredient>? deserializeItems = JsonSerializer.Deserialize<List<Ingredient>>(payload);
        return deserializeItems ?? new List<Ingredient>();
    }

}