using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Service.DataContract.Response;
using WcfBankingService.Service.Validation;

namespace WcfBankingService.Service.Soap
{
    /// <summary>
    /// <see cref="IBankingService"/>
    /// </summary>
    public class BankingService : IBankingService
    {
        private readonly IServiceInputValidator _inputValidator;
        private readonly Bank _bank;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BankingService()
            : this(new DbDataInserter())
        {
        }

        /// <summary>
        /// Constructor which allows to pass mock data inserter. Used in tests
        /// </summary>
        /// <param name="dataInserter">inserter used to save operation informations into database</param>
        public BankingService(IBankDataInserter dataInserter)
        {
            _inputValidator = new ServiceInputValidator();
            _bank = new Bank(dataInserter);
        }

        /// <summary>
        /// <see cref="IBankingService"/>
        /// </summary>
        public LogInResponse SignIn(string login, string password)
        {
            _inputValidator.ValidateLogin(login);
            _inputValidator.ValidatePassword(password);
            return _bank.SignIn(login, password);
        }

        /// <summary>
        /// <see cref="IBankingService"/>
        /// </summary>
        public PaymentResponse Deposit(DepositData paymentData)
        {
            _inputValidator.Validate(paymentData);
            return _bank.Deposit(paymentData);
        }

        /// <summary>
        /// <see cref="IBankingService"/>
        /// </summary>
        public PaymentResponse Transfer(SoapTransferData transferData)
        {
            _inputValidator.Validate(transferData);
            return _bank.SoapTransfer(transferData);
        }

        /// <summary>
        /// <see cref="IBankingService"/>
        /// </summary>
        public PaymentResponse Withdraw(WithdrawData paymentData)
        {
            _inputValidator.Validate(paymentData);
            return _bank.Withdraw(paymentData);
        }

        /// <summary>
        /// <see cref="IBankingService"/>
        /// </summary>
        public OperationHistoryResponse GetOperationHistory(string accessToken, string accountNumber)
        {
            _inputValidator.ValidateAccessToken(accessToken);
            _inputValidator.ValidateAccountNumber(accountNumber);
            return _bank.GetOperationHistory(accessToken, accountNumber);
        }
    }
}