using System.ComponentModel;
using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Response
{
    public static class EnumExtensions
    {
       
        public static string Message(this ResponseStatus val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}