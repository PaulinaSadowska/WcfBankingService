using System.Collections.Generic;
using System.Security.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Balance;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Database.DataProvider;
using WcfBankingService.Service.DataContract.Response;
using WcfBankingService.Users;
using WcfBankingService.Users.Access;

namespace BankTest.User
{
    [TestClass]
    public class UserManagerTest
    {
        private const string Login = "Admin";
        private const string Password = "Pass";

        private readonly string _accessToken;

        private readonly AccountNumber _accountNumber;

        private readonly UserManager _userManager;

        public UserManagerTest()
        {
            var hashedPassword = new StandardPasswordHasher().HashPassword(Password);
   
            IUser user = new WcfBankingService.Users.User(Login, hashedPassword);
            _accessToken = user.GenerateAccessToken(Password);
            Assert.IsNotNull(_accessToken);
            _accountNumber = new AccountNumber("12345678", "1234567891234321", "12");
            IAccount account = new WcfBankingService.Accounts.Account(_accountNumber, new Balance(122m));
            Assert.IsTrue(user.AddAccount(_accessToken, account));
            var userList = new List<IUser>
            {
                user
            };
            _userManager = new UserManager(new MockDataProvider(userList));

            Assert.AreEqual("Incorrect Login or Password", ResponseStatus.IncorrectLoginOrPassword.Message());
            var response = ResponseStatus.IncorrectLoginOrPassword;
            Assert.AreEqual("Incorrect Login or Password", response.Message());
        }


        [TestMethod]
        public void SignInUser_UserExists_returnsAccessToken()
        {
            var accessToken = _userManager.SignIn(Login, Password);
            Assert.IsNotNull(accessToken);
        }

        [TestMethod]
        [ExpectedException(typeof(BankException))]
        public void SignInUser_WrongLogin_throwsException()
        {
            _userManager.SignIn("Doge", Password);
        }

        [TestMethod]
        [ExpectedException(typeof(BankException))]
        public void SignInUser_WrongPassword_throwsException()
        {
            _userManager.SignIn(Login, "Wow");
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
        [ExpectedException(typeof(AuthenticationException))]
        public void GetAllAccountFromUser_WrongAccessToken_ReturnsNull()
        {
            _userManager.GetAllAccounts(Login, "Wrong token");
        }

        [TestMethod]
        public void GetAccount_CorrectAccessTokenAndAccountNumber_ReturnsAccount()
        {
            Assert.IsNotNull(_userManager.GetAccount(_accessToken, _accountNumber));
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void GetAccount_IncorrectAccessToken_ReturnsNull()
        {
            _userManager.GetAccount("wrong token nie wow", _accountNumber);
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
            var account = new WcfBankingService.Accounts.Account(new AccountNumber("122222", "11", "1"),
                new Balance(123));
            var accessToken = _userManager.SignIn(Login, Password);
            Assert.IsNotNull(accessToken);
            Assert.IsFalse(_userManager.AddAccount("Wow", accessToken, account));
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void AddAccountNumber_WrongAccessToken_ReturnsFalse()
        {
            var account = new WcfBankingService.Accounts.Account(new AccountNumber("122222", "11", "1"),
                new Balance(123));
            _userManager.AddAccount(Login, "nie wow", account);
        }
    }
}