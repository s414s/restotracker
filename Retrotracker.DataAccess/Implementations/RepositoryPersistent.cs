using Retrotracker.Domain;
using System.Text.Json;

namespace Retrotracker.DataAccess;
public class RepositoryPersistent<T> where T : class, IHasId
{
    private readonly string _storageFileName;
    private readonly string _path;
    public RepositoryPersistent(string jsonFileName)
    {
        _storageFileName = jsonFileName;
        _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalStorage", _storageFileName);
    }

    public virtual T Add(T entity)
    {
        var allEntities = GetAll().ToList();
        if (allEntities.FindIndex(x => x.Id == entity.Id) != -1)
        {
            return entity;
        }

        allEntities.Add(entity);
        SaveData(allEntities);
        return entity;
    }

    public virtual bool Delete(T entity)
    {
        var allEntities = GetAll().ToList();
        var result = allEntities.Remove(entity);
        if (result)
        {
            SaveData(allEntities);
        }
        return result;
    }

    public virtual IEnumerable<T> GetAll()
    {
        return GetDeserializedItems();
    }

    public virtual T? GetByID(string id)
    {
        return GetAll().FirstOrDefault(x => x.Id == id);
    }

    public virtual T Update(T entity)
    {
        var allEntities = GetAll().ToList();
        var ingredientIndex = allEntities.FindIndex(x => x.Id == entity.Id);

        if (ingredientIndex != -1)
        {
            allEntities[ingredientIndex] = entity;
        }
        else
        {
            allEntities.ToList().Add(entity);
        }

        SaveData(allEntities);
        return entity;
    }

    private void SaveData(IEnumerable<T> ingredients)
    {
        var payloadAsString = JsonSerializer.Serialize(ingredients);
        File.WriteAllText(_path, payloadAsString);
    }

    private List<T> GetDeserializedItems()
    {
        string payload = File.ReadAllText(_path);
        List<T>? deserializeItems = JsonSerializer.Deserialize<List<T>>(payload);
        return deserializeItems ?? new List<T>();
    }

}