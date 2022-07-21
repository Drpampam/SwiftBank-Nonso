using Swyft.Core.Models;
using Swyft.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swyft.Core.Interfaces
{
    public interface ITransactionService : IEntityService
    {
        public void Create(decimal amount, int acccountId, TransType transType, TransCategory transCategory, string transDesc);
        public Transaction Get(int id);
        List<Transaction> GetAllAccountTransactions(int id);
    }
}
