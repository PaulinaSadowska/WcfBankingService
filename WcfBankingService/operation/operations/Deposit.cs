using WcfBankingService.Accounts;
using WcfBankingService.operation;

namespace WcfBankingService.Operation.Operations
{
    /// <summary>
    /// deposit operation
    /// </summary>
    public class Deposit : BankOperation
    {
        private readonly IPublicAccount _targetAccount;
        private readonly decimal _amount;

        /// <summary>
        /// creates deposit command. Used to create deposit (default source field value)
        /// </summary>
        /// <param name="targetAccount">account on which operation will be performed</param>
        /// <param name="amount">amount to send</param>
        /// <param name="operationTitle">operation title</param>
        public Deposit(IPublicAccount targetAccount, decimal amount, string operationTitle) :
            this(targetAccount, amount, operationTitle, "Deposit")
        { 
        }

        /// <summary>
        /// creates deposit command. Allows to add custom spurce value
        /// </summary>
        /// <param name="targetAccount">account on which operation will be performed</param>
        /// <param name="amount">amount to send</param>
        /// <param name="operationTitle">operation title</param>
        /// <param name="source">source of the money</param>
        public Deposit(IPublicAccount targetAccount, decimal amount, string operationTitle, string source) : 
            base(targetAccount.AccountNumber, new OperationRecord
            {
                Title = operationTitle,
                Credit = amount,
                Source = source
            })
        {
            _targetAccount = targetAccount;
            _amount = amount;
        }

        /// <summary>
        /// Executes deposit operation and sabe balance after it into operation record
        /// </summary>
        public override void Execute()
        {
            if(Executed)
                return;

            _targetAccount.AddToBalance(_amount);
            Executed = true;

            RecordBalanceAfterOperation(_targetAccount.GetBalanceValue());
            _targetAccount.AddOperationToHistory(OperationRecord);
        }
    }
}