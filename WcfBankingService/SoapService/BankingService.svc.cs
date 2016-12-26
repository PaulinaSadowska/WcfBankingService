using System;
using System.Collections.Generic;
using WcfBankingService.operation;
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
            return new LogInResponse(_bank.SignIn(login, password));
        }

        public PaymentResponse Deposit(PaymentData paymentData)
        {
            _inputValidator.CheckPaymentData(paymentData);
            _bank.Deposit(paymentData); // TODO - add return type (operation status)
            return new PaymentResponse();
        }

        public PaymentResponse Transfer(TransferData transferData)
        {
            throw new NotImplementedException();
        }

        public PaymentResponse Withdraw(PaymentData paymentData)
        {
            _inputValidator.CheckPaymentData(paymentData);
            _bank.Withdraw(paymentData);
            return new PaymentResponse();
        }

        public IEnumerable<OperationRecord> GetOperationHistory(string accessToken, string accountNumber)
        {
           //TODO - validate input data
            return _bank.GetOperationHistory(accessToken, accountNumber);
            //TODO - change type to OperationHistoryResponse to send error codes
        }
    }
}
