namespace Retrotracker.Domain;
public class Order
{
    public Guid Id { get; set; }
    public string State { get; set; }
    public Dish Dish { get; set; }
    public DateTime Date { get; set; }
    public string AuthorId { get; set; }
}