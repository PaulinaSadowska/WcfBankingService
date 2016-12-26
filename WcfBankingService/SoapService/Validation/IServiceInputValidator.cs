using WcfBankingService.SOAPService.DataContract;

namespace WcfBankingService.SoapService.Validation
{
    interface IServiceInputValidator
    {
        //throws exception when sign in data are not valid
        void CheckSignInDataValid(string login, string password);
        void CheckPaymentData(PaymentData paymentData);
    }
}
