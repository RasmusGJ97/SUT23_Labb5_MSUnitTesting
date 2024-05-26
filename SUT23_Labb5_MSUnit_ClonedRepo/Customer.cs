using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Group_Project_Loop_Legends
{
    public class Customer : User
    {
        public List<Account> _accountList = new List<Account>();
        public List<string> _historyList = new List<string>();
        private double _credit = 0;
        public string authenticator = "";
        public string authQuestion = "";
        public double warningBalance = 0;

        // Standardkonstruktor utan användarnamn och lösenord
        public Customer() : base("", "")
        {
        }
        public Customer(string _name, string _password) : base(_name, _password)
        {
        }

        public void Menu(List<Customer> customerList)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Welcome, {_name}! Choose an option:");
                Console.ResetColor();

                Console.WriteLine("    See Your Accounts");
                Console.WriteLine("    Create New Account");
                Console.WriteLine("    Show Account History");
                Console.WriteLine("    Transfer Money");
                Console.WriteLine("    Loan Money");
                Console.WriteLine("    Settings");
                Console.WriteLine("    Log Out");

                Console.ForegroundColor = ConsoleColor.Red;
                string warningCheck = WarningMessageCheck(_accountList);
                Console.WriteLine("\n" + warningCheck);
                Console.ResetColor();

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

                    else if (navigator.Key == ConsoleKey.DownArrow && cursorPos < 7)
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
                        SeeAccounts();
                        break;
                    case 2:
                        AddAccount();
                        break;
                    case 3:
                        AccountHistory();
                        break;
                    case 4:
                        TransferMoney(customerList, _accountList, _historyList);
                        break;
                    case 5:
                        LoanMoney(_accountList, _credit, _historyList);
                        break;
                    case 6:
                        Settings();
                        break;
                    case 7:
                        LogOut(); isRunning = false;
                        break;
                    default:
                        throw new ArgumentException($"Menu option {cursorPos} not available");


                }
            }
        }


        ///////////////////////////////// 1. See Your Accounts //////////////////////////////////////////////////////


        public void SeeAccounts()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Account Name             Balance        Currency\n");
            Console.ResetColor();
            foreach (Account item in _accountList)
            {
                item.PrintAccounts();
            }
            Console.WriteLine("\nPress Enter to return to Menu");
            Console.ReadLine();
        }



        ////////////////////////////////// 2. Create New Account /////////////////////////////////////////////////////


        public void AddAccount()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Name of the account (max 23 characters):");
            Console.ResetColor();
            Console.CursorVisible = true;
            string accountName = Console.ReadLine();

            while (accountName.Length > 23 || accountName.Length < 1)
            {
                Console.WriteLine("Please Enter a name shorter than 24 characters and longer than zero characters");
                accountName = Console.ReadLine();
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("What currency would you like your account to have?");
            Console.ResetColor();
            Console.WriteLine("\n     SEK\n     USD\n     EURO\n     GBP\n     JPY\n");

            int cursorPosition = 2;

            Console.SetCursorPosition(0, cursorPosition);
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("-->");
            Console.ResetColor();
            ConsoleKeyInfo navigator;

            do
            {
                navigator = Console.ReadKey();
                Console.SetCursorPosition(0, cursorPosition);
                Console.Write("   ");

                if (navigator.Key == ConsoleKey.UpArrow && cursorPosition > 2)
                {
                    cursorPosition--;
                }

                else if (navigator.Key == ConsoleKey.DownArrow && cursorPosition < 6)
                {
                    cursorPosition++;
                }

                Console.SetCursorPosition(0, cursorPosition);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("-->");
                Console.ResetColor();
            } while (navigator.Key != ConsoleKey.Enter);
            Console.Clear();

            string currency;
            switch (cursorPosition)
            {
                case 2:
                    currency = "SEK";
                    break;
                case 3:
                    currency = "USD";
                    break;
                case 4:
                    currency = "EURO";
                    break;
                case 5:
                    currency = "GBP";
                    break;
                case 6:
                    currency = "JPY";
                    break;
                default:
                    throw new ArgumentException($"Menu option {cursorPosition} not available");
            }

            Account newAccount = new Account(0, currency, accountName, _name);

            _accountList.Add(newAccount);
        }


        ///////////////////////////////////////// 3. Show Account History ///////////////////////////////////////////


        public void AccountHistory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("SUM                ACTION               ACCOUNT                 BALANCE\n");
            Console.ResetColor();
            foreach (string transaction in _historyList)
            {
                Console.WriteLine(transaction);
                Console.WriteLine("-");
            }
            Console.WriteLine("\nPress Enter to return to Menu");
            Console.ReadLine();
        }



        ////////////////////////////////////////// 4. Transfer Money ////////////////////////////////////////////////


        public void TransferMoney(List<Customer> customerList, List<Account> accounts, List<string> historyList)
        {
            // Print menu of this users account to transfer from - only accounts with balance > 0 are visible

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Select an account to transfer money FROM");
            Console.ResetColor();

            List<Account> temporaryAccounts = new List<Account>();

            foreach (Account account in accounts)
            {
                if (account.Balance > 0)
                {
                    Console.WriteLine($"     {account.AccountName}");

                    // Put available accounts in a new list with correct index according to user selection
                    temporaryAccounts.Add(account);
                }
            }

            // Return the user to customer meny if no accounts have any balance > 0
            if (temporaryAccounts.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("You do not have any money! Returning to menu...");
                Thread.Sleep(5000);
            }

            else
            {
                // Let user select account from menu
                int fromAccountPosition = 1;
                Console.SetCursorPosition(0, fromAccountPosition);
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("-->");
                Console.ResetColor();
                ConsoleKeyInfo navigator;

                do
                {
                    navigator = Console.ReadKey();
                    Console.SetCursorPosition(0, fromAccountPosition);
                    Console.Write("   ");

                    if (navigator.Key == ConsoleKey.UpArrow && fromAccountPosition > 1)
                    {
                        fromAccountPosition--;
                    }

                    else if (navigator.Key == ConsoleKey.DownArrow && fromAccountPosition < temporaryAccounts.Count)
                    {
                        fromAccountPosition++;
                    }

                    Console.SetCursorPosition(0, fromAccountPosition);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("-->");
                    Console.ResetColor();
                } while (navigator.Key != ConsoleKey.Enter);
                Console.Clear();

                // Print menu with available customers

                List<Customer> recipients = new List<Customer>();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Select recipient");
                Console.ResetColor();

                foreach (Customer customer in customerList)
                {
                    if (customer._accountList.Count > 0)
                    {
                        Console.WriteLine($"     {customer._name}");
                        recipients.Add(customer);
                    }
                }

                // Let user select recipient/customer

                int cursorPosition = 1;
                Console.SetCursorPosition(0, cursorPosition);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("-->");
                Console.ResetColor();

                do
                {
                    navigator = Console.ReadKey();
                    Console.SetCursorPosition(0, cursorPosition);
                    Console.Write("   ");

                    if (navigator.Key == ConsoleKey.UpArrow && cursorPosition > 1)
                    {
                        cursorPosition--;
                    }

                    else if (navigator.Key == ConsoleKey.DownArrow && cursorPosition < recipients.Count)

                    {
                        cursorPosition++;
                    }

                    Console.SetCursorPosition(0, cursorPosition);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("-->");
                    Console.ResetColor();
                } while (navigator.Key != ConsoleKey.Enter);
                Console.Clear();

                // Print recipient's available accounts
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Select an account to transfer money TO");
                Console.ResetColor();

                foreach (Account account in recipients[cursorPosition - 1]._accountList)
                {
                    Console.WriteLine($"     {account.AccountName}");
                }

                // Let user select an account
                int toAccountPosition = 1;
                Console.SetCursorPosition(0, toAccountPosition);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("-->");
                Console.ResetColor();

                do
                {
                    navigator = Console.ReadKey();
                    Console.SetCursorPosition(0, toAccountPosition);
                    Console.Write("   ");

                    if (navigator.Key == ConsoleKey.UpArrow && toAccountPosition > 1)
                    {
                        toAccountPosition--;
                    }

                    else if (navigator.Key == ConsoleKey.DownArrow && toAccountPosition < recipients[cursorPosition - 1]._accountList.Count)
                    {
                        toAccountPosition++;
                    }

                    Console.SetCursorPosition(0, toAccountPosition);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("-->");
                    Console.ResetColor();
                } while (navigator.Key != ConsoleKey.Enter);

                // Let user enter amount to transfer
                double amountToTransfer;
                do
                {
                    Console.Clear();
                    Console.CursorVisible = true;
                    Console.WriteLine($"Currently there are no currency conversion fee\n\nHow much money would you like to transfer? (Enter value in {temporaryAccounts[fromAccountPosition - 1].Currency})");

                    while (!double.TryParse(Console.ReadLine(), out amountToTransfer))
                    {
                        Console.Clear();
                        Console.CursorVisible = false;
                        Console.WriteLine("Incorrect value");
                        Thread.Sleep(1500);
                        Console.Clear();
                        Console.CursorVisible = true;
                        Console.WriteLine($"Currently there are no currency conversion fee\n\nHow much money would you like to transfer? (Enter value in {temporaryAccounts[fromAccountPosition - 1].Currency})");
                    }

                    Console.CursorVisible = false;

                    if (amountToTransfer < 0)
                    {
                        Console.WriteLine("Can not tranfer a negativ amount!\nPlease enter a positive value");
                        Thread.Sleep(3000);
                    }

                    // Check if there is enough balance in account to transfer from - Prints message if not
                    else if (temporaryAccounts[fromAccountPosition - 1].Balance - amountToTransfer < 0)
                    {
                        Console.WriteLine("The amount exceeds current balance!\nPlease enter a lower amount");
                        Thread.Sleep(3000);
                    }

                } while (temporaryAccounts[fromAccountPosition - 1].Balance - amountToTransfer < 0 || amountToTransfer < 0);

                // Convert the amount to correct currency
                double amountInCorrectCurrency = CurrencyConverter.ConvertCurrency(temporaryAccounts[fromAccountPosition - 1], recipients[cursorPosition - 1]._accountList[toAccountPosition - 1], amountToTransfer);

                // Find correct index on the account where money is withdrawn from
                int fromAccountIndex = accounts.IndexOf(temporaryAccounts[fromAccountPosition - 1]);

                // Find index of recipient in customerList
                int customerIndex = customerList.IndexOf(recipients[cursorPosition - 1]);

                Console.Clear();
                Console.WriteLine($"{amountToTransfer} {accounts[fromAccountIndex].Currency} will be transferred from {accounts[fromAccountIndex].AccountName} to {customerList[customerIndex]._accountList[toAccountPosition - 1].AccountName}");

                Console.WriteLine("\nPress Enter to return to Menu.");
                Console.ReadLine();

                // Send transaction to method that completes the transaction at right time

                Task.Run(() => HandleTransaction(accounts[fromAccountIndex], customerList[customerIndex]._accountList[toAccountPosition - 1], amountToTransfer, amountInCorrectCurrency, historyList, customerList[cursorPosition - 1]._historyList));
            }
        }


        public void HandleTransaction(Account withdrawAccount, Account depositAccount, double withdrawAmount, double depositAmount, List<string> senderHistory, List<string> receiverHistory)
        {
            bool doTransaction = false;

            while (doTransaction == false)
            {

                DateTime minutes = DateTime.Now;

                if (minutes.Minute % 1 == 0)
                {
                    if (withdrawAccount.Balance - withdrawAmount >= 0)
                    {
                        withdrawAccount.Balance -= withdrawAmount;
                        depositAccount.Balance += depositAmount;

                        senderHistory.Add($"{withdrawAccount.Currency} {withdrawAmount,-15:N2}withdrawn from       {withdrawAccount.AccountName,-23} {withdrawAccount.Currency} {withdrawAccount.Balance:N2}");
                        receiverHistory.Add($"{depositAccount.Currency} {depositAmount,-15:N2}deposited to         {depositAccount.AccountName,-23} {depositAccount.Currency} {depositAccount.Balance:N2}");
                    }

                    else
                    {
                        senderHistory.Add($"{withdrawAccount.Currency} {withdrawAmount,-14:N2} canceled due to not enough balance");
                    }
                    doTransaction = true;
                }
                Thread.Sleep(1000);
            }
        }


        ////////////////////////////////////////////// 5. Loan Money //////////////////////////////////////////////


        public void LoanMoney(List<Account> accountList, double credit, List<string> historyList)
        {
            Console.CursorVisible = true;
            ConsoleKeyInfo navigator;
            double wantLoan = 0;
            double totalAssetInSEK = CurrencyConverter.TotalAsset(accountList);
            double maxLoan = (totalAssetInSEK - credit) * 5 - credit;

            if (maxLoan > 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Based on your total assets ({totalAssetInSEK:N2} SEK) and your credit ({credit} SEK) you can loan up to {maxLoan:N2} SEK.");
                Console.ResetColor();

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\nHow much do you want to loan? ");
                    Console.ResetColor();
                    try
                    {
                        wantLoan = Convert.ToDouble(Console.ReadLine());
                        if (wantLoan >= 0)
                            break;
                        else
                            Console.WriteLine("You can not loan a negative amount.");
                    }
                    catch
                    {
                        Console.WriteLine("\nInvalid input. Try again.");
                    }
                }
                while (wantLoan > maxLoan)
                {
                    Console.WriteLine("\nYou can not loan that much.");
                    Console.Write("Request a lower amount: ");
                    try
                    {
                        wantLoan = Convert.ToDouble(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input. Try again.\n");
                    }
                }

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"We currently have an interest rate of 4,9%. There will be a yearly fee of {wantLoan * 0.049:N2} SEK for your loan.\n");
                Console.WriteLine("To which account do you want to insert the loan?\n");
                Console.ResetColor();

                foreach (Account account in accountList) // Possible improvement: also print out the balance and currency of the accounts
                {
                    Console.WriteLine($"     {account.AccountName}");
                }
                int cursorPosition = 4;

                Console.SetCursorPosition(0, cursorPosition);
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("-->");
                Console.ResetColor();

                do
                {
                    navigator = Console.ReadKey();
                    Console.SetCursorPosition(0, cursorPosition);
                    Console.Write("   ");

                    if (navigator.Key == ConsoleKey.UpArrow && cursorPosition > 4)
                    {
                        cursorPosition--;
                    }

                    else if (navigator.Key == ConsoleKey.DownArrow && cursorPosition < accountList.Count + 3)
                    {
                        cursorPosition++;
                    }

                    Console.SetCursorPosition(0, cursorPosition);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("-->");
                    Console.ResetColor();
                } while (navigator.Key != ConsoleKey.Enter);

                Console.Clear();
                Credit += wantLoan;
                Account bankLoan = new Account(wantLoan, "SEK", "bankLoanTempAccount", "Loop Legends Bank"); // Had to create a new account to use the ConvertCurrency method.

                double balanceBeforeLoan = accountList[cursorPosition - 4].Balance;
                accountList[cursorPosition - 4].Balance += CurrencyConverter.ConvertCurrency(bankLoan, accountList[cursorPosition - 4], wantLoan); ;

                historyList.Add($"SEK {wantLoan,-15:N2}loaned into          {accountList[cursorPosition - 4].AccountName,-23} {accountList[cursorPosition - 4].Currency} {accountList[cursorPosition - 4].Balance:N2}");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"You have now loaned {wantLoan} SEK and inserted it into {accountList[cursorPosition - 4].AccountName}");
                Console.ResetColor();
                Console.WriteLine($"\nBalance before loan: {balanceBeforeLoan:N2} {accountList[cursorPosition - 4].Currency}");
                Console.WriteLine($"Balance after loan : {accountList[cursorPosition - 4].Balance:N2} {accountList[cursorPosition - 4].Currency}");

            }
            else
            {
                Console.WriteLine("We're sorry to inform you that you can not loan money from us. Either you have 0 total assets with us, or you've already loaned up to the maximum amount.");
            }

            Console.WriteLine("\nPress Enter to return to Menu.");
            Console.ReadLine();
        }

        //////////////////////////////////////////// 6. Settings /////////////////////////////////////////////////

        public void Settings()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Settings");
            Console.ResetColor();
            Console.WriteLine("    Handle warning message");
            Console.WriteLine("    Two-Factor authentication");
            Console.ResetColor();

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

                else if (navigator.Key == ConsoleKey.DownArrow && cursorPos < 2)
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
                    WarningMethod();
                    return;
                case 2:
                    string oldAuth = authenticator;
                    (string auth, string aQuestion) = AuthenticationClass.AuthenticatorMethod(authenticator, authQuestion);
                    authenticator = auth;
                    authQuestion = aQuestion;

                    if (authenticator != oldAuth && authenticator != "")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nAuthenticator successfully updated . . .");
                        Console.ResetColor();
                    }
                    else if (authenticator != oldAuth && authenticator == "")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Authenticator successfully removed . . .");
                        Console.ResetColor();
                    }
                    else if (authenticator == oldAuth)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Authenticator did not update . . .");
                        Console.ResetColor();
                    }
                    Thread.Sleep(2500);
                    return;
            }
            Console.WriteLine("\nPress Enter to return to Menu.");
            Console.ReadLine();
        }

        //////////////////////////////////////////// 7. Log Out /////////////////////////////////////////////////


        public static void LogOut()
        {
            Console.WriteLine("Logging out...");
            Thread.Sleep(1500);
            Console.Clear();
            //Login.LogIn();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public void CreateNewAccount(Account newAccount)
        {
            _accountList.Add(newAccount);
        }
        public double Credit
        {
            get { return _credit; }
            set { _credit = value; }
        }

        public List<string> HistoryList
        {
            get { return _historyList; }
        }

        public List<Account> AccountList
        {
            get { return _accountList; }
        }

        public void WarningMethod()
        {
            if (warningBalance == 0)
            {
                Console.Clear();
                Console.WriteLine("No warning message found\nAt what total balance in SEK do you want your warning message?" +
                    "\n(Enter 0 for no warning message)");
                try
                {
                    warningBalance = Convert.ToDouble(Console.ReadLine());
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please write a number . . .");
                    Console.ResetColor();
                    Thread.Sleep(1500);
                    WarningMethod();
                }
                return;
            }
            else
            {
                Console.WriteLine($"Warning message already exists, it is at {warningBalance} SEK." +
                    "\nDo you want to remove your warning message?");
                Console.WriteLine("    Yes");
                Console.WriteLine("    No");
                Console.ResetColor();

                int cursorPos = 2;


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

                    if (navigator.Key == ConsoleKey.UpArrow && cursorPos > 2)
                    {
                        cursorPos--;
                    }

                    else if (navigator.Key == ConsoleKey.DownArrow && cursorPos < 3)
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
                    case 2:
                        Console.WriteLine("Removing warning message . . .");
                        warningBalance = 0;
                        Thread.Sleep(1500);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Warning message successfully removed . . .");
                        Thread.Sleep(1500);
                        Console.ResetColor();
                        return;
                    case 3:
                        Console.WriteLine("Warning message still in effect, returning to menu . . .");
                        Thread.Sleep(2500);
                        return;
                }
            }
        }
        public string WarningMessageCheck(List<Account> accountList)
        {
            double totalBalance = CurrencyConverter.TotalAsset(accountList);

            if (warningBalance > totalBalance)
            {
                return $"Warning your current balance is below {warningBalance} SEK";
            }
            else
            {
                return "";
            }
        }
    }
}
