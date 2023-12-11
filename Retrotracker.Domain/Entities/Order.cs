using System.Text.Json.Serialization;

namespace Retrotracker.Domain;
public class Order : IHasId
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = new Guid().ToString();
    [JsonPropertyName("state")]
    public string State { get; set; } = "ordered";
    [JsonPropertyName("dishes")]
    public List<Dish> Dishes { get; set; } = new List<Dish>();
    [JsonPropertyName("date")]
    public DateTime Date { get; set; } = DateTime.Now;
    [JsonPropertyName("tableNumber")]
    public int TableNumber { get; set; }
    [JsonPropertyName("author")]
    [JsonIgnore]
    public decimal TotalPrice
    {
        get
        {
            return Dishes.Sum(x => x.Price);
        }
    }

    public Order() { }
    public Order(List<Dish> dishes, int tableNumber)
    {
        TableNumber = tableNumber;
        Dishes = dishes;
    }
}