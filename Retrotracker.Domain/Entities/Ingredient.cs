namespace Retrotracker.Domain;
public class Ingredient : IHasId
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Calories { get; set; }
    public string UnitOfMeasurement { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public Ingredient() { }
}