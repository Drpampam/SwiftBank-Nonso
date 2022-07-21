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
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Pin { get; set; }
        public EntityStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public User(int id, string firstName, string lastName, string email, string password, string pin)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            CreatedAt = DateTime.Now;
            Pin = pin;
        }
    }
}
