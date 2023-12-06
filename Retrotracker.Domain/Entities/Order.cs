namespace Retrotracker.Domain;
public class Order
{
    public string Id { get; set; }
    public State State { get; set; }
    public List<Dish> Dishes { get; set; }
    public DateTime Date { get; set; }
    public int TableNumber { get; set; }
    public User Author { get; set; }

    public override string ToString()
    {
        return $"State {State}, Table: {TableNumber}, Date: {Date}, Author: {Author}";
    }

}