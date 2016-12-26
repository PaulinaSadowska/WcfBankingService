using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WcfBankingService.operation;

namespace WcfBankingService.SoapService.DataContract.Response
{
    [DataContract]
    public class OperationHistoryResponse : IResponse
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
