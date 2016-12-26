﻿using WcfBankingService.SOAPService.DataContract;

namespace WcfBankingService.SoapService.Validation
{
    internal interface IServiceInputValidator
    {
        void ValidateLogin(string login);

        void ValidatePassword(string password);

        void ValidateAccessToken(string accessToken);

        void ValidateAccountNumber(string accountNumber);

        void ValidatePaymentData(PaymentData paymentData);
    }
}
