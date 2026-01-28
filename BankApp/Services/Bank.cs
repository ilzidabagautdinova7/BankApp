using BankApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Services
{
    public class Bank
    {

        private List<BankAccount> _accounts = new List<BankAccount>();

        public void AddAccount(BankAccount account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));
            _accounts.Add(account);
        }

        public BankAccount FindAccount(string accountNumber)
        {
            return _accounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber)!;
        }

        public List<BankAccount> GetAllAccounts()
        {
            return new List<BankAccount>(_accounts);
        }

        public bool DepositToAccount(string accountNumber, decimal amount)
        {
            var account = FindAccount(accountNumber);
            if (account == null) return false;

            account.Deposit(amount);
            return true;
        }

        public bool WithdrawFromAccount(string accountNumber, decimal amount)
        {
            var account = FindAccount(accountNumber);
            if (account == null) return false;

            try
            {
                account.Withdraw(amount);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        public List<BankAccount> GetAccountsByOwner(string accountName)
        {
            return _accounts
                .Where(acc => acc.AccountName.Equals(accountName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<SavingsAccount> GetSavingsAccounts()
        {
            return _accounts.OfType<SavingsAccount>().ToList();
        }

        public List<CreditAccount> GetCreditAccounts()
        {
            return _accounts.OfType<CreditAccount>().ToList();
        }
    }
}

   