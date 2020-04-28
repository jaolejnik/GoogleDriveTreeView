using NUnit.Framework;

namespace GoogleDriveTreeView.Tests
{
    class UserTest
    {
        private User user;
        private User userArg;
        private string username = "username";
        private string password = "password";

        [SetUp]
        public void Setup()
        {
            userArg = new User(username, password);
            user = new User();
            user.Username = username;
            user.Password = password;
        }

        [Test]
        public void User_DefConstr_test()
        {
            Assert.AreEqual(user.Username, username);
            Assert.AreEqual(user.Password, password);
        }

        [Test]
        public void User_ArgConstr_test()
        {
            Assert.AreEqual(userArg.Username, username);
            Assert.AreEqual(user.Password, password);
        }
    }
}
