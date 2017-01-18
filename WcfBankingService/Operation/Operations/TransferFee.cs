using WcfBankingService.Accounts;
using WcfBankingService.operation.operations;

namespace WcfBankingService.Operation.Operations
{
    public class TransferFee : Withdraw
    {
        private const decimal TransferFeeValue = 0.1m;
        public TransferFee(IAccount account, string receiverAccountNumber)
            : base(account, TransferFeeValue, $"fee for transfer to {receiverAccountNumber}", "Bank 00112169")
        {

        }
    }
}