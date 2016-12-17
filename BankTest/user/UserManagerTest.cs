using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Balance;
using WcfBankingService.Accounts.Number;
using WcfBankingService.User;

namespace BankTest.User
{
    [TestClass]
    public class UserManagerTest
    {
        private const string Login = "Admin";
        private const string Password = "Password";

        private const string MockLogin = "NieAdmin132";
        private const string MockAccessToken = "accessToken142";

        private readonly IAccount _account;

        private readonly UserManager _userManager;

        public UserManagerTest()
        {
            var userList = new List<IUser>
            {
                new WcfBankingService.User.User(Login, Password),
                new UserMock(MockLogin, MockAccessToken)
            };
            _userManager = new UserManager(userList);
            _account = new WcfBankingService.Accounts.Account(new AccountNumber("", "", ""), new Balance(122m));
        }


        [TestMethod]
        public void SignInUser_UserExists_returnsAccessToken()
        {
            var accessToken = _userManager.SignIn(Login, Password);
            Assert.IsNotNull(accessToken);
        }

        [TestMethod]
        public void SignInUser_WrongLogin_returnsNull()
        {
            var accessToken = _userManager.SignIn("Doge", Password);
            Assert.IsNull(accessToken);
        }

        [TestMethod]
        public void SignInUser_WrongPassword_returnsNull()
        {
            var accessToken = _userManager.SignIn(Login, "Wow");
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
        public void GetAllAccountsFromUser_CorrectAccessTokenAndLogin_ReturnsAccountNumbers()
        {
            Assert.IsNotNull(_userManager.GetAllAccounts(MockLogin, MockAccessToken));
        }

        [TestMethod]
        public void GetAllAcountsFromUser_WrongLogin_ReturnsNull()
        {
            Assert.IsNull(_userManager.GetAllAccounts("WOOW", MockAccessToken));
        }

        [TestMethod]
        public void GetAllAccountFromUser_WrongAccessToken_ReturnsNull()
        {
            Assert.IsNull(_userManager.GetAllAccounts(MockLogin, "Wrong token"));
        }

        [TestMethod]
        public void AddAccountNumber_NewNumber_ReturnsTrue()
        {
            var accessToken = _userManager.SignIn(Login, Password);
            Assert.IsNotNull(accessToken);
            Assert.IsTrue(_userManager.AddAccount(Login, accessToken, _account));
            var accounts = _userManager.GetAllAccounts(Login, accessToken);
            Assert.IsNotNull(accounts);
            Assert.AreEqual(accounts.Count(), 1);
        }

        [TestMethod]
        public void AddAccountNumber_WrongLogin_ReturnsFalse()
        {
            var accessToken = _userManager.SignIn(Login, Password);
            Assert.IsNotNull(accessToken);
            Assert.IsFalse(_userManager.AddAccount("Wow", accessToken, _account));
        }

        [TestMethod]
        public void AddAccountNumber_WrongAccessToken_ReturnsFalse()
        {
            Assert.IsFalse(_userManager.AddAccount(Login, "nie wow", _account));
        }
    }
}