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
    public class TransactionService : ITransactionService
    {
        public static int IdCount { get; set; }
        public void Create(decimal amount, int acccountId, TransType transType, TransCategory transCategory, string transDesc)
        {
            IdCount++;
            var trans = new Transaction(IdCount, transType,transCategory, amount, acccountId, transDesc);

            DataStore.Transactions.Add(trans);

            var account = DataStore.Accounts.First(x => x.Id == acccountId);
            trans.AccountBalance = account.Balance;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id)
        {
            throw new NotImplementedException();
        }

        public Transaction Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetAllAccountTransactions(int transId)
        {
            return DataStore.Transactions.Where(x => x.AccountId == transId).ToList();
        }
    }
}
