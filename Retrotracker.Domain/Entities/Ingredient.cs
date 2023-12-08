namespace Retrotracker.Domain;
public class Ingredient : IHasId
{
    public string Id { get; set; } = new Guid().ToString();
    public string Name { get; set; } = string.Empty;
    public string UnitOfMeasurement { get; set; } = string.Empty;
    public int Calories { get; set; }
    public Ingredient() { }
}

public class IngredientDecomposition
{
    public decimal Quantity { get; set; }
    public Ingredient Ingredient { get; set; }
}