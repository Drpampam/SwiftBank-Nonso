using Swyft.Core.Data;
using Swyft.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Swyft.Helpers
{
    public class Validate
    {
        public static bool Register(string firstName, string lastName, string email, string password, string passwordConfirm, string pin, out List<string> messages)
        {
            messages = new List<string>();
            if (!Regex.IsMatch(firstName, @"[A-Z][a-z]+"))
            {
                messages.Add("Invalid input for Firstname: name must be in block letters.");
            }
            if (!Regex.IsMatch(lastName, @"[A-Z][a-z]+"))
            {
                messages.Add("Invalid input for Lastname: name must be in block letters.");
            }
            if (!Regex.IsMatch(email, @"^[a-z0-9]{5,20}@[a-z]{3,20}\.[a-z]{2,4}$"))
            {
                messages.Add("Invalid input for Email: must be a valid email address.");
            }
            if (!Regex.IsMatch(password, @"^(?=.*[@$!%*#?&])(?=.*[A-Za-z])(?=.*\d)[A-Za-z0-9@$!%*#?&]{6,}$"))
            {
                messages.Add("Invalid input for Password: must be minimum 6 characters that include alphanumeric and at least one special character");
            }
            if (password != passwordConfirm)
            {
                messages.Add("Passwords do not match.");
            }
            if (!Regex.IsMatch(pin, @"^\d{4}$"))
            {
                messages.Add("Invalid transaction pin: must be a 4 digit value");
            }

            if (messages.Count > 0) return false;
            return true;
        }

        public static Account CheckAccountExists(string accountNumber, out string message)
        {
            if(!Regex.IsMatch(accountNumber, @"\d{10}"))
            {
                message = "Invalid input";
                return null;
            }
            var account = DataStore.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
            if(account == null)
            {
                message = "Destination account does not exist.";
            }

            message = String.Empty;
            return account;
        }
    }
}
