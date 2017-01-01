using WcfBankingService.SOAPService.DataContract;

namespace WcfBankingService.Service.Validation
{
    internal interface IServiceInputValidator
    {
        void ValidateLogin(string login);

        void ValidatePassword(string password);

        void ValidateAccessToken(string accessToken);

        void ValidateAccountNumber(string accountNumber);

        void ValidatePaymentData(PaymentData paymentData);

        void ValidateTransferData(TransferData transferData);
    }
}
