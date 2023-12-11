namespace Retrotracker.Presentation;

public class ItemsLogger<T> where T : class
{
    public static void PrintItems(List<T> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}.- {items[i]}");
        }
    }
}