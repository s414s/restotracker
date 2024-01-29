using Retrotracker.Domain;

namespace Retrotracker.Application;
public class DishDTO
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Calories { get; set; }

    public Dish MapToDomainEntity()
    {
        return new Dish
        {
            Id = Id,
            Name = Name,
            Price = Price,
        };
    }

    public static DishDTO MapFromDomainEntity(Dish dish)
    {
        return new DishDTO
        {
            Id = dish.Id,
            Name = dish.Name,
            Price = dish.Price,
            Calories = dish.Calories,
        };
    }
    public override string ToString() => $"Name {Name} - {Price}$ - Cal: {Calories}";
}