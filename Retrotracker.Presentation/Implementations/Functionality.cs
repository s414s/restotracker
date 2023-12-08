namespace Retrotracker.Presentation;

public record Functionality
{
    public string Description { get; set; } = "";
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