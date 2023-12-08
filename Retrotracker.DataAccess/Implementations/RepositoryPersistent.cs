using Retrotracker.Domain;
using System.Text.Json;

namespace Retrotracker.DataAccess;

public class RepositoryPersistent<T> where T : class, IHasId
{
    private readonly string _storageFileName;
    private readonly string _path;
    private List<T> _allItems = new();
    public RepositoryPersistent(string jsonFileName)
    {
        _storageFileName = jsonFileName;
        _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalStorage", _storageFileName);
        GetDeserializedItems();
    }

    public virtual T Add(T entity)
    {
        if (_allItems.FindIndex(x => x.Id == entity.Id) == -1)
        {
            _allItems.Add(entity);
        }
        return entity;
    }

    public virtual bool Delete(T entity)
    {
        return _allItems.Remove(entity);
    }

    public virtual IEnumerable<T> GetAll()
    {
        return _allItems;
    }

    public virtual T? GetByID(string id)
    {
        return _allItems.FirstOrDefault(x => x.Id == id);
    }

    public virtual T Update(T entity)
    {
        var ingredientIndex = _allItems.FindIndex(x => x.Id == entity.Id);
        if (ingredientIndex != -1)
        {
            _allItems[ingredientIndex] = entity;
        }
        else
        {
            _allItems.Add(entity);
        }
        return entity;
    }

    public virtual void SaveChanges()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            // PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var payloadAsString = JsonSerializer.Serialize(_allItems, options);
        File.WriteAllText(_path, payloadAsString);
    }

    private void GetDeserializedItems()
    {
        if (!File.Exists(_path))
        {
            throw new NullReferenceException($"The path for the file {_path} does not exist");
        }
        string payload = File.ReadAllText(_path);
        List<T>? deserializeItems = JsonSerializer.Deserialize<List<T>>(payload);
        _allItems = deserializeItems ?? new List<T>();
    }
}