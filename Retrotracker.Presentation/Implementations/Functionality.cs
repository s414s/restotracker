namespace Retrotracker.Presentation;

public record Functionality
{
    public int Key { get; set; }
    public string Description { get; set; } = "No description available";
    public Action Function { get; set; } = () => Console.WriteLine("No Function Mapped");
    public override string ToString()
    {
        return Description;
    }
    public void Execute()
    {
        Function();
    }
}