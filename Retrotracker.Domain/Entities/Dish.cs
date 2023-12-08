using System.Text.Json.Serialization;

namespace Retrotracker.Domain;
public class Dish : IHasId
{
    public string Id { get; set; } = new Guid().ToString();
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    [JsonIgnore]
    public decimal Calories
    {
        get { return Ingredients.Sum(x => x.Calories * x.Quantity); }
    }

    public Dish() { }

    public Dish(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString() => $"Name {Name}, Price: {Price}, Calories: {Calories}";
}