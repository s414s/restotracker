using Retrotracker.Application;

namespace Retrotracker.Presentation;
public class ConsoleLogger
{
    private readonly IUserServices _userServices;
    private readonly IOrderServices _orderrServices;
    private readonly IDishServices _dishServices;
    private readonly List<string> optionNames = new List<string>(){
                {"1. Sign In"},
                {"2. Create User"},
                {"3. Exit"},
        };

    private bool exit = false;
    private UserDTO? activeUser = null;

    public void Run()
    {
        AuthenticateUser();
        if (exit) { return; }

        do
        {
            PrintOptions();
            // AskForOption();
        }
        while (!exit);
    }

    public void AuthenticateUser()
    {
        Console.WriteLine("Introduce your account number");

        while (true)
        {
            int inputAccount = AskForInteger("Introduce your account number", 1);
            string inputPassword = AskForString("Introduce your password");
            (UserDTO activeUser, string error) = _accountService.AuthenticateUser(inputAccount, inputPassword);
            if (error == null)
            {
                this.activeUser = activeUser;
                break;
            }
            Console.WriteLine(error);
            Console.WriteLine("Try again");
        }
        Console.WriteLine("You reached the maximum number of attempts. Try latter on.");
        exit = true;
    }

    public int AskForInteger(string consoleText, int minimumValue)
    {
        Console.WriteLine($"{consoleText}. It must be an integer greater or equal to {minimumValue}.");
        while (true)
        {
            (int validatedInput, string error) = new InputValidator().ParseInteger(Console.ReadLine(), minimumValue);
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

    public decimal AskForDecimal(string consoleText, int minimumValue)
    {
        Console.WriteLine($"{consoleText}. It must be a decimal number greater or equal to {minimumValue}.");
        while (true)
        {
            (decimal validatedInput, string error) = new InputValidator().ParseDecimal(Console.ReadLine(), minimumValue);

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

    public string AskForString(string consoleText)
    {
        Console.WriteLine($"{consoleText}. It must be a valid string");
        while (true)
        {
            (string validatedInput, string error) = new InputValidator().ParseString(Console.ReadLine());

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
        Console.WriteLine("Logging you out...");
    }
}