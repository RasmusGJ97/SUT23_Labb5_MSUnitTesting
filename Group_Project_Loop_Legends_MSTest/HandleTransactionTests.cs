using Group_Project_Loop_Legends;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System.ComponentModel;

namespace Group_Project_Loop_LegendsMSTest
{
    [TestClass]
    public class HandleTransactionTests
    {
        [TestMethod]
        [TestCategory("Transactions")]
        public void TestHandleTransaction_SuccessfulTransaction()
        {
            // Arrange
            Account test1 = new Account(2000, "SEK", "test account", "test");
            Account test2 = new Account(2000, "SEK", "test account", "test2");
            var withdrawAccount = test1;
            var depositAccount = test2;
            double withdrawAmount = 500;
            double depositAmount = 500;
            var senderHistory = new List<string>();
            var receiverHistory = new List<string>();

            Customer testFunc = new Customer("testPerson", "testpass");

            // Act
            testFunc.HandleTransaction(withdrawAccount, depositAccount, withdrawAmount, depositAmount, senderHistory, receiverHistory);

            // Assert
            Assert.AreEqual(1500, withdrawAccount.Balance);
            Assert.AreEqual(2500, depositAccount.Balance);
            Assert.AreEqual(1, senderHistory.Count);
            Assert.AreEqual(1, receiverHistory.Count);
            StringAssert.Contains(senderHistory[0], "withdrawn from");
            StringAssert.Contains(receiverHistory[0], "deposited to");
        }

        [TestMethod]
        [TestCategory("Transactions")]
        public void TestHandleTransaction_InsufficientBalance()
        {
            // Arrange
            Account test1 = new Account(200, "SEK", "test account", "test");
            Account test2 = new Account(2000, "SEK", "test account", "test2");
            var withdrawAccount = test1;
            var depositAccount = test2;
            double withdrawAmount = 500;
            double depositAmount = 500;
            var senderHistory = new List<string>();
            var receiverHistory = new List<string>();

            Customer testFunc = new Customer("testPerson", "testpass");

            // Act
            testFunc.HandleTransaction(withdrawAccount, depositAccount, withdrawAmount, depositAmount, senderHistory, receiverHistory);

            // Assert
            Assert.AreEqual(200, withdrawAccount.Balance);
            Assert.AreEqual(2000, depositAccount.Balance);
            Assert.AreEqual(1, senderHistory.Count);
            Assert.AreEqual(0, receiverHistory.Count);
            StringAssert.Contains(senderHistory[0], "canceled due to not enough balance");
        }
    }
}