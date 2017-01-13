using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WcfBankingService.operation;

namespace WcfBankingService.Service.DataContract.Response
{
    [DataContract]
    public class OperationHistoryResponse
    {
        [DataMember]
        public IEnumerable<OperationRecord> OperationRecords { get; set; }

        public OperationHistoryResponse(IEnumerable<OperationRecord> operationRecords)
        {
            OperationRecords = operationRecords;
        }
    }
}
