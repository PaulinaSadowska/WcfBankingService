using System.ServiceModel;
using WcfBankingService.SOAPService.DataContract;

namespace WcfBankingService.SoapService.Validation
{
    public class ServiceInputValidator : IServiceInputValidator
    {
        public static readonly int MinLoginLength = 3;
        public static readonly int MinPasswordLength = 3;

        public void CheckSignInDataValid(string login, string password)
        {
            if (login == null)
            {
                throw new FaultException("login can't be null");
            }
            if(password == null)
            {
                throw new FaultException("login can't be null");
            }
            if(login.Length < MinLoginLength)
            {
                throw new FaultException($"login must contains at least {MinLoginLength} characters");
            }
            if (password.Length < MinPasswordLength)
            {
                throw new FaultException($"password must contains at least {MinPasswordLength} characters");
            }
        }

        public void CheckPaymentData(PaymentData paymentData)
        {
            if (paymentData == null)
            {
                throw new FaultException("payment data can't be null!");
            }
            if (paymentData.AccountNumber == null)
            {
                throw new FaultException("Account number can't be null!");
            }
            if (paymentData.AccessToken == null)
            {
                throw new FaultException("Access token can't be null!");
            }
            if (paymentData.OperationTitle == null)
            {
                throw new FaultException("Operation title can't be null!");
            }
            if (paymentData.Amount<0)
            {
                throw new FaultException("Amount must be greater or equal to 0");
            }
        }
    }
}