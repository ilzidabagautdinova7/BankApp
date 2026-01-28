namespace BankApp.Models
{
    public class CreditAccount : BankAccount
    {
        public decimal CreditLimit { get; }

        public CreditAccount(string accountNumber, string accountName, decimal initialBalance, decimal creditLimit)
            : base(accountNumber, accountName, initialBalance)
        {
            if (creditLimit < 0)
                throw new ArgumentException("Кредитный лимит не может быть отрицательным");
            CreditLimit = creditLimit;
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Сумма снятия должна быть положительной");

            decimal available = Balance + CreditLimit;
            if (amount > available)
                throw new InvalidOperationException("Превышен кредитный лимит");

            Balance -= amount;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Кредитный счёт: {AccountNumber}");
            Console.WriteLine($"Владелец: {AccountName}");
            Console.WriteLine($"Текущий баланс: {Balance:C}");
            Console.WriteLine($"Кредитный лимит: {CreditLimit:C}");
            Console.WriteLine($"Доступно: {(Balance + CreditLimit):C}");
        }
    }
}