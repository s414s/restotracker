namespace Retrotracker.Domain;
public class Order
{
    public Guid Id { get; set; }
    public State State { get; set; }
    public List<Dish> Dishes { get; set; }
    public DateTime Date { get; set; }
    public int TableNumber { get; set; }
    public User Author { get; set; }
}