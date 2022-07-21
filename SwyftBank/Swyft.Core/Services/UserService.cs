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
    public class UserService : IUserService
    {
        
        public static int IdCount { get; set; }
        public void Create(string firstName, string lastName, string email, string password, string pin)
        {
            IdCount++;

            var user = new User(IdCount, firstName, lastName, email, password, pin);

            DataStore.Users.Add(user);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id)
        {
            throw new NotImplementedException();
        }

        public IEntity Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
