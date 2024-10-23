using BankApplication.Client;
using BankApplication.DataBase;
using BankApplication.Interfaces;

namespace BankApplication.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly BankContext _bankContext;

        public TransactionsService (BankContext bankContext)
        {
            this._bankContext = bankContext;
        }

        public ITransactionHistory GetTransactionHistory(ICreditCard creditCard)
        {
            return this._bankContext.CreditCards.FirstOrDefault(c => c.CardNumber == creditCard.CardNumber)!.transactions!;
        }
    }
}
