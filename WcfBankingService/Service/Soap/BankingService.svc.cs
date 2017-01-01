using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.Validation;
using WcfBankingService.SoapService.DataContract.Response;
using WcfBankingService.SOAPService.DataContract;

namespace WcfBankingService.Service.Soap
{
    public class BankingService : IBankingService
    {
        private readonly IServiceInputValidator _inputValidator;
        private readonly Bank _bank;


        public BankingService()
        {
            _inputValidator = new ServiceInputValidator();
            _bank = new Bank(new DbDataInserter());
        }

        public BankingService(IBankDataInserter dataInserter)
        {
            _inputValidator = new ServiceInputValidator();
            _bank = new Bank(dataInserter);
        }

        public LogInResponse SignIn(string login, string password)
        {
            _inputValidator.ValidateLogin(login);
            _inputValidator.ValidatePassword(password);
            return _bank.SignIn(login, password);
        }

        public PaymentResponse Deposit(PaymentData paymentData)
        {
            _inputValidator.ValidatePaymentData(paymentData);
            return _bank.Deposit(paymentData);
        }

        public PaymentResponse Transfer(TransferData transferData)
        {
            return new PaymentResponse(ResponseStatus.Success);
        }

        public PaymentResponse Withdraw(PaymentData paymentData)
        {
            _inputValidator.ValidatePaymentData(paymentData);
            return _bank.Withdraw(paymentData);
        }

        public OperationHistoryResponse GetOperationHistory(string accessToken, string accountNumber)
        {
            _inputValidator.ValidateAccessToken(accessToken);
            _inputValidator.ValidateAccountNumber(accountNumber);
            return _bank.GetOperationHistory(accessToken, accountNumber);
        }
    }
}