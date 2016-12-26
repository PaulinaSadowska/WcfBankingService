using System.ComponentModel;

namespace WcfBankingService.SoapService.DataContract.Response
{
    public enum ResponseStatus
    {
        [Description("")]
        Success = 0,

        [Description("Incorrect Login or Password")]
        IncorrectLoginOrPassword = -1,

        [Description("Insufficient funds on the account to perform the operation")]
        InsufficientFunds = -2,
    }
}