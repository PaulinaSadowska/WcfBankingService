using System.Collections.Generic;
using WcfBankingService.account.balance;
using WcfBankingService.account.number;
using WcfBankingService.operation;

namespace WcfBankingService.account
{
    public class Account
    {
        private readonly AccountNumber _accountNumber;
        private readonly IBalance _balance;
        private IEnumerable<OperationRecord> _operationHistory;

        public Account(AccountNumber accountNumber, IBalance balance)
        {
            this._accountNumber = accountNumber;
            this._balance = balance;
        }
    }
}