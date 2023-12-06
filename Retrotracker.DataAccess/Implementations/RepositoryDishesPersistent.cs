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
            var dataEntity = new DishDataEntity().MapFromDomainEntity(entity);
            var allDataEntities = GetDeserializedItems().ToList();
            var result = allDataEntities.Remove(dataEntity);
            if (result)
            {
                SaveData(allDataEntities);
            }
            return result;
        }

        public IEnumerable<Dish> GetAll()
        {
            var allDataEntities = GetDeserializedItems().ToList();
            var allDishes = new List<Dish>();

            foreach (var item in allDataEntities)
            {
                var ingredients = GetIngredientsFromDish(item).ToList();
                allDishes.Add(item.MapToDomainEntity(ingredients));
            }
            return allDishes;
        }

        public Dish? GetByID(string id)
        {
            var dataEntity = GetDeserializedItems().FirstOrDefault(x => x.Id == id);
            if (dataEntity is null)
            {
                return null;
            }
            var dishIngredients = GetIngredientsFromDish(dataEntity).ToList();
            return dataEntity.MapToDomainEntity(dishIngredients);
        }

        public Dish Update(Dish entity)
        {
            var dataEntity = new DishDataEntity().MapFromDomainEntity(entity);
            var allDataEntities = GetDeserializedItems().ToList();
            var dataEntityIndex = allDataEntities.FindIndex(x => x.Id == entity.Id);

            if (dataEntityIndex != -1)
            {
                allDataEntities[dataEntityIndex] = dataEntity;
            }
            else
            {
                allDataEntities.Add(dataEntity);
            }

            SaveData(allDataEntities);
            return entity;
        }

        private void SaveData(IEnumerable<DishDataEntity> ingredients)
        {
            var payloadAsString = JsonSerializer.Serialize(ingredients);
            File.WriteAllText(_path, payloadAsString);
        }

        private IEnumerable<DishDataEntity> GetDeserializedItems()
        {
            string payload = File.ReadAllText(_path);
            List<DishDataEntity>? deserializeItems = JsonSerializer.Deserialize<List<DishDataEntity>>(payload);
            return deserializeItems ?? new List<DishDataEntity>();
        }

        private IEnumerable<Ingredient> GetIngredientsFromDish(DishDataEntity dataEntity)
        {
            return _repositoryIngredients.GetByIDs(dataEntity.IngredientsIDs);
        }

    }
}