using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public abstract class BankAccount
    {
        public string AccountNumber { get; }
        public string AccountName { get; }

        protected BankAccount(string accountNumber, string accountName)
        {
            AccountNumber = accountNumber;
            AccountName = accountName;
        }
        public abstract void DisplayInfo();
    }
}
