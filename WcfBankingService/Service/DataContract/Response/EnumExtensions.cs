using System.ComponentModel;
using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Response
{
    public static class EnumExtensions
    {
       /// <summary>
       /// returns message field from ResponseStatus
       /// </summary>
       /// <param name="val">response status</param>
       /// <returns>response status message</returns>
        public static string Message(this ResponseStatus val)
        {
            var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}