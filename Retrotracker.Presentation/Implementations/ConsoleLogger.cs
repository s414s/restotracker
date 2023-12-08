using Retrotracker.Application;

namespace Retrotracker.Presentation;
public class ConsoleLogger
{
    private readonly IUserServices _userServices;
    private readonly IOrderServices _orderrServices;
    private readonly IDishServices _dishServices;
    private readonly List<string> optionNames = new(){
                {"1. Sign In"},
                {"2. Create User"},
                {"3. Exit"},
        };

    private bool exit = false;
    private UserDTO? activeUser = null;

    public ConsoleLogger(IUserServices userServices, IOrderServices orderServices, IDishServices dishServices)
    {
        _userServices = userServices;
        _orderrServices = orderServices;
        _dishServices = dishServices;
    }

    public void Run()
    {
        AuthenticateUser();
        // if (exit) { return; }

        do
        {
            PrintOptions();
            // AskForOption();
        }
        while (!exit);
    }

    public void AuthenticateUser()
    {
        Console.WriteLine("Authenticate your user");
        while (true)
        {
            string inputUsername = AskForString("Introduce your username");
            string inputPassword = AskForString("Introduce your password");
            var activeUser = _userServices.SignIn(inputUsername, inputPassword);
            if (activeUser is not null)
            {
                this.activeUser = activeUser;
                return;
                // break;
            }
            Console.WriteLine("Username or password incorrect, try again");
        }
    }

    public static int AskForInteger(string consoleText, int minimumValue)
    {
        Console.WriteLine($"{consoleText}. It must be an integer greater or equal to {minimumValue}.");
        while (true)
        {
            (int validatedInput, string error) = InputValidator.ParseInteger(Console.ReadLine(), minimumValue);
            if (error is null)
            {
                return validatedInput;
            }
            else
            {
                Console.WriteLine(error);
                Console.WriteLine("Please make sure your input is correct");
            }
        }
    }

    public static decimal AskForDecimal(string consoleText, int minimumValue)
    {
        Console.WriteLine($"{consoleText}. It must be a decimal number greater or equal to {minimumValue}.");
        while (true)
        {
            (decimal validatedInput, string error) = InputValidator.ParseDecimal(Console.ReadLine(), minimumValue);

            if (error is null)
            {
                return validatedInput;
            }
            else
            {
                Console.WriteLine(error);
                Console.WriteLine("Please make sure your input is correct");
            }
        }
    }

    public static string AskForString(string consoleText)
    {
        Console.WriteLine($"{consoleText}. It must be a valid string");
        while (true)
        {
            (string validatedInput, string error) = InputValidator.ParseString(Console.ReadLine());

            if (error is null)
            {
                return validatedInput;
            }
            else
            {
                Console.WriteLine(error);
                Console.WriteLine("Please make sure your input is a positive integer");
            }
        }
    }

    public void PrintOptions()
    {
        foreach (string option in optionNames)
        {
            Console.WriteLine(option);
        }
    }

    public void Logout()
    {
        activeUser = null;
        Console.WriteLine("Logging out...");
    }
}