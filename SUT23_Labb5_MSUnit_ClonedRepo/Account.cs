using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Loop_Legends
{
    public class Account
    {
        private double _balance;
        private string _currency;
        private string _accountName;
        private string _userName;
        public Account(double balance, string currency, string accountName, string userName)
        {
            this._balance = balance;
            this._currency = currency;
            this._accountName = accountName;
            this._userName = userName;
        }
        public void PrintAccounts()
        {
            Console.WriteLine($"{_accountName,-25}{_balance,-15:N2}{_currency}");
        }

        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
        public string AccountName
        {
            get { return _accountName; }
        }
    }
}
