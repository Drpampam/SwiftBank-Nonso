using Swyft.Core.Authentication;
using Swyft.Core.Data;
using Swyft.Core.Interfaces;
using Swyft.Core.Models;
using Swyft.Core.Services;
using Swyft.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Swyft.UI
{
    public class AccountView : IAccountView
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public AccountView(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public void DisplayAccountMenu()
        {
            Print.PrintLogo();
            WriteLine("Select an option to continue:");
            WriteLine("\t1. View Accounts\n\t2. Create new Savings or Current account\n\t3. Logout");
            Write("==> ");
            string answer= ReadLine();

            var user = Auth.CurrentUser;

            if (answer == "1")
            {
                DisplayViewAccountMenu(user);
            }
            else if (answer == "2")
            {
                DisplayCreateAccountMenu(user);
            }else if (answer == "3")
            {
                Auth.Logout();
            }
        }

        public void DisplayCreateAccountMenu(User user)
        {
            WriteLine("Select account type:");
            WriteLine("\t1. Savings\n\t2. Current\n");
            Write("==> ");
            string answer = ReadLine();

            if (answer == "1" || answer == "2")
            {
                WriteLine("Creating account. Please wait ..."); // pause for dramatic effect
                Thread.Sleep(1500);

                _accountService.Create(answer);
                var account = _accountService.Get(AccountService.IdCount);

                WriteLine("Account successfully created. Your account details are:");
                WriteLine($"\t- Account Name: {account.AccountName}\n\t- Account Number: {account.AccountNumber} \n\t- Account Type: {account.Type}");
                Write("Press Enter to continue: ");
                ReadLine();

                DisplayAccountMenu();
            }
        }

        public void DisplayViewAccountMenu(User user)
        {
            var accounts = _accountService.GetAllUserAccounts(user.Id);
            Print.PrintAccountDetails(accounts);

            Write("Select an account to continue: ");
            var answer = ReadLine();
            int.TryParse(answer, out int num);

            if (num > 0 && num <= accounts.Count)
            {
                var account = accounts[num-1];
                DisplaySingleAccount(account);
            }

        }

        public void DisplaySingleAccount(Account account)
        {
            Print.PrintLogo();
            WriteLine($"\t- Account Name: {account.AccountName}\t- Account Number: {account.AccountNumber} \t- Account Type: {account.Type}");
            WriteLine("Select an action to continue:");
            WriteLine("\t1. Deposit\t2. Withdraw\n\t3. Transfer\t4. Request Statement\n\t5. Get Balance\t6. Main Menu");
            Write("==> ");
            var answer = ReadLine();

            if (answer == "1")
            {
                DisplayDepositMenu(account);
            }
            else if (answer == "2")
            {
                DisplayWithdrawalMenu(account);
            }
            else if (answer == "3")
            {
                DisplayTransferMenu(account);
            }else if (answer == "4")
            {
                DisplayAccountStatement(account);
            }else if (answer == "5")
            {
                DisplayAccountBalance(account);
            }else if (answer == "6")
            {
                DisplayAccountMenu();
            }
        }

        public void DisplayDepositMenu(Account account)
        {
            Write("Amount to deposit: ");
            var answer = ReadLine();

            if (decimal.TryParse(answer, out decimal amount))
            {
                try
                {
                    _accountService.Deposit(amount, account.Id);

                    WriteLine("Deposit transaction successful");
                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                }
            }

            Write("Press Enter to continue: ");
            ReadLine();
            DisplaySingleAccount(account);
        }

        public void DisplayWithdrawalMenu(Account account)
        {
            Write("Amount to withdraw: ");
            var answer = ReadLine();

            if (decimal.TryParse(answer, out decimal amount))
            {
                try
                {
                    _accountService.Withdraw(amount, account.Id);
                    WriteLine("Withdrawal transaction successful");
                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                }
            }

            Write("Press Enter to continue: ");
            ReadLine();
            DisplaySingleAccount(account);
        }

        public void DisplayTransferMenu(Account account)
        {
            Write("Enter amount: ");
            var answer = ReadLine();

            if(!decimal.TryParse(answer, out decimal amount))
            {
                WriteLine("Invalid input");
                DisplaySingleAccount(account);
            }

            Write("Enter destination account: ");
            var answer2 = ReadLine();
            var destinationAccount= Validate.CheckAccountExists(answer2, out string message);

            if(destinationAccount == null)
            {
                WriteLine(message);
                Write("Press Enter to continue: ");
                ReadLine();

                DisplaySingleAccount(account);
            }
            else
            {
                Write($"Enter your 4 digit PIN to transfer {amount} to [ {destinationAccount.AccountName}, {destinationAccount.AccountNumber}, Swyft Bank ]: ");
                var answer3 = ReadLine();

                if (answer3 == Auth.CurrentUser.Pin)
                {
                    try
                    {
                        _accountService.Transfer(amount, account.Id, destinationAccount.Id);
                        WriteLine("Transfer transaction successful");
                    }
                    catch (Exception e)
                    {
                        WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid transaction PIN");
                }

                Write("Press Enter to continue: ");
                ReadLine();
                DisplaySingleAccount(account);
            }
        }

        public void DisplayAccountStatement(Account account)
        {
            Print.PrintLogo();

            var transactions = _transactionService.GetAllAccountTransactions(account.Id);

            Print.PrintAccountStatement(account, transactions);

            Write("Press Enter to continue: ");
            ReadLine();

            DisplaySingleAccount(account);
        }

        public void DisplayAccountBalance(Account account)
        {
            WriteLine($"Available balance for account {account.AccountNumber}: {account.Balance:N2}");

            Write("Press Enter to continue: ");
            ReadLine();

            DisplaySingleAccount(account);
        }
    }
}
