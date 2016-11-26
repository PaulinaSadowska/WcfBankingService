using System.ComponentModel;
using System.Runtime.Serialization;

namespace WcfBankingService
{
    [DataContract]
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