using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using WcfBankingService.SoapService.DataContract.Response;
using WcfBankingService.SOAPService.DataContract;
using static System.Int32;

namespace WcfBankingService.RestService
{
    public class BankingRestService : IBankingRestService
    {
    
        public string Transfer(TransferData transferData)
        {
            if (transferData != null)
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Created;
                return $"{transferData.Amount} {transferData.SenderAccountNumber} receiver: {transferData.AccountNumber} {transferData.Title}";
            }
            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.BadRequest;
            return "";
        }
    }

}