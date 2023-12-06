using Retrotracker.Domain;

namespace Retrotracker.DataAccess
{
    public class RepositoryIngredientsPersistent : IRepository<Ingredient>
    {
        private readonly string _storageFileName = "ingredientsStorage.json";
        private readonly string _path;
        public RepositoryIngredientsPersistent()
        {
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalStorage", _storageFileName);
        }

        public Ingredient Add(Ingredient entity)
        {
            throw new NotImplementedException();
        }

        public Ingredient Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ingredient> GetAll()
        {
            throw new NotImplementedException();
        }

        public Ingredient GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public Ingredient Update(Ingredient entity)
        {
            throw new NotImplementedException();
        }
    }
}