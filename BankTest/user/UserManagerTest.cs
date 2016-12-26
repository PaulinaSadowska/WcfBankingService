using System.Collections.Generic;
using System.Linq;
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

        private readonly string _accessToken;

        private readonly AccountNumber _accountNumber;

        private readonly UserManager _userManager;

        public UserManagerTest()
        {
            IUser user = new WcfBankingService.User.User(Login, Password);
            _accessToken = user.GenerateAccessToken(Password);
            Assert.IsNotNull(_accessToken);

            _accountNumber = new AccountNumber("12345678", "1234567891234321", "12");
            IAccount account = new WcfBankingService.Accounts.Account(_accountNumber, new Balance(122m));
            Assert.IsTrue(user.AddAccount(_accessToken, account));
            var userList = new List<IUser>
            {
                user
            };

            _userManager = new UserManager(userList);
          
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
        public void GetAllAccountsFromUser_CorrectAccessTokenAndLogin_ReturnsAccounts()
        {
            Assert.IsNotNull(_userManager.GetAllAccounts(Login, _accessToken));
        }

        [TestMethod]
        public void GetAllAcountsFromUser_WrongLogin_ReturnsNull()
        {
            Assert.IsNull(_userManager.GetAllAccounts("WOOW", _accessToken));
        }

        [TestMethod]
        public void GetAllAccountFromUser_WrongAccessToken_ReturnsNull()
        {
            Assert.IsNull(_userManager.GetAllAccounts(Login, "Wrong token"));
        }

        [TestMethod]
        public void GetAccount_CorrectAccessTokenAndAccountNumber_ReturnsAccount()
        {
            Assert.IsNotNull(_userManager.GetAccount(_accessToken, _accountNumber));
        }

        [TestMethod]
        public void GetAccount_IncorrectAccessToken_ReturnsNull()
        {
            Assert.IsNull(_userManager.GetAccount("wrong token nie wow", _accountNumber));
        }

        [TestMethod]
        public void GetAccount_CorrectAccessTokenIncorrectAccountNumber_ReturnsNull()
        {
            Assert.IsNull(_userManager.GetAccount(_accessToken, new AccountNumber("13456222", "1232222222222222", "12")));
        }

        [TestMethod]
        public void AddAccountNumber_NewNumber_ReturnsTrue()
        {
            var accountNumber = new AccountNumber("122222", "11", "1");
            var account = new WcfBankingService.Accounts.Account(accountNumber, new Balance(123));
            var accessToken = _userManager.SignIn(Login, Password);
            Assert.IsNotNull(accessToken);
            Assert.IsTrue(_userManager.AddAccount(Login, accessToken, account));
            var searchedAccount = _userManager.GetAccount(accessToken, accountNumber);
            Assert.IsNotNull(searchedAccount);
            Assert.AreEqual(account, searchedAccount);
        }

        [TestMethod]
        public void AddAccountNumber_WrongLogin_ReturnsFalse()
        {
            var account = new WcfBankingService.Accounts.Account(new AccountNumber("122222", "11", "1"), new Balance(123));
            var accessToken = _userManager.SignIn(Login, Password);
            Assert.IsNotNull(accessToken);
            Assert.IsFalse(_userManager.AddAccount("Wow", accessToken, account));
        }

        [TestMethod]
        public void AddAccountNumber_WrongAccessToken_ReturnsFalse()
        {
            var account = new WcfBankingService.Accounts.Account(new AccountNumber("122222", "11", "1"), new Balance(123));
            Assert.IsFalse(_userManager.AddAccount(Login, "nie wow", account));
        }
    }
}