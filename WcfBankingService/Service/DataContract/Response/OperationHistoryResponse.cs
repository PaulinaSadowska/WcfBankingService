using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WcfBankingService.operation;

namespace WcfBankingService.Service.DataContract.Response
{
    /// <summary>
    /// response after get operation history operation
    /// </summary>
    [DataContract]
    public class OperationHistoryResponse
    {
        /// <summary>
        /// operation history
        /// </summary>
        [DataMember]
        public IEnumerable<OperationRecord> OperationRecords { get; set; }

        public OperationHistoryResponse(IEnumerable<OperationRecord> operationRecords)
        {
            OperationRecords = operationRecords;
        }
    }
}
