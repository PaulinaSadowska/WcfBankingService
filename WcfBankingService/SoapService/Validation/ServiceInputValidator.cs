using System.ServiceModel;

namespace WcfBankingService.SoapService
{
    public class ServiceInputValidator : IServiceInputValidator
    {
        public static readonly int MIN_LOGIN_LENGTH = 3;
        public static readonly int MIN_PASSWORD_LENGTH = 3;

        public void CheckSignInDataValid(string login, string password)
        {
            if (login == null)
            {
                throw new FaultException("login can't be null");
            }
            else if(password == null)
            {
                throw new FaultException("login can't be null");
            }
            else if(login.Length < MIN_LOGIN_LENGTH)
            {
                throw new FaultException($"login must contains at least {MIN_LOGIN_LENGTH} characters");
            }
            else if (password.Length < MIN_PASSWORD_LENGTH)
            {
                throw new FaultException($"password must contains at least {MIN_PASSWORD_LENGTH} characters");
            }
        }
    }
}