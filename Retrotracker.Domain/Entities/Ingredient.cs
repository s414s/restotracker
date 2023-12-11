using System.Text.Json.Serialization;

namespace Retrotracker.Domain;
public class Ingredient : IHasId
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = new Guid().ToString();
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("unitOfMeasurement")]
    public string UnitOfMeasurement { get; set; } = string.Empty;
    [JsonPropertyName("calories")]
    public int Calories { get; set; }
    public Ingredient() { }
}

public class IngredientDecomposition
{
    public decimal Quantity { get; set; }
    public Ingredient Ingredient { get; set; } = new();
}