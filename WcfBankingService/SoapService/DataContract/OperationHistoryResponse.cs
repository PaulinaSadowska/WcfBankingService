using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WcfBankingService.operation;
using WcfBankingService.SoapService.DataContract.Response;

namespace WcfBankingService.SoapService.DataContract
{
    [DataContract]
    public class OperationHistoryResponse
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; }

        [DataMember]
        public IEnumerable<OperationRecord> OperationRecords { get; }

        public OperationHistoryResponse(ResponseStatus responseStatus)
        {
            if(responseStatus==ResponseStatus.Success)
                throw new Exception("Cannot set response to success without assigning operationRecords");
            ResponseStatus = responseStatus;
        }

        public OperationHistoryResponse(IEnumerable<OperationRecord> operationRecords)
        {
            OperationRecords = operationRecords;
            ResponseStatus = ResponseStatus.Success;
        }
    }
}
