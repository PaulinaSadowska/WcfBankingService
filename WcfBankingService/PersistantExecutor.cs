using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfBankingService.Accounts;
using WcfBankingService.Database.SavingData;
using WcfBankingService.operation.Complex;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService
{
    public class PersistantExecutor
    {
        private readonly IBankDataInserter _dataInserter;

        public PersistantExecutor(IBankDataInserter dataInserter)
        {
            _dataInserter = dataInserter;
        }

        public void ExecuteAndSave(ComplexCommand complexCommand, IPublicAccount sender, IPublicAccount receiver)
        {
            ExecuteAndSave(complexCommand, sender);
            _dataInserter.SaveAccountBalance(receiver);
        }

        public void ExecuteAndSave(ComplexCommand complexCommand, IPublicAccount sender)
        {
            complexCommand.Execute();
            _dataInserter.SaveOperations(complexCommand.GetExecutedOperations());
            _dataInserter.SaveAccountBalance(sender);
        }

        public void ExecuteAndSave(BankOperation operation, IPublicAccount account)
        {
            operation.Execute();
            _dataInserter.SaveOperation(operation);
            _dataInserter.SaveAccountBalance(account);
        }
    }
}