using Retrotracker.Application;
using Retrotracker.Domain;

namespace Retrotracker.Presentation;
public class ConsoleLogger
{
    private readonly IUserServices _userServices;
    private readonly IOrderServices _orderServices;
    private readonly IDishServices _dishServices;
    private readonly List<string> loginOptions = new(){
        "Sign In",
        "Exit",
    };

    private readonly List<string> options = new(){
        "View Available Dishes",
        "View Pending Orders",
        "Create New Orders",
        "Change Order",
        "Delete Orders",
        "Exit",
    };

    private bool exit = false;
    private UserDTO? activeUser = null;

    public ConsoleLogger(IUserServices userServices, IOrderServices orderServices, IDishServices dishServices)
    {
        _userServices = userServices;
        _orderServices = orderServices;
        _dishServices = dishServices;
    }

    public void Run()
    {
        StartUp();
        AuthenticateUser();
        // if (exit) { return; }
        do
        {
            ItemsLogger<string>.PrintItems(options);
            // AskForOption();
        }
        while (!exit);
    }

    public void StartUp()
    {
        ItemsLogger<string>.PrintItems(loginOptions);
        int optionIndex = AskForInteger("Introduce an option", 1, loginOptions.Count + 1) - 1;
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

    public void PrintOrders(State state)
    {
        var orders = _orderServices.GetAll(state);
        Console.WriteLine(state == State.ordered ? "Pending Orders:" : "Paid Orders");
        ItemsLogger<OrderDTO>.PrintItems(orders);
    }

    public void AskForOption()
    {
        string chosenOption = AskForString("Introduce an option");
        if (exit) { return; }
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

    public static int AskForInteger(string consoleText, int minValue, int maxValue)
    {
        Console.WriteLine($"{consoleText}.");
        Console.WriteLine($"It must be an integer between {minValue} and {maxValue}.");
        while (true)
        {
            (int validatedInput, string? error) = InputValidator.ParseInteger(Console.ReadLine() ?? "", minValue, maxValue);
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
            (string validatedInput, string? error) = InputValidator.ParseString(Console.ReadLine() ?? "");
            if (error is null)
            {
                return validatedInput;
            }
            Console.WriteLine(error);
            Console.WriteLine("Please make sure your input is a positive integer");
        }
    }

    public void Logout()
    {
        activeUser = null;
        Console.WriteLine("Logged out");
    }
}