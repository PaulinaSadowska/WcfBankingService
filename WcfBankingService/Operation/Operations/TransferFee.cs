using WcfBankingService.Accounts;
using WcfBankingService.operation.operations;

namespace WcfBankingService.Operation.Operations
{
    /// <summary>
    /// bank fee executed during interbank transfer
    /// </summary>
    public class TransferFee : Withdraw
    {
        /// <param name="account">sender account number</param>
        /// <param name="transferFeeValue">fee value</param>
        /// <param name="receiverAccountNumber">receiver account number</param>
        public TransferFee(IAccount account, decimal transferFeeValue, string receiverAccountNumber)
            : base(account, transferFeeValue, $"fee for transfer to {receiverAccountNumber}", "Bank 00112169")
        {

        }
    }
}