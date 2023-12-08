using System.Text.Json.Serialization;

namespace Retrotracker.Domain;
public class Order : IHasId
{
    public string Id { get; set; } = new Guid().ToString();
    public State State { get; set; } = State.ordered;
    public List<Dish> Dishes { get; set; } = new List<Dish>();
    public DateTime Date { get; set; } = DateTime.Now;
    public int TableNumber { get; set; }
    public User Author { get; set; } = new();
    [JsonIgnore]
    public decimal TotalPrice
    {
        get
        {
            return Dishes.Sum(x => x.Price);
        }
    }

    public Order() { }
    public Order(int tableNumber, List<Dish> dishes, User author)
    {
        TableNumber = tableNumber;
        Dishes = dishes;
        Author = author;
    }

    public override string ToString() => $"State {State}, Table: {TableNumber}, Date: {Date}, Author: {Author}";
}