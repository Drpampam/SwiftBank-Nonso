using Swyft.Core.Authentication;
using Swyft.Core.Data;
using Swyft.Core.Interfaces;
using Swyft.Core.Models;
using Swyft.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swyft.Core.Services
{
    public class AccountService : IAccountService
    {
        public static int IdCount { get; set; }

        private readonly ITransactionService _transactionService;

        public AccountService(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        public void Create(string type)
        {
            var user = Auth.CurrentUser;
            IdCount++;
            string accountNumber= "00";
            AccountType accountType = type == "1" ? AccountType.Savings : AccountType.Current;
            var rand= new Random();
            for (int i = 1; i <= 8; i++)
            {
                int num= rand.Next(10);
                accountNumber += num;
            }
            var account = new Account(IdCount, user.FullName, accountNumber, accountType, user.Id);

            DataStore.Accounts.Add(account);
        }

        public void Delete(int id)
        {
            var account = DataStore.Accounts.FirstOrDefault(x => x.Id == id);
            var transactions= DataStore.Transactions.Where(x => x.AccountId == account.Id);
            foreach (var trans in transactions)
            {
                trans.Status = EntityStatus.Inactive;
            }
            account.Status = EntityStatus.Inactive;
        }

        public void Edit(int id)
        {
            throw new NotImplementedException();
        }

        public Account Get(int id)
        {
            return DataStore.Accounts.Where(x => x.Status == EntityStatus.Active).First(x => x.Id == id);
        }

        public void Deposit(decimal amount, int accountId)
        {
            if(amount < 0)
            {
                throw new Exception("Why this?");
            }
            var transType = TransType.Credit;
            var transCategory = TransCategory.Deposit;
            string transDesc = $"Deposit by {Auth.CurrentUser.FullName}";

            _transactionService.Create(amount, accountId, transType, transCategory, transDesc);
        }

        public void Withdraw(decimal amount, int accountId)
        {
            if (amount < 0)
            {
                throw new Exception("Why this?");
            }

            var account = DataStore.Accounts.First(x => x.Id == accountId);

            if (account.Type == AccountType.Savings)
            {
                if (account.Balance - amount < 1000)
                {
                    throw new Exception("You have insufficient funds to complete this transaction.");
                }
            }
            else if (account.Type == AccountType.Current)
            {
                if (account.Balance < amount)
                {
                    throw new Exception("You have insufficient funds to complete this transaction.");
                }
            }

            var transType = TransType.Debit;
            var transCategory = TransCategory.Withdrawal;
            string transDesc = $"Withdrawal by {Auth.CurrentUser.FullName}";

            _transactionService.Create(amount, accountId, transType, transCategory, transDesc);
        }

        public void Transfer(decimal amount, int accountId, int destinationAccountId)
        {
            if (amount < 0)
            {
                throw new Exception("Why this?");
            }

            var account = DataStore.Accounts.First(x => x.Id == accountId);
            var destinationAccount = DataStore.Accounts.First(x => x.Id == destinationAccountId);

            if (account.Type == AccountType.Savings)
            {
                if (account.Balance - amount < 1000)
                {
                    throw new Exception("You have insufficient funds to complete this transaction.");
                }
            }
            else if (account.Type == AccountType.Current)
            {
                if (account.Balance < amount)
                {
                    throw new Exception("You have insufficient funds to complete this transaction.");
                }
            }

            var transType = TransType.Debit;
            var transCategory = TransCategory.Transfer;
            string transDesc = $"Transfer by {Auth.CurrentUser.FullName}";

            var transType2 = TransType.Credit;
            var transCategory2 = TransCategory.Deposit;
            string transDesc2 = $"Transfer from {Auth.CurrentUser.FullName}";

            _transactionService.Create(amount, accountId, transType, transCategory, transDesc);
            _transactionService.Create(amount, destinationAccountId, transType2, transCategory2, transDesc2);
        }

        public List<Account> GetAllUserAccounts(int accountId)
        {
            return DataStore.Accounts.Where(x => x.UserId == accountId).ToList();
        }
    }
}
