using System.Text.Json.Serialization;

namespace Retrotracker.Domain;
public class Dish : IHasId
{
    public string Id { get; set; } = new Guid().ToString();
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<IngredientDecomposition> Ingredients { get; set; } = new List<IngredientDecomposition>();
    [JsonIgnore]
    public decimal Calories
    {
        get { return Ingredients.Sum(x => x.Ingredient.Calories * x.Quantity); }
    }

    public Dish() { }
    public Dish(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}