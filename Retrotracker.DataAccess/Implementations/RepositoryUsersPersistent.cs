using Retrotracker.Domain;
using System.Text.Json;

namespace Retrotracker.DataAccess
{
    public class RepositoryUsersPersistent : IRepository<User>
    {
        private readonly string _storageFileName = "usersStorage.json";
        private readonly string _path;
        public RepositoryUsersPersistent()
        {
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalStorage", _storageFileName);
        }

        public User Add(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public User Update(User entity)
        {
            throw new NotImplementedException();
        }

        private void SaveData(IEnumerable<User> ingredients)
        {
            var payloadAsString = JsonSerializer.Serialize(ingredients);
            File.WriteAllText(_path, payloadAsString);
        }

        private List<User> GetDeserializedItems()
        {
            string payload = File.ReadAllText(_path);
            List<User>? deserializeItems = JsonSerializer.Deserialize<List<User>>(payload);
            return deserializeItems ?? new List<User>();
        }

    }
}