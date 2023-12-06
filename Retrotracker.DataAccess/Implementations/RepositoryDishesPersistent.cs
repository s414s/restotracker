using Retrotracker.Domain;
using System.Text.Json;

namespace Retrotracker.DataAccess
{
    public class RepositoryDishesPersistent : IRepository<Dish>
    {
        private readonly string _storageFileName = "dishesStorage.json";
        private readonly string _path;
        private readonly IRepositoryIngredients _repositoryIngredients;
        public RepositoryDishesPersistent(IRepositoryIngredients ingredientsRepo)
        {
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalStorage", _storageFileName);
            _repositoryIngredients = ingredientsRepo;
        }

        public Dish Add(Dish entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Dish entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dish> GetAll()
        {
            throw new NotImplementedException();
        }

        public Dish GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public Dish Update(Dish entity)
        {
            throw new NotImplementedException();
        }

        private void SaveData(IEnumerable<Dish> ingredients)
        {
            var payloadAsString = JsonSerializer.Serialize(ingredients);
            File.WriteAllText(_path, payloadAsString);
        }

        private List<Dish> GetDeserializedItems()
        {
            string payload = File.ReadAllText(_path);
            List<Dish>? deserializeItems = JsonSerializer.Deserialize<List<Dish>>(payload);
            return deserializeItems ?? new List<Dish>();
        }

    }
}