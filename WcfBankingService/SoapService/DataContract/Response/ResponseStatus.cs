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
        InsufficientFunds = -11,

        [Description("Account number does not exist")]
        AccountNumberDoesntExist = -21,

        [Description("Wrong account number format")]
        WrongAccountNumberFormat = -22,

        [Description("Account number does not belong to this bank")]
        AccountNumberFromOtherBank = -23,

        [Description("Access Denied")]
        AccessDenied = -41,
    }
}