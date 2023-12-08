using Retrotracker.Application;
using Retrotracker.Domain;

namespace Retrotracker.Presentation;
public class ConsoleLogger
{
    private readonly IUserServices _userServices;
    private readonly IOrderServices _orderServices;
    private readonly IDishServices _dishServices;

    // private readonly List<string> loginOptions = new(){
    //     "Sign In",
    //     "Exit",
    // };
    // private readonly List<string> options = new(){
    //     "View Pending Orders",
    //     "View Paid Orders",
    //     "Create New Order",
    //     "Change Order",
    //     "Delete Order",
    //     "Exit",
    // };

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
        _mappedFunctions.Add(9, new Functionality { Description = "Exit", Function = Logout });
    }

    public void Run()
    {
        StartUp();
        AuthenticateUser();
        if (_exit) { return; }
        do
        {
            PrintFunctions(new List<int> { 1, 2, 3, 9 });
            // ItemsLogger<string>.PrintItems(options);
            // AskForOption();
        }
        while (!_exit);
    }

    public void StartUp()
    {
        List<int> options = new() { 1, 9 };
        PrintFunctions(options);
        int chosenOption = AskForInteger("Introduce an option", options);
        _mappedFunctions[chosenOption].Execute();
    }

    public void PrintFunctions(List<int> functions)
    {
        foreach (var key in functions)
        {
            if (_mappedFunctions.TryGetValue(key, out Functionality function))
            {
                Console.WriteLine($"{key}.- {function.ToString()}");
            }
            else
            {
                Console.WriteLine($"Key {key} not found");
            }
        }
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

    public void AskForOption()
    {
        string chosenOption = AskForString("Introduce an option");
        if (_exit) { return; }
        switch (chosenOption)
        {
            case "1":
                PrintOrders(State.ordered);
                break;
            case "2":
                PrintOrders(State.paid);
                break;
            case "3":
                // PrintTransactions(TransactionType.All);
                break;
            case "4":
                // PrintTransactions(TransactionType.Income);
                break;
            case "5":
                // PrintTransactions(TransactionType.Outcome);
                break;
            case "6":
                // PrintAccountBalance();
                break;
            case "7":
                Logout();
                break;
            default:
                Console.WriteLine("Option not available. Please introduce a number between 1 and 7");
                break;
        }
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
        _exit = true;
        Console.WriteLine("Logged out");
    }
}