using System;
using BankApp.Models;
using BankApp.Services;

namespace BankApp;

class Program
{
    private static Bank _bank = new Bank();

    static void Main(string[] args)
    {
        Console.WriteLine("Добро пожаловать в BankApp!");
        ShowMenu();
    }

    static void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Меню ---");
            Console.WriteLine("1. Создать счёт");
            Console.WriteLine("2. Пополнить счёт");
            Console.WriteLine("3. Снять деньги");
            Console.WriteLine("4. Показать информацию о счёте");
            Console.WriteLine("5. Показать все счета");
            Console.WriteLine("6. Выйти");
            Console.Write("Выберите действие (1–6): ");

            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 6)
            {
                Console.WriteLine("Ошибка: введите число от 1 до 6.");
                continue;
            }

            switch (choice)
            {
                case 1: CreateAccount(); break;
                case 2: Deposit(); break;
                case 3: Withdraw(); break;
                case 4: ShowAccountInfo(); break;
                case 5: ShowAllAccounts(); break;
                case 6:
                    Console.WriteLine("До свидания!");
                    return;
            }
        }
    }

    static void CreateAccount()
    {
        Console.Write("Номер счёта: ");
        string accountNumber = Console.ReadLine()!;

        Console.Write("Владелец: ");
        string accountName = Console.ReadLine()!;

        Console.Write("Начальный баланс: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal initialBalance) || initialBalance < 0)
        {
            Console.WriteLine("Ошибка: баланс должен быть неотрицательным.");
            return;
        }

        Console.Write("Тип счёта (1 - обычный, 2 - сберегательный): ");
        if (!int.TryParse(Console.ReadLine(), out int type) || type < 1 || type > 2)
        {
            Console.WriteLine("Ошибка: выберите тип 1 или 2.");
            return;
        }

        try
        {
            BankAccount account;
            switch (type)
            {
                case 1:
                    account = new RegularBankAccount(accountNumber, accountName, initialBalance);
                    break;
                case 2:
                    Console.Write("Процентная ставка (например, 0.05 для 5%): ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal interestRate) || interestRate < 0)
                    {
                        Console.WriteLine("Ошибка: ставка должна быть неотрицательной.");
                        return;
                    }
                    account = new SavingsAccount(accountNumber, accountName, initialBalance, interestRate);
                    break;
                default:
                    Console.WriteLine("Неизвестный тип счёта.");
                    return;
            }

            _bank.AddAccount(account);
            Console.WriteLine("Счёт успешно создан!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void Deposit()
    {
        Console.Write("Номер счёта: ");
        string accountNumber = Console.ReadLine()!;

        Console.Write("Сумма пополнения: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
        {
            Console.WriteLine("Ошибка: сумма должна быть положительной.");
            return;
        }

        if (_bank.DepositToAccount(accountNumber, amount))
            Console.WriteLine("Пополнение выполнено!");
        else
            Console.WriteLine("Ошибка: счёт не найден.");
    }

    static void Withdraw()
    {
        Console.Write("Номер счёта: ");
        string accountNumber = Console.ReadLine()!;

        Console.Write("Сумма снятия: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
        {
            Console.WriteLine("Ошибка: сумма должна быть положительной.");
            return;
        }

        if (_bank.WithdrawFromAccount(accountNumber, amount))
            Console.WriteLine("Снятие выполнено!");
        else
            Console.WriteLine("Ошибка: недостаточно средств или счёт не найден.");
    }

    static void ShowAccountInfo()
    {
        Console.Write("Номер счёта: ");
        string accountNumber = Console.ReadLine()!;

        var account = _bank.FindAccount(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Ошибка: счёт не найден.");
            return;
        }

        account.DisplayInfo();
    }

    static void ShowAllAccounts()
    {
        var accounts = _bank.GetAllAccounts();
        if (accounts.Count == 0)
        {
            Console.WriteLine("Счетов пока нет.");
            return;
        }

        Console.WriteLine("\nВсе счета:");
        foreach (var account in accounts)
        {
            account.DisplayInfo();
            Console.WriteLine("-".PadLeft(40, '-'));
        }
    }
}