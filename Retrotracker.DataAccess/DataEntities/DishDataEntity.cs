namespace Retrotracker.Domain;
public class DishDataEntity
{
    public string Id { get; set; } = new Guid().ToString();
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<string> IngredientsIDs { get; set; } = new List<string>();

    public Dish MapToDomainEntity(List<Ingredient> ingredients)
    {
        return new Dish
        {
            Id = Id,
            Name = Name,
            Price = Price,
            Ingredients = ingredients
        };
    }

    public static DishDataEntity MapFromDomainEntity(Dish dish)
    {
        return new DishDataEntity
        {
            Id = dish.Id,
            Name = dish.Name,
            Price = dish.Price,
            IngredientsIDs = dish.Ingredients.Select(x => x.Id).ToList(),
        };
    }

}