using System.ComponentModel;
using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Response
{
    public enum ResponseStatus
    {
        [Description("")]
        Success = 0,

        [Description("Incorrect Login or HashedPassword")]
        IncorrectLoginOrPassword = -1,

        [Description("Insufficient funds on the account to perform the operation")]
        InsufficientFunds = -11,

        [Description("Account number does not exist")]
        AccountNumberDoesntExist = -21,

        [Description("Wrong account number format")]
        WrongAccountNumber = -22,


        [Description("Account number from other bank")]
        OtherBankAccount = -23,
        

        [Description("Access Denied")]
        AccessDenied = -41,

        [Description("Sending transfer to other bank failed")]
        InterbankTransferFailed = -61,

        [Description("The bank you want to transfer the money to does not exist")]
        BankNotExists = -62,
    }
}