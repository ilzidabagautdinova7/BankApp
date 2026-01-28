using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public abstract class BankAccount
    {
        private decimal _balance;
        public string AccountNumber { get; private set; }
        public string AccountName { get; private set; }

        protected BankAccount(string accountNumber, string accountName , decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Номер счета не может быть пустым");
            if (string.IsNullOrWhiteSpace(accountName))
                throw new ArgumentException("Имя владельца не может быть пусты");
            if (initialBalance < 0)
                throw new ArgumentException("Начальный баланс не может быть отрицательным");

            AccountName = accountName;
            AccountNumber = accountNumber;
            _balance = initialBalance;
        }

        public decimal Balance
        {
            get => _balance;
            protected set
            {
                if (value < 0)
                    throw new InvalidOperationException("Баланс не может быть отрицательным");
                _balance = value;
            }
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Сумма пополнения дллжна быть положителбной");
            Balance += amount;
        }
        public abstract void Withdraw(decimal amount);
        public abstract void DisplayInfo();
    }

}

