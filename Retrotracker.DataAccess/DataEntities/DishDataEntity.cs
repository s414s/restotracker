using System.Text.Json.Serialization;

namespace Retrotracker.Domain;
public class DishDataEntity
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = new Guid().ToString();
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    [JsonPropertyName("quantities")]
    public List<decimal> Quantities { get; set; } = new List<decimal>();
    [JsonPropertyName("ingredientsIDs")]
    public List<string> IngredientsIDs { get; set; } = new List<string>();

    public Dish MapToDomainEntity(List<Ingredient> ingredients)
    {
        return new Dish
        {
            Id = Id,
            Name = Name,
            Price = Price,
            Ingredients = Quantities.Select((x, i) => new IngredientDecomposition
            {
                Quantity = x,
                Ingredient = ingredients[i].Id == IngredientsIDs[i] ? ingredients[i] : new Ingredient { },
            }).ToList()
        };
    }

    public static DishDataEntity MapFromDomainEntity(Dish dish)
    {
        return new DishDataEntity
        {
            Id = dish.Id,
            Name = dish.Name,
            Price = dish.Price,
            Quantities = dish.Ingredients.Select(x => x.Quantity).ToList(),
            IngredientsIDs = dish.Ingredients.Select(x => x.Ingredient.Id).ToList(),
        };
    }
}