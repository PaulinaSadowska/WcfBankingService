using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.Operation.Operations
{
    public class RestTransfer: BankOperation
    {
        public RestTransfer(IAccount sender, decimal amount, 
            string operationTitle, AccountNumber receiverAccountNumber) : 
            base(receiverAccountNumber, operationTitle, amount, $"RestTransfer to {receiverAccountNumber}")
        {
           
            //TODO - implement
        }

        public override void Execute()
        {
            //MAKE REQUEST
            //if response is 201 created error null - do nothing, everythins is fine
            //if response is error - throw Bank exception with response status
            //
            //TODO - implement
        }
    }
}