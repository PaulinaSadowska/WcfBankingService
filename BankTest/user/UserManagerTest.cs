using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService;
using WcfBankingService.user;

namespace BankTest.user
{
    [TestClass]
    public class UserManagerTest
    {
        private const string Login = "Admin";
        private const string Password = "Password";

        private readonly UserManager _userManager;

        public UserManagerTest()
        {
            var userList = new List<User> {new User(Login, Password)};
            _userManager = new UserManager(userList);
        }


        [TestMethod]
        public void SignInUser_UserExists_returnsAccessToken()
        {
            string accessToken = _userManager.SignIn(Login, Password);
            Assert.IsNotNull(accessToken);
        }

        [TestMethod]
        public void SignInUser_WrongLogin_returnsNull()
        {
            string accessToken = _userManager.SignIn("Doge", Password);
            Assert.IsNull(accessToken);
        }

        [TestMethod]
        public void SignInUser_WrongPassword_returnsNull()
        {
            string accessToken = _userManager.SignIn(Login, "Wow");
            Assert.IsNull(accessToken);
        }

        [TestMethod]
        public void ContainsUser_UserExists_returnsTrue()
        {
            Assert.IsTrue(_userManager.ContainsUser(Login));
        }

        [TestMethod]
        public void ContainsUser_UserNotExists_returnsFalse()
        {
            Assert.IsFalse(_userManager.ContainsUser("Doge"));
        }
    }
}