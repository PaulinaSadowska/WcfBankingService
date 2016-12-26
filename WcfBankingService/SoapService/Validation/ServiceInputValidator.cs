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
            else if(password == null)
            {
                throw new FaultException("login can't be null");
            }
            else if(login.Length < MinLoginLength)
            {
                throw new FaultException($"login must contains at least {MinLoginLength} characters");
            }
            else if (password.Length < MinPasswordLength)
            {
                throw new FaultException($"password must contains at least {MinPasswordLength} characters");
            }
        }

        public void CheckPaymentData(PaymentData paymentData)
        {
            if (paymentData == null)
            {
                throw new FaultException("payment data can't be null!");
            }//TODO - further validation
        }
    }
}