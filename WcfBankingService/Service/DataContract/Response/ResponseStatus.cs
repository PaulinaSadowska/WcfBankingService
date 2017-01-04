using System.ComponentModel;

namespace WcfBankingService.Service.DataContract.Response
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
        WrongAccountNumber = -22,

        [Description("Access Denied")]
        AccessDenied = -41,

        [Description("Sending transfer to other bank failed")]
        InterbankTransferFailed = -61,
    }
}