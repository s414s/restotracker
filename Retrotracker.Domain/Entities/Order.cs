namespace Retrotracker.Domain;
public class Order
{
    public Guid Id { get; set; }
    public State State { get; set; }
    public Dish Dish { get; set; }
    public DateTime Date { get; set; }
    public int Table { get; set; }
    public string AuthorId { get; set; } = string.Empty;
}