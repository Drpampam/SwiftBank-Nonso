using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swyft.Core.Interfaces;
using Swyft.Core.Services;
using Swyft.UI;
using System;

namespace SwyftBank
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddScoped<IUserService, UserService>();
                    services.AddScoped<ITransactionService, TransactionService>();
                    services.AddScoped<IAccountService, AccountService>();
                    services.AddScoped<IAuthView, AuthView>();
                    services.AddScoped<IAccountView, AccountView>();
                }).Build();

            var userInterface = ActivatorUtilities.CreateInstance<UserInterface>(host.Services);
            userInterface.Run();
        }
    }
}
