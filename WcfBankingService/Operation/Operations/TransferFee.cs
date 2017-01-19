using WcfBankingService.Accounts;
using WcfBankingService.operation.operations;

namespace WcfBankingService.Operation.Operations
{
    public class TransferFee : Withdraw
    {

        public TransferFee(IAccount account, decimal transferFeeValue, string receiverAccountNumber)
            : base(account, transferFeeValue, $"fee for transfer to {receiverAccountNumber}", "Bank 00112169")
        {

        }
    }
}