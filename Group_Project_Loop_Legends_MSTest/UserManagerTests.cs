using Group_Project_Loop_Legends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Loop_LegendsMSTest
{
    [TestClass]
    public class UserManagerTests
    {
        [TestMethod]
        public void Test_CreateInitialUsers()
        {
            // Arrange
            UserManager userManager = new UserManager();

            // Act
            userManager.CreateInitialUsers();

            // Assert
            // Test customer creation
            Assert.AreEqual(4, UserManager.customerList.Count);
            Assert.IsTrue(UserManager.customerList.Exists(c => c._name == "Anton"));
            Assert.IsTrue(UserManager.customerList.Exists(c => c._name == "Daniel"));
            Assert.IsTrue(UserManager.customerList.Exists(c => c._name == "John"));
            Assert.IsTrue(UserManager.customerList.Exists(c => c._name == "Rasmus"));

            // Test admin creation
            Assert.AreEqual(2, userManager.adminList.Count);
            Assert.IsTrue(userManager.adminList.Exists(a => a._name == "Pär"));
            Assert.IsTrue(userManager.adminList.Exists(a => a._name == "Tobias"));

            // Test account creation
            foreach (var customer in UserManager.customerList)
            {
                Assert.AreEqual(2, customer._accountList.Count);
            }
        }
    }
}
