namespace Retrotracker.Domain;
public class Ingredient
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Calories { get; set; }
    public string UnitOfMeasurement { get; set; }
    public decimal TotalQuantity { get; set; }
}