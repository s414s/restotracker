using Retrotracker.Application;

namespace Retrotracker.Presentation;
public class MenuPrinter: IMenuPrinter
{
    private readonly IUserServices _userServices;
    private readonly IOrderServices _orderServices;
    private readonly IDishServices _dishServices;
    private readonly List<Functionality> _functions;
    private bool _exit;
    private readonly List<int> _tables;
    private UserDTO? _activeUser;
    public MenuPrinter(IUserServices userServices, IOrderServices orderServices, IDishServices dishServices)
    {
        _userServices = userServices;
        _orderServices = orderServices;
        _dishServices = dishServices;
        _tables = new() { 1, 2, 3, 4, 5 };
        _functions = new(){
            new Functionality { Key = 1, Description = "Sign In", Function = AuthenticateUser },
            new Functionality { Key = 2, Description = "Print pending orders", Function = PrintPendingOrders },
            new Functionality { Key = 3, Description = "Print paid orders", Function = PrintPaidOrders },
            new Functionality { Key = 4, Description = "Create a new order", Function = PrintAddNewOrder },
            new Functionality { Key = 5, Description = "Delete an order", Function = PrintDeleteOrder },
            new Functionality { Key = 6, Description = "Modify the state of an open order", Function = PrintModifyOrderState },
            new Functionality { Key = 7, Description = "Sign Out", Function = Logout },
            new Functionality { Key = 8, Description = "Exit", Function = Exit }
        };
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
        List<int> options = new() { 1, 8 };
        AskForOption(options);
    }

    private void PrintMainScreen(string role)
    {
        List<int> options = role == "admin"
            ? new() { 2, 3, 4, 5, 6, 7, 8 }
            : new() { 2, 3, 4, 5, 6, 7, 8 };
        AskForOption(options);
    }

    private void AskForOption(List<int> functions)
    {
        Console.WriteLine("OPTIONS");
        foreach (var key in functions)
        {
            var functionality = _functions.FirstOrDefault(x => x.Key == key);
            if(functionality is not null)
            {
                Console.WriteLine($"{key}.- {functionality.Description}");
                continue;
            }
            Console.WriteLine($"Key {key} not found");
        }
        int chosenOption = ValueSeeker.AskForInteger("Choose an option", functions);
        ExecuteFunction(chosenOption);
    }

    private void AuthenticateUser()
    {
        Console.WriteLine("Authenticate your user");
        while (true)
        {
            string inputUsername = ValueSeeker.AskForString("Introduce your username");
            string inputPassword = ValueSeeker.AskForString("Introduce your password");
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

    private void ExecuteFunction(int key)
    {
        _functions.FirstOrDefault(x => x.Key == key)?.Execute();
    }

    private void PrintPaidOrders()
    {
        Console.WriteLine("Paid Orders:");
        PrintOrders("paid");
    }

    private void PrintAddNewOrder()
    {
        List<DishDTO> allDishes = _dishServices.GetAll();
        ItemsLogger<DishDTO>.PrintItems(allDishes);
        List<DishDTO> selectedDishes = new();
        var indexes = allDishes.Select((x, i) => i + 1).ToList() ?? new List<int>();
        List<int> selectedIndexes = ValueSeeker.AskForIntegers("Select the dishes for this order:", indexes);
        foreach (var index in selectedIndexes)
        {
            var selectedDish = allDishes[index - 1];
            if (selectedDish is not null)
            {
                selectedDishes.Add(selectedDish);
            }
        }
        int selectedTable = ValueSeeker.AskForInteger("Now select the table:", _tables);
        var result = _orderServices.Create(selectedDishes, selectedTable);
        Console.WriteLine(result ? "Order Created Successfully" : "Order Could not be Created");
    }

    private void PrintDeleteOrder()
    {
        List<OrderDTO> allOrders = _orderServices.GetAll(null);
        ItemsLogger<OrderDTO>.PrintItems(allOrders);
        var indexes = allOrders.Select((x, i) => i + 1).ToList() ?? new List<int>();
        int chosenOrderIndex = ValueSeeker.AskForInteger("Choose which order you want to delete", indexes);
        var result = _orderServices.Delete(allOrders[chosenOrderIndex]);
        Console.WriteLine(result ? "Order Successfully Deleted" : "Order Unsuccessfully Deleted");
    }

    private void PrintModifyOrderState()
    {
        List<OrderDTO> allOrders = _orderServices.GetAll("ordered");
        ItemsLogger<OrderDTO>.PrintItems(allOrders);
        var indexes = allOrders.Select((x, i) => i + 1).ToList() ?? new List<int>();
        int chosenOrderIndex = ValueSeeker.AskForInteger("Choose which order you want to modify", indexes);
        var chosenOrder = allOrders[chosenOrderIndex];
        var newState = chosenOrder.State == "ordered" ? "paid" : "ordered";
        var result = _orderServices.UpdateState(chosenOrder, newState);
        Console.WriteLine(result ? "Order Successfully Modified" : "Order Unsuccessfully Modified");
    }

    private void PrintOrders(string state)
    {
        var orders = _orderServices.GetAll(state);
        ItemsLogger<OrderDTO>.PrintItems(orders);
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