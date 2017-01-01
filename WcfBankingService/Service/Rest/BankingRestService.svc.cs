using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.DataContract;
using WcfBankingService.Service.Validation;
using WcfBankingService.SOAPService.DataContract;

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
            //201 Created if succeess - DONE
            //404 when there is no receiver - AUTOMATICALLY
            //400 wrong format (missing field, amount <0) - DONE
            //403 not authorized (basic auth) - TODO
            //500 server error - AUTOMATICALY
            try
            {
                _inputValidator.ValidateTransferData(transferData);
            }
            catch (FaultException exception)
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.BadRequest;
                return new TransferResponse(exception.Message);
            }
            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Created;
            return
                new TransferResponse();
        }
    }
}