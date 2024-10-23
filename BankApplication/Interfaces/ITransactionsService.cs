namespace BankApplication.Interfaces
{
    public interface ITransactionsService
    {
        public ITransactionHistory GetTransactionHistory(ICreditCard creditCard);
    }
}
