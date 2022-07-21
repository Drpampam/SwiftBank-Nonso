using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swyft.Core.Interfaces
{
    public interface IUserService : IEntityService
    {
        public void Create(string firstName, string lastName, string email, string password, string pin);
    }
}
