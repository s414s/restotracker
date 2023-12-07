using Retrotracker.Domain;

namespace Retrotracker.Application;
public class OrderDTO
{
    public string Id { get; set; }
    public State State { get; set; }
    public DateTime Date { get; set; }
    public int TableNumber { get; set; }

    public Order MapToDomainEntity()
    {
        return new Order
        {
            Id = Id,
            State = State,
            Date = Date,
            TableNumber = TableNumber,
        };
    }

    public OrderDTO MapFromDomainEntity(Order order)
    {
        return new OrderDTO
        {
            Id = order.Id,
            State = order.State,
            Date = order.Date,
            TableNumber = order.TableNumber,
        };
    }
}