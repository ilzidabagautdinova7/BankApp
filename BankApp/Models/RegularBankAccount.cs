using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class RegularBankAccount:BankAccount
    {
        public RegularBankAccount(string accountNumber, string owner, decimal initialBalance)
        : base(accountNumber, owner, initialBalance) { }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Сумма снятия должна быть положительной");
            if (amount > Balance)
                throw new InvalidOperationException("Недостаточно средств на счёте");

            Balance -= amount;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Счёт: {AccountNumber}");
            Console.WriteLine($"Владелец: {AccountName}");
            Console.WriteLine($"Баланс: {Balance:C}");
        }
    }
}

