using System.ComponentModel;

namespace WcfBankingService.SoapService.DataContract
{
    public enum OperationResponse
    {
        [Description("")]
        Success = 0,

        [Description("Incorrect Login")]
        IncorrectLogin = -1,

        [Description("Incorrect Password")]
        IncorrectPassword = -2
    }
}