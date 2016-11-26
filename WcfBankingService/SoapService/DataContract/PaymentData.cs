﻿using System;
using System.Numerics;
using System.Runtime.Serialization;

namespace WcfBankingService.SOAPService.DataContract
{
    [DataContract]
    public class PaymentData
    {
        [DataMember]
        private String AccountNumber { get; set; }

        [DataMember]
        private BigInteger Amount { get; set; }
    }
}