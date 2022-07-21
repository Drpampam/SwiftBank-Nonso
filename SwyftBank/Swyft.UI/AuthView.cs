using Swyft.Core.Authentication;
using Swyft.Core.Interfaces;
using Swyft.Core.Services;
using Swyft.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Swyft.UI
{
    public class AuthView : IAuthView
    {
        private readonly IUserService _userService;

        public AuthView(IUserService userService)
        {
            _userService = userService;
        }
        public void DisplayAuthMenu()
        {
            WriteLine("Welcome, select an option to continue:");
            WriteLine("\t1. Login\n\t2. Register\n\t3. Exit");
            Write("==> ");

            string reply = ReadLine();

            if (reply == "1")
            {
                Login();
            }
            else if (reply == "2")
            {
                Register();
            }else if (reply == "3")
            {
                Environment.Exit(0);
            }
        }

        public bool Login()
        {
            int count = 0;

            while (count < 3)
            {
                WriteLine("Enter your details to login");
                Write("Email: ");
                string email = ReadLine();

                Write("Password: ");
                string password = ReadLine();

                if (Auth.Login(email, password)) return true;

                WriteLine("Invalid email or password");
                count++;
            }
            return false;
        }

        public void Register()
        {
            bool invalid = true;
            int count = 0;

            while (invalid && count < 3)
            {
                Write("Firstname: ");
                string firstName = ReadLine();

                Write("Lastname: ");
                string lastName = ReadLine();

                Write("Email: ");
                string email = ReadLine();

                Write("Password: ");
                string password = ReadLine();

                Write("Confirm Password: ");
                string passwordConfirm = ReadLine();

                Write("Enter a PIN to use for your transactions: ");
                string pin = ReadLine();

                bool detailsValid = Validate.Register(firstName, lastName, email, password, passwordConfirm, pin, out List<string> messages);

                if (detailsValid)
                {
                    _userService.Create(firstName, lastName, email, password, pin);
                    Auth.Login(email, password);
                    invalid = false;
                }
                else
                {
                    WriteLine();
                    foreach (var message in messages)
                    {
                        WriteLine(message);
                    }
                    count++;
                    continue;
                }
            }
        }
    }
}
