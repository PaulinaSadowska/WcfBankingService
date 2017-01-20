namespace WcfBankingService.Operation.Operations
{
    /// <summary>
    /// command interface used to separate creation and execution of the operation
    /// </summary>
    public interface IBankCommand
    {
        /// <summary>
        /// Execute bank command
        /// </summary>
        void Execute();
    }
}
