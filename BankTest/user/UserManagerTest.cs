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

        private readonly string MockLogin = "NieAdmin132";
        private readonly string MockAccessToken = "accessToken142";

        private readonly UserManager _userManager;

        public UserManagerTest()
        {
            var userList = new List<IUser>
            {
                new User(Login, Password),
                new UserMock(MockLogin, MockAccessToken)
            };
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

        [TestMethod]
        public void SignUpUser_UserNotExists_returnsTrue()
        {
            Assert.IsTrue(_userManager.SignUp("Doge", "Wow"));
        }

        [TestMethod]
        public void SignUpUser_UserAlreadyExists_returnsFalse()
        {
            Assert.IsFalse(_userManager.SignUp(Login, "Wow"));
        }
        
        [TestMethod]
        public void GetAccountNumbersFromUser_CorrectAccessToken_ReturnsAccountNumbers()
        {
            Assert.IsTrue(_userManager.ContainsUser(MockLogin));
        }
    }
}