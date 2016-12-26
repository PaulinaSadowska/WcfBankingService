using System;
using System.Collections.Generic;
using WcfBankingService.operation;
using WcfBankingService.SoapService.DataContract;
using WcfBankingService.SoapService.DataContract.Response;
using WcfBankingService.SoapService.Validation;
using WcfBankingService.SOAPService.DataContract;

namespace WcfBankingService.SoapService
{
    public class BankingService : IBankingService
    {
        private readonly IServiceInputValidator _inputValidator;
        private readonly Bank _bank;


        public BankingService()
        {
            _inputValidator = new ServiceInputValidator();
            _bank = new Bank();
        }

        public LogInResponse SignIn(string login, string password)
        {
            _inputValidator.CheckSignInDataValid(login, password);
            return _bank.SignIn(login, password);
        }

        public PaymentResponse Deposit(PaymentData paymentData)
        {
            _inputValidator.CheckPaymentData(paymentData);
            return _bank.Deposit(paymentData);
        }

        public PaymentResponse Transfer(TransferData transferData)
        {
            throw new NotImplementedException();
        }

        public PaymentResponse Withdraw(PaymentData paymentData)
        {
            _inputValidator.CheckPaymentData(paymentData);
            return _bank.Withdraw(paymentData);
        }

        public OperationHistoryResponse GetOperationHistory(string accessToken, string accountNumber)
        {
           //TODO - validate input data
            return _bank.GetOperationHistory(accessToken, accountNumber);
        }
    }
}
