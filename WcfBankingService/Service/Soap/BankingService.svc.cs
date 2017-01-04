using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Service.Validation;
using WcfBankingService.SoapService.DataContract.Response;

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

        public PaymentResponse Deposit(DepositData paymentData)
        {
            _inputValidator.Validate(paymentData);
            return _bank.Deposit(paymentData);
        }

        public PaymentResponse Transfer(TransferData transferData, string accessToken)
        {
            _inputValidator.Validate(transferData);
            return _bank.SoapTransfer(transferData, accessToken);
        }

        public PaymentResponse Withdraw(WithdrawData paymentData)
        {
            _inputValidator.Validate(paymentData);
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