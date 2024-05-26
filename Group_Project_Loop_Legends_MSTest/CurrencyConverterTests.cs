using Group_Project_Loop_Legends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Loop_LegendsMSTest
{
    [TestClass]
    public class CurrencyConverterTests
    {
        [TestMethod]
        public void ConvertCurrency_FromUSDToSEK_ShouldReturnCorrectAmount()
        {
            // Arrange
            Account test1 = new Account(1000, "USD", "testAccount", "testPerson");
            Account test2 = new Account(0, "SEK", "testAccount", "testPerson");
            var currentAccount = test1;
            var receivingAccount = test2;
            double amount = 100;

            // Act
            double result = CurrencyConverter.ConvertCurrency(currentAccount, receivingAccount, amount);

            // Assert
            Assert.AreEqual(1041.67, Math.Round(result, 2));
        }

        [TestMethod]
        public void ConvertCurrency_FromEURToGBP_ShouldReturnCorrectAmount()
        {
            // Arrange
            Account test1 = new Account(100, "EURO", "testAccount", "testPerson");
            Account test2 = new Account(0, "GBP", "testAccount", "testPerson");
            var currentAccount = test1;
            var receivingAccount = test2;
            double amount = 100;

            // Act
            double result = CurrencyConverter.ConvertCurrency(currentAccount, receivingAccount, amount);

            // Assert
            Assert.AreEqual(86.36, Math.Round(result, 2));
        }

        [TestMethod]
        public void TotalAsset_WithMixedCurrencies_ShouldReturnCorrectTotalInSEK()
        {
            // Arrange
            var accounts = new List<Account>
            {
                new Account ( 100, "USD", "test", "testPerson"),
                new Account ( 100, "EURO", "test", "testPerson"),
                new Account ( 100, "GBP", "test", "testPerson"),
                new Account ( 1000, "JPY", "test", "testPerson"),
            };

            // Act
            double result = CurrencyConverter.TotalAsset(accounts);

            // Assert
            Assert.AreEqual(3563.70, Math.Round(result, 2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertCurrency_WithUnsupportedCurrency_ShouldThrowArgumentException()
        {
            // Arrange
            Account test1 = new Account(1000, "ABC", "testAccount", "testPerson");
            Account test2 = new Account(0, "SEK", "testAccount", "testPerson");
            var currentAccount = test1;
            var receivingAccount = test2;
            double amount = 100;

            // Act
            CurrencyConverter.ConvertCurrency(currentAccount, receivingAccount, amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TotalAsset_WithUnsupportedCurrency_ShouldThrowArgumentException()
        {
            // Arrange
            var accounts = new List<Account>
            {
                new Account ( 1000, "ABC", "test", "testPerson")
            };

            // Act
            CurrencyConverter.TotalAsset(accounts);
        }
    }
}