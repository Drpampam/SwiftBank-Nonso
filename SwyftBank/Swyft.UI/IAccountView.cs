using Swyft.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swyft.UI
{
    public interface IAccountView
    {
        public void DisplayAccountMenu();
        public void DisplayCreateAccountMenu(User user);
        public void DisplayViewAccountMenu(User user);
        public void DisplaySingleAccount(Account account);
        public void DisplayDepositMenu(Account account);
        public void DisplayWithdrawalMenu(Account account);
        public void DisplayTransferMenu(Account account);
        public void DisplayAccountStatement(Account account);
        public void DisplayAccountBalance(Account account);
    }
}
