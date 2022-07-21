using NUnit.Framework;
using Swyft.Core.Models;
using Swyft.Utility;

namespace Swyft.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AccountBalanceValid()
        {
            // arrange
            var user = new User(1, "Emeka", "Ike", "emekaike@test.com", "emeka2022!", "1234");
            var account = new Account(1, user.FullName, "000999", AccountType.Savings, 1);

            // act
            decimal expected = 0M;
            decimal actual = account.Balance;

            // assert
            Assert.AreEqual(expected, actual);


        }
    }
}