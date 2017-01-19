using WcfBankingService.Accounts;

namespace WcfBankingService.Operation.Operations
{
    public class RollbackTransferFee : Deposit
    {

        public RollbackTransferFee(IPublicAccount account, decimal transferFeeValue, string receiverAccountNumber)
            : base(account, transferFeeValue, $"fee refund for transfer to {receiverAccountNumber}", "Bank 00112169")
        {

        }
    }
}