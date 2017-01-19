using WcfBankingService.Accounts;
using WcfBankingService.Database.SavingData;
using WcfBankingService.operation.Complex;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService
{
    /// <summary>
    /// Used fo erform operations and save them using given data inserter
    /// </summary>
    public class PersistantExecutor
    {
        private readonly IBankDataInserter _dataInserter;

        public PersistantExecutor(IBankDataInserter dataInserter)
        {
            _dataInserter = dataInserter;
        }

        /// <summary>
        /// Execute complex command; save operation records and receiver and sender acount balance
        /// </summary>
        /// <param name="complexCommand">complex command to execute and save</param>
        /// <param name="sender">sender account</param>
        /// <param name="receiver">receiver account</param>
        public void ExecuteAndSave(ComplexCommand complexCommand, IPublicAccount sender, IPublicAccount receiver)
        {
            ExecuteAndSave(complexCommand, sender);
            _dataInserter.SaveAccountBalance(receiver);
        }

        /// <summary>
        /// Execute complex command; save operation records and sender acount balance
        /// </summary>
        /// <param name="complexCommand">complex command to execute and save</param>
        /// <param name="sender">sender account</param>
        public void ExecuteAndSave(ComplexCommand complexCommand, IPublicAccount sender)
        {
            complexCommand.Execute();
            _dataInserter.SaveOperations(complexCommand.GetExecutedOperations());
            _dataInserter.SaveAccountBalance(sender);
        }

        /// <summary>
        /// Execute operation, save it and save account operation balance afterwards
        /// </summary>
        /// <param name="operation">operation to execute and save</param>
        /// <param name="account">account to save</param>
        public void ExecuteAndSave(BankOperation operation, IPublicAccount account)
        {
            operation.Execute();
            _dataInserter.SaveOperation(operation);
            _dataInserter.SaveAccountBalance(account);
        }
    }
}