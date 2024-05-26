using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Loop_Legends
{
    public class UserManager
    {
        public static List<Customer> customerList = new List<Customer>();
        public List<Admin> adminList = new List<Admin>();

        public void CreateInitialUsers()
        {

            Customer c1 = new Customer("Anton", "passwordA");
            Customer c2 = new Customer("Daniel", "passwordD");
            Customer c3 = new Customer("John", "passwordJ");
            Customer c4 = new Customer("Rasmus", "passwordR");

            c4.authenticator = "Varberg";
            c4.authQuestion = "What's the name of your hometown?";

            customerList.Add(c1);
            customerList.Add(c2);
            customerList.Add(c3);
            customerList.Add(c4);

            Account c1a1 = new Account(100, "SEK", "Antons Lönekonto", "Anton");
            Account c1a2 = new Account(100, "USD", "Antons Sparkonto", "Anton");

            c1.CreateNewAccount(c1a1);
            c1.CreateNewAccount(c1a2);

            Account c2a1 = new Account(200, "SEK", "Daniels Lönekonto", "Daniel");
            Account c2a2 = new Account(200, "GBP", "Daniels Sparkonto", "Daniel");

            c2.CreateNewAccount(c2a1);
            c2.CreateNewAccount(c2a2);

            Account c3a1 = new Account(300, "SEK", "Johns Lönekonto", "John");
            Account c3a2 = new Account(300, "USD", "Johns Sparkonto", "John");

            c3.CreateNewAccount(c3a1);
            c3.CreateNewAccount(c3a2);

            Account c4a1 = new Account(400, "SEK", "Rasmus Lönekonto", "Rasmus");
            Account c4a2 = new Account(400, "JPY", "Rasmus Sparkonto", "Rasmus");

            c4.CreateNewAccount(c4a1);
            c4.CreateNewAccount(c4a2);

            Admin a1 = new Admin("Pär", "passwordP");
            Admin a2 = new Admin("Tobias", "passwordT");

            adminList.Add(a1);
            adminList.Add(a2);


            //Utkommenterat för att köra tester

            //bool isRunning = true;
            //while (isRunning)
            //{
            //    Login.LogIn(customerList, adminList);
            //}

        }
        public static void AddCustomer(string username, string password)
        {
            Customer NewC = new Customer(username, password);
            customerList.Add(NewC);
        }
    }
}
