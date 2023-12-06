namespace Retrotracker.Domain;
public class Dish
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<Ingredient> Ingredients { get; set; }

    public Dish() { }

    public Dish(string name, decimal price)
    {
        Id = new Guid().ToString();
        Name = name;
        Price = price;
        Ingredients = new List<Ingredient>();
    }

    public decimal TotalCalories()
    {
        return Ingredients.Sum(x => x.Calories * x.Quantity);
    }

}