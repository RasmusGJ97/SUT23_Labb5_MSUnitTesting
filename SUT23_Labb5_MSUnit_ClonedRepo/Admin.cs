using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

namespace Group_Project_Loop_Legends
{
    public class Admin : User
    {
        public Admin(string _name, string _password) : base(_name, _password)
        {

        }
        public void Menu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Welcome, {_name}! Choose an option:");
            Console.ResetColor();
            Console.WriteLine("    Set New Currency");
            Console.WriteLine("    Create New Customer");
            Console.WriteLine("    See Total Assets");
            Console.WriteLine("    See All Transactions");
            Console.WriteLine("    Log Out");

            int cursorPos = 1;

            Console.SetCursorPosition(0, cursorPos);
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("-->");
            Console.ResetColor();
            ConsoleKeyInfo navigator;

            do
            {
                navigator = Console.ReadKey();
                Console.SetCursorPosition(0, cursorPos);
                Console.Write("   ");

                if (navigator.Key == ConsoleKey.UpArrow && cursorPos > 1)
                {
                    cursorPos--;
                }

                else if (navigator.Key == ConsoleKey.DownArrow && cursorPos < 5)
                {
                    cursorPos++;
                }

                Console.SetCursorPosition(0, cursorPos);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("-->");
                Console.ResetColor();
            } while (navigator.Key != ConsoleKey.Enter);

            Console.Clear();
            switch (cursorPos)
            {
                case 1:
                    SetNewCurrency();
                    break;
                case 2:
                    Console.WriteLine("Enter a username");
                    Console.Write(": ");
                    string userName = Console.ReadLine();
                    while (userName.Length > 30 || userName.Length < 1)
                    {
                        Console.WriteLine("Please enter a name shorter than 31 characters and longer than 0 characters");
                        userName = Console.ReadLine();
                    }
                    Console.WriteLine("Enter a password");
                    Console.Write(": ");
                    string userPassword = Console.ReadLine();
                    while (userPassword.Length > 30 || userPassword.Length < 3)
                    {
                        Console.WriteLine("Please enter a password shorter than 31 characters and longer than 3 characters");
                        userPassword = Console.ReadLine();
                    }
                    Console.WriteLine("Confirm password");
                    Console.Write(": ");
                    string confirmPassword = Console.ReadLine();

                    if (userPassword == confirmPassword)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"The customer account for {userName} has been successfully created!");
                        Console.ResetColor();
                        Console.WriteLine("\nPress Enter to return to the menu");
                        Console.ReadKey();

                        UserManager.AddCustomer(userName, userPassword);
                        Menu();
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPasswords do not match, try again.");
                        Console.ResetColor();
                        Console.WriteLine("\nPress Enter to return to the menu");
                        Console.ReadKey();
                        Menu();
                    }
                    break;
                case 3:
                    ShowBankAssets();
                    Menu();
                    break;
                case 4:
                    SeeAllTransactions();
                    Menu();
                    break;
                case 5:
                    LogOut();
                    break;
            }

        }
        public void SetNewCurrency()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Choose the currency to update: ");
            Console.WriteLine("\n   CURRENCY    EXCHANGE RATE");
            Console.ResetColor();
            Console.WriteLine($"    SEK:        {CurrencyConverter._sekRate}");
            Console.WriteLine($"    USD:        {CurrencyConverter._usdRate}");
            Console.WriteLine($"    EURO:       {CurrencyConverter._euroRate}");
            Console.WriteLine($"    GBP:        {CurrencyConverter._gbpRate}");
            Console.WriteLine($"    JPY:        {CurrencyConverter._jpyRate}");

            int cursorPos = 3;

            Console.SetCursorPosition(0, cursorPos);
            Console.CursorVisible = false;
            Console.Write("-->");
            ConsoleKeyInfo navigator;

            do
            {
                navigator = Console.ReadKey();
                Console.SetCursorPosition(0, cursorPos);
                Console.Write("   ");

                if (navigator.Key == ConsoleKey.UpArrow && cursorPos > 3)
                {
                    cursorPos--;
                }

                else if (navigator.Key == ConsoleKey.DownArrow && cursorPos < 7)
                {
                    cursorPos++;
                }

                Console.SetCursorPosition(0, cursorPos);
                Console.Write("-->");
            } while (navigator.Key != ConsoleKey.Enter);

            string currency = "";

            double currentRate = 0.0;

            switch (cursorPos)
            {
                case 3:
                    currency = "SEK";
                    currentRate = CurrencyConverter._sekRate;
                    break;
                case 4:
                    currency = "USD";
                    currentRate = CurrencyConverter._usdRate;
                    break;
                case 5:
                    currency = "EURO";
                    currentRate = CurrencyConverter._euroRate;
                    break;
                case 6:
                    currency = "GBP";
                    currentRate = CurrencyConverter._gbpRate;
                    break;
                case 7:
                    currency = "JPY";
                    currentRate = CurrencyConverter._jpyRate;
                    break;
                default:
                    Console.WriteLine($"Currency not supported: {cursorPos}");
                    Console.WriteLine("\nPress Enter to return to the menu");
                    Console.ReadKey();
                    Menu();
                    return;
            }

            Console.Clear();
            Console.WriteLine($"\nCurrent exchange rate for {currency}: {currentRate}");
            Console.WriteLine("\nEnter the new exchange rate");
            Console.Write(": ");

            double newRate;
            while (!double.TryParse(Console.ReadLine(), out newRate))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the new rate:");
            }

            switch (currency)
            {
                case "SEK":
                    CurrencyConverter._sekRate = newRate;
                    break;
                case "USD":
                    CurrencyConverter._usdRate = newRate;
                    break;
                case "EURO":
                    CurrencyConverter._euroRate = newRate;
                    break;
                case "GBP":
                    CurrencyConverter._gbpRate = newRate;
                    break;
                case "JPY":
                    CurrencyConverter._jpyRate = newRate;
                    break;
            }

            Console.WriteLine($"\nExchange rate for {currency} has been updated from {currentRate} to {newRate}");
            Console.WriteLine("\nPress Enter to return to the menu");
            Console.ReadKey();
            Menu();
        }

        public static void ShowBankAssets()
        {
            double sumOfAllAccounts = 0;
            double sumOfLoans = 0;

            foreach (Customer customer in UserManager.customerList)
            {
                sumOfAllAccounts += CurrencyConverter.TotalAsset(customer.AccountList);
            }

            foreach (Customer customer in UserManager.customerList)
            {
                sumOfLoans += customer.Credit;
            }

            Console.WriteLine($"Total balance of all accounts: {sumOfAllAccounts:N2} SEK \nSum of all loans: {sumOfLoans:N2} SEK \nBank's total assets: {sumOfAllAccounts - sumOfLoans:N2} SEK");

            Console.WriteLine("\nPress Enter to return to Menu");
            Console.ReadKey();
        }

        public static void SeeAllTransactions()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("All Transactions and Loans");
            Console.ResetColor();

            foreach (Customer customer in UserManager.customerList)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nTransactions and Loans for {customer._name}:");
                Console.ResetColor();
                foreach (string transaction in customer._historyList)
                {
                    Console.WriteLine(transaction);
                    Console.WriteLine("-");
                }
            }

            Console.WriteLine("\nPress Enter to return to Menu");
            Console.ReadKey();


        }

        public static void LogOut()
        {
            Console.WriteLine("Logging out...");
            Thread.Sleep(1500);
            Console.Clear();
        }
    }
}