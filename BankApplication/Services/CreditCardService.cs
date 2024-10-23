using BankApplication.Client;
using BankApplication.DataBase;
using BankApplication.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly BankContext _context;

        public CreditCardService(BankContext context, ITransactionsService transactionsService)
        {
            this._context = context;
            this._transactionsService = transactionsService;
        }

        private ITransactionsService _transactionsService;

        public async Task<ICreditCard> AddNewCreditCard(ICreditCard creditCard)
        {
            var newCreditCard = new ClientCreditCard
            {
                CardNumber = creditCard.CardNumber,
                ExpirationDate = creditCard.ExpirationDate,
                CVVcode = creditCard.CVVcode,
                Fortune = creditCard.Fortune,
                PIN = creditCard.PIN,
                transactions = new TransactionHistory()
            };

            await this._context.CreditCards.AddAsync(newCreditCard);
            await this._context.SaveChangesAsync();

            return newCreditCard;
        }

        public ClientCreditCard GetCreditCard(string cardNumber)
        {
            return this._context.CreditCards.FirstOrDefault(c => c.CardNumber == cardNumber)!;
        }

        public ClientCreditCard GetCreditCardForClient(int clientId, int item)
        {
            var creditCards = this._context.Clients.FirstOrDefault(c => c.Id == clientId)!.ClientBalance!.creditCards!.getCreditCards();

            if (item > creditCards.Count || item < 0)
            {
                throw new IndexOutOfRangeException("item is have invalid value");
            }

            var desiredCreditCard = creditCards[item];

            this._transactionsService = new TransactionsService(this._context);

            return new ClientCreditCard
            {
                CardNumber = desiredCreditCard.CardNumber,
                CVVcode = desiredCreditCard.CVVcode,
                PIN = desiredCreditCard.PIN,
                transactions = this._transactionsService.GetTransactionHistory(desiredCreditCard)
            };

        }

        public ClientCreditCard GetCreditCardForClient(int clientId)
        {
            var creditCards = this._context.Clients.Include(c => c.Balance).ThenInclude(b => b.CreditCardsForDB).FirstOrDefault(c => c.Id == clientId)!.Balance!.CreditCardsForDB;

            return creditCards.First();
        }

        public IEnumerable<ICreditCard> GetCreditCards()
        {
            throw new NotImplementedException();
        }
    }
}
