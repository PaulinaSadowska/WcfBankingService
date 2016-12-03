using WcfBankingService.account.balance;

namespace WcfBankingService.operation.operations
{
    public interface IOperation
    {
        //TODO - add "was executed" flag (?)
        IBalance Execute();
        string GetInfo();
    }
}
