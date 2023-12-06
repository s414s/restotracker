using Retrotracker.Domain;
using System.Text.Json;

namespace Retrotracker.DataAccess
{
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
            throw new NotImplementedException();
        }

        public Order Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order entity)
        {
            throw new NotImplementedException();
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
}