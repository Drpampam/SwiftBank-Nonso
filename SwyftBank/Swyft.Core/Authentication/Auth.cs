using Swyft.Core.Data;
using Swyft.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swyft.Core.Authentication
{
    public class Auth
    {
        public static User CurrentUser { get; set; }

        public static bool Login(string email, string password)
        {
            User user = DataStore.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user == null) return false;
            else
            {
                CurrentUser = user;
                return true;
            }
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
