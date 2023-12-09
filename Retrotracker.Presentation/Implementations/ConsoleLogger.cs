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
    private List<int> _tables;
    private UserDTO? _activeUser;
    public ConsoleLogger(IUserServices userServices, IOrderServices orderServices, IDishServices dishServices)
    {
        _userServices = userServices;
        _orderServices = orderServices;
        _dishServices = dishServices;

        _tables = new() { 1, 2, 3, 4, 5 };
        _mappedFunctions.Add(1, new Functionality { Description = "Sign In", Function = AuthenticateUser });
        _mappedFunctions.Add(2, new Functionality { Description = "Print pending orders", Function = PrintPendingOrders });
        _mappedFunctions.Add(3, new Functionality { Description = "Print paid orders", Function = PrintPaidOrders });
        _mappedFunctions.Add(4, new Functionality { Description = "Create a new order", Function = PrintNewOrder });
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
                continue;
            }
            PrintMainScreen(_activeUser.Role);
        }
        while (!_exit);
    }

    private void PrintSignInScreen()
    {
        List<int> options = new() { 1, 9 };
        AskForOption(options);
    }

    private void PrintMainScreen(string role)
    {
        List<int> options = role == "admin"
            ? new() { 2, 3, 8, 9 }
            : new() { 2, 3, 8, 9 };
        AskForOption(options);
    }

    private void AskForOption(List<int> functions)
    {
        Console.WriteLine("OPTIONS");
        foreach (var key in functions)
        {
            if (_mappedFunctions.TryGetValue(key, out Functionality? function))
            {
                Console.WriteLine($"{key}.- {function}");
                continue;
            }
            Console.WriteLine($"Key {key} not found");
        }
        int chosenOption = AskForInteger("Choose an option", functions);
        _mappedFunctions[chosenOption].Execute();
    }

    private void AuthenticateUser()
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

    private void PrintPendingOrders()
    {
        Console.WriteLine("Pending Orders:");
        PrintOrders("ordered");
    }

    private void PrintPaidOrders()
    {
        Console.WriteLine("Paid Orders:");
        PrintOrders("paid");
    }

    private void PrintNewOrder()
    {
        List<DishDTO> allDishes = _dishServices.GetAll();
        ItemsLogger<DishDTO>.PrintItems(allDishes);
        List<DishDTO> selectedDishes = new();

        var indexes = allDishes.Select((x, i) => i + 1).ToList() ?? new List<int>();
        List<int> selectedIndexes = AskForIntegers("Select the dishes for this order:", indexes);
        foreach (var index in selectedIndexes)
        {
            var selectedDish = allDishes[index - 1];
            if (selectedDish is not null)
            {
                selectedDishes.Add(selectedDish);
            }
        }

        int selectedTable = AskForInteger("Now select the table:", _tables);
        _orderServices.Create(selectedDishes, selectedTable);
        Console.WriteLine("Orders Created Correctly");
    }

    private void PrintDeleteOrder()
    {
        List<OrderDTO> allOrders = _orderServices.GetAll(null);
        ItemsLogger<OrderDTO>.PrintItems(allOrders);
        var indexes = allOrders.Select((x, i) => i + 1).ToList() ?? new List<int>();

        int chosenOrder = AskForInteger("Choose which order you want to delete", indexes);
    }

    private void PrintModifyOrder()
    {
    }

    private void PrintOrders(string state)
    {
        var orders = _orderServices.GetAll(state);
        ItemsLogger<OrderDTO>.PrintItems(orders);
    }

    private static int AskForInteger(string consoleText, List<int> allowedRange)
    {
        var values = string.Join(", ", allowedRange);
        Console.WriteLine($"{consoleText}.");
        Console.WriteLine($"It must be one of the following integers: {values}.");
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

    private static List<int> AskForIntegers(string consoleText, List<int> allowedRange)
    {
        var allowedValues = string.Join(", ", allowedRange);
        Console.WriteLine($"{consoleText}.");
        Console.WriteLine($"It must be one or more numbers, followed my a coma: {allowedValues}.");
        while (true)
        {
            (List<int> validatedInput, string? error) = InputValidator.ParseIntegers(Console.ReadLine() ?? "", allowedRange);
            if (error is null)
            {
                return validatedInput;
            }
            Console.WriteLine(error);
            Console.WriteLine("Please make sure your input is correct");
        }
    }

    private static decimal AskForDecimal(string consoleText, int minValue)
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

    private static string AskForString(string consoleText)
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

    private void Logout()
    {
        _activeUser = null;
        Console.WriteLine("Logged out");
    }

    private void Exit()
    {
        _exit = true;
        Console.WriteLine("Good Bye");
    }
}