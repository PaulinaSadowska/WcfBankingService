using WcfBankingService.Accounts;

namespace WcfBankingService.Operation.Operations
{
    public class RollbackTransferFee : Deposit
    {
        private const decimal TransferFeeValue = 0.1m;
        public RollbackTransferFee(IPublicAccount account, string receiverAccountNumber)
            : base(account, TransferFeeValue, $"fee refund for transfer to {receiverAccountNumber}", "Bank 00112169")
        {

        }
    }
}