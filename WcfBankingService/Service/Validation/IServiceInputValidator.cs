using WcfBankingService.Service.DataContract.Request;

namespace WcfBankingService.Service.Validation
{
    internal interface IServiceInputValidator
    {
        /// <summary>
        /// Validates if login field is correct
        /// </summary>
        /// <param name="login">user login field to validate</param>
        void ValidateLogin(string login);

        /// <summary>
        /// Validates if password field is correct
        /// </summary>
        /// <param name="password">user password field to validate</param>
        void ValidatePassword(string password);

        /// <summary>
        /// Validates if access token field is correct
        /// </summary>
        /// <param name="accessToken">access token to validate</param>
        void ValidateAccessToken(string accessToken);

        /// <summary>
        /// Validate if account number has corrent format
        /// </summary>
        /// <param name="accountNumber">account number to validate</param>
        void ValidateAccountNumber(string accountNumber);

        /// <summary>
        /// Validate if all withdraw data fields are valid
        /// </summary>
        /// <param name="withdrawData">withdraw data to validate</param>
        void Validate(WithdrawData withdrawData);

        /// <summary>
        /// Validate if all deposit data fields are valid
        /// </summary>
        /// <param name="depositData">deposit data to validate</param>
        void Validate(DepositData depositData);

        /// <summary>
        /// Validate if all transfer data fields are valid
        /// </summary>
        /// <param name="transferData">transfer data to validate</param>
        void Validate(TransferData transferData);

        /// <summary>
        /// Validate if all soap transfer data fields are valid
        /// </summary>
        /// <param name="transferData">soap transfer data to validate</param>
        void Validate(SoapTransferData transferData);
    }
}
