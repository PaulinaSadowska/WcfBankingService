using WcfBankingService.Service.DataContract.Request;

namespace WcfBankingService.Service.Validation
{
    internal interface IServiceInputValidator
    {
        void ValidateLogin(string login);

        void ValidatePassword(string password);

        void ValidateAccessToken(string accessToken);

        void ValidateAccountNumber(string accountNumber);

        void Validate(WithdrawData paymentData);

        void Validate(DepositData paymentData);

        void Validate(TransferData transferData);

        void Validate(SoapTransferData transferData);
    }
}
