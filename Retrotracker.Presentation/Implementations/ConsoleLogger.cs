using Retrotracker.Application;
using Retrotracker.Domain;

namespace Retrotracker.Presentation;
public class ConsoleLogger
{
    private readonly IUserServices _userServices;
    private readonly IOrderServices _orderServices;
    private readonly IDishServices _dishServices;
    private readonly Dictionary<int, Functionality> _mappedFunctions = new();
    private bool _exit;
    private UserDTO? _activeUser;

    public ConsoleLogger(IUserServices userServices, IOrderServices orderServices, IDishServices dishServices)
    {
        _userServices = userServices;
        _orderServices = orderServices;
        _dishServices = dishServices;

        _mappedFunctions.Add(1, new Functionality { Description = "Sign In", Function = AuthenticateUser });
        _mappedFunctions.Add(2, new Functionality { Description = "Print pending orders", Function = PrintPendingOrders });
        _mappedFunctions.Add(3, new Functionality { Description = "Print paid orders", Function = PrintPaidOrders });
        _mappedFunctions.Add(8, new Functionality { Description = "Sign Out", Function = Logout });
        _mappedFunctions.Add(9, new Functionality { Description = "Exit", Function = Exit });
    }

    public void Run()
    {
        do
        {
            if (_activeUser is null)
            {
                PrintSignInScreen();
            }
            else
            {
                PrintMainScreen(_activeUser.Role);
            }
        }
        while (!_exit);
    }

    public void PrintSignInScreen()
    {
        List<int> options = new() { 1, 9 };
        AskForOption(options);
    }

    public void PrintMainScreen(Role role)
    {
        List<int> options;
        if (role == Role.admin)
        {
            options = new() { 2, 3, 8, 9 };
        }
        else
        {
            options = new() { 2, 3, 8, 9 };
        }
        AskForOption(options);
    }

    public void AskForOption(List<int> functions)
    {
        Console.WriteLine("Choose one of the available options:");
        foreach (var key in functions)
        {
            if (_mappedFunctions.TryGetValue(key, out Functionality? function))
            {
                Console.WriteLine($"{key}.- {function}");
            }
            else
            {
                Console.WriteLine($"Key {key} not found");
            }
        }
        int chosenOption = AskForInteger("Introduce an option", functions);
        _mappedFunctions[chosenOption].Execute();
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
                _activeUser = activeUser;
                return;
                // break;
            }
            Console.WriteLine("Username or password incorrect, try again");
        }
    }

    public void PrintPendingOrders()
    {
        Console.WriteLine("Pending Orders:");
        PrintOrders(State.ordered);
    }

    public void PrintPaidOrders()
    {
        Console.WriteLine("Paid Orders:");
        PrintOrders(State.paid);
    }

    public void PrintOrders(State state)
    {
        var orders = _orderServices.GetAll(state);
        ItemsLogger<OrderDTO>.PrintItems(orders);
    }

    public static int AskForInteger(string consoleText, List<int> allowedRange)
    {
        Console.WriteLine($"{consoleText}.");
        Console.WriteLine($"It must be an integer in the range {allowedRange}.");
        while (true)
        {
            (int validatedInput, string? error) = InputValidator.ParseInteger(Console.ReadLine() ?? "", allowedRange);
            if (error is null)
            {
                return validatedInput;
            }
            Console.WriteLine(error);
            Console.WriteLine("Please make sure your input is correct");
        }
    }

    public static decimal AskForDecimal(string consoleText, int minValue)
    {
        Console.WriteLine($"{consoleText}.");
        Console.WriteLine($"It must be a decimal number greater or equal to {minValue}.");
        while (true)
        {
            (decimal validatedInput, string? error) = InputValidator.ParseDecimal(Console.ReadLine() ?? "", minValue);
            if (error is null)
            {
                return validatedInput;
            }
            Console.WriteLine(error);
            Console.WriteLine("Please make sure your input is correct");
        }
    }

    public static string AskForString(string consoleText)
    {
        Console.WriteLine($"{consoleText}. It must be a valid string");
        while (true)
        {
            (string? validatedInput, string? error) = InputValidator.ParseString(Console.ReadLine() ?? "");
            if (error is null)
            {
                return validatedInput ?? string.Empty;
            }
            Console.WriteLine(error);
            Console.WriteLine("Please make sure your input is a positive integer");
        }
    }

    public void Logout()
    {
        _activeUser = null;
        Console.WriteLine("Logged out");
    }

    public void Exit()
    {
        _exit = true;
        Console.WriteLine("Good Bye");
    }
}