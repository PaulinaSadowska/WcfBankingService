using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.DataContract;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Service.Validation;
using WcfBankingService.SoapService.DataContract.Response;

namespace WcfBankingService.Service.Rest
{
    public class BankingRestService : IBankingRestService
    {
        private readonly IServiceInputValidator _inputValidator;
        private readonly Bank _bank;


        public BankingRestService()
        {
            _inputValidator = new ServiceInputValidator();
            _bank = new Bank(new DbDataInserter());
        }

        public TransferResponse Transfer(TransferData transferData)
        {
            try
            {
                _inputValidator.Validate(transferData);
            }
            catch (FaultException exception)
            {
                SetResponseCode(HttpStatusCode.BadRequest);
                return new TransferResponse(exception.Message);
            }
            var response = _bank.IncomingTransfer(transferData);
            SetResponseCode(HttpStatusCode.Created);
            return response.ResponseStatus == ResponseStatus.Success ? new TransferResponse() : new TransferResponse(response.ResponseStatus.ToString());
        }

        private static void SetResponseCode(HttpStatusCode statusCode)
        {
            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.StatusCode = statusCode;
        }
    }
}