using Microsoft.Extensions.Options;
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
    public class UserInterface
    {
        private readonly IAuthView _authView;
        private readonly IAccountView _accountView;

        public UserInterface(IAuthView authView, IAccountView accountView )
        {
            _authView = authView;
            _accountView = accountView;
        }
    
        public void Run()
        {

            ForegroundColor = ConsoleColor.DarkYellow;

            bool running = true;

            while (running)
            {
                while (Auth.CurrentUser == null)
                {
                    Print.PrintLogo();
                    _authView.DisplayAuthMenu();
                }

                _accountView.DisplayAccountMenu();
            }
        }
    }
}
