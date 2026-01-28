using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class SavingsAccount : BankAccount
    {
        public decimal InterestRate {get ;set;}

        public SavingsAccount(string accountNumber,string accountName, decimal initialBalance, decimal interestRate)
            : base(accountNumber,accountName, initialBalance)
        {
            InterestRate = interestRate;
        }
        

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
            decimal interestBalance = Balance * (1 + InterestRate);
            Console.WriteLine($"Сберегательный счёт: {AccountNumber}");
            Console.WriteLine($"Владелец: {AccountName}");
            Console.WriteLine($"Текущий баланс: {Balance:C}");
            Console.WriteLine($"С учётом процентов: {interestBalance:C}");
        }
    }

}
    