using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.DataContract;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Service.Validation;

namespace WcfBankingService.Service.Rest
{
    public class BankingRestService : IBankingRestService
    {

        private readonly IServiceInputValidator _inputValidator;
        private readonly Bank _bank;


        public BankingRestService()
        {
            _inputValidator = new ServiceInputValidator();
            _bank = new Bank(new MockDataInserter());
        }

        public TransferResponse Transfer(TransferData transferData)
        {
            //403 not authorized (basic auth) - TODO
            try 
            {
                _inputValidator.Validate(transferData);
            }
            catch (FaultException exception)
            {
                SetResponseCode(HttpStatusCode.BadRequest);
                return new TransferResponse(exception.Message);
            }
            try
            {
                _bank.Transfer(transferData);
                SetResponseCode(HttpStatusCode.Created);
                return new TransferResponse();
            }
            catch (BankException exception)
            {
                SetResponseCode(HttpStatusCode.InternalServerError);
                return new TransferResponse(exception.Message);
            }
        }

        private static void SetResponseCode(HttpStatusCode statusCode)
        {
            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.StatusCode = statusCode;
        }
    }
}