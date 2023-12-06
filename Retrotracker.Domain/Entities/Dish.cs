namespace Retrotracker.Domain;
public class Dish
{
    public string Id { get; set; } = new Guid().ToString();
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public decimal Calories
    {
        get { return Ingredients.Sum(x => x.Calories * x.Quantity); }
        private set { }
    }

    public Dish() { }

    public Dish(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"Name {Name}, Price: {Price}, Calories: {Calories}";
    }

}