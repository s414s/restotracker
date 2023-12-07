using Retrotracker.Domain;
using System.Text.Json;

namespace Retrotracker.DataAccess;
public class RepositoryOrdersPersistent : IRepository<Order>
{
    private readonly string _storageFileName = "ingredientsStorage.json";
    private readonly string _path;
    public RepositoryOrdersPersistent()
    {
        _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalStorage", _storageFileName);
    }

    public Order Add(Order entity)
    {
        var allOrders = GetAll().ToList();
        if (allOrders.FindIndex(x => x.Id == entity.Id) != -1)
        {
            return entity;
        }

        allOrders.Add(entity);
        SaveData(allOrders);
        return entity;
    }

    public bool Delete(Order entity)
    {
        var allOrders = GetAll().ToList();
        var result = allOrders.Remove(entity);
        if (result)
        {
            SaveData(allOrders);
        }
        return result;
    }

    public IEnumerable<Order> GetAll()
    {
        return GetDeserializedItems();
    }

    public Order? GetByID(string id)
    {
        return GetAll().FirstOrDefault(x => x.Id == id);
    }

    public Order Update(Order entity)
    {
        var allOrders = GetAll().ToList();
        var ingredientIndex = allOrders.FindIndex(x => x.Id == entity.Id);
        if (ingredientIndex != -1)
        {
            allOrders[ingredientIndex] = entity;
        }
        else
        {
            allOrders.ToList().Add(entity);
        }

        SaveData(allOrders);
        return entity;
    }

    private void SaveData(IEnumerable<Order> ingredients)
    {
        var payloadAsString = JsonSerializer.Serialize(ingredients);
        File.WriteAllText(_path, payloadAsString);
    }

    private List<Order> GetDeserializedItems()
    {
        string payload = File.ReadAllText(_path);
        List<Order>? deserializeItems = JsonSerializer.Deserialize<List<Order>>(payload);
        return deserializeItems ?? new List<Order>();
    }

}