using BankApplication.Client;

namespace BankApplication.Interfaces
{
    public interface ICreditCardService
    {
        public ClientCreditCard GetCreditCard(string cardNumber);

        public IEnumerable<ICreditCard> GetCreditCards();

        public Task<ICreditCard> AddNewCreditCard(ICreditCard client);

        public ClientCreditCard GetCreditCardForClient(int clientId, int item);

        public ClientCreditCard GetCreditCardForClient(int clientId);
    }
}
