namespace Retrotracker.Domain;
public class DishDataEntity : Dish
{
    public List<string> IngredientsIDs { get; set; } = new List<string>();
}