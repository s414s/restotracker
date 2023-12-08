using System.Globalization;

namespace Retrotracker.Application;
public class InputValidator
{
    public static (int validatedInput, string? error) ParseInteger(string userInput, int minValue, int maxValue)
    {
        if (int.TryParse(userInput, out int validatedInput) && validatedInput >= minValue && validatedInput <= maxValue)
        {
            return (validatedInput, null);
        }
        return (0, "Invalid input");
    }

    public static (decimal validatedInput, string? error) ParseDecimal(string userInput, int minimumValue)
    {
        if (decimal.TryParse(userInput, out decimal validatedInput) && validatedInput >= minimumValue)
        {
            return (validatedInput, null);
        }
        return (0, "Invalid input");
    }

    public static (DateTime validatedInput, string? error) ParseDate(string userInput)
    {
        if (DateTime.TryParseExact(userInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validatedInput))
        {
            return (validatedInput, null);
        }
        return (new DateTime(), "Date input not valid");
    }

    public static (string? validatedInput, string? error) ParseString(string userInput)
    {
        if (!string.IsNullOrEmpty(userInput))
        {
            return (userInput, null);
        }
        return (null, "Invalid input, the string is null or empty");
    }
}