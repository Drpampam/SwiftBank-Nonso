using Swyft.Core.Data;
using Swyft.Core.Interfaces;
using Swyft.Core.Services;
using Swyft.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swyft.Core.Models
{
    public class Account : IEntity
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public AccountType Type { get; set; }
        public int UserId { get; set; }
        public EntityStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Balance {
            get
            {
                //subtract sum of all debits from credits
                decimal balance = DataStore.Transactions.Where(x => x.AccountId == Id).Where(x => x.Type == TransType.Credit).Sum(x => x.Amount) -
                    DataStore.Transactions.Where(x => x.AccountId == Id).Where(x => x.Type == TransType.Debit).Sum(x => x.Amount);

                return balance;
            }
        }

        public Account(int id, string accountName, string accountNumber, AccountType type, int userId)
        {
            Id = id;
            AccountName = accountName;
            AccountNumber = accountNumber;
            Type = type;
            UserId = userId;
            Status = EntityStatus.Active;
            CreatedAt = DateTime.Now;
        }

    }
}
