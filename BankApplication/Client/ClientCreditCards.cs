using BankApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Client
{
    public class ClientCreditCards : IListofCreditCards
    {
        private List<ICreditCard> creditCards = new List<ICreditCard>();

        public List<ICreditCard> getCreditCards()
        {
            return this.creditCards;
        }

        public void showCreditCards()
        {
            if (creditCards.Count == 0)
            {
                Console.WriteLine("Dear client you didn't add any credit card yet.");
            }

            foreach (var card in creditCards)
            {
                Console.WriteLine(card.ToString());
                Console.WriteLine("===========================\n");
            }
        }

        public void addCreditCard(ICreditCard creditCard)
        {
            this.creditCards.Add(creditCard);
        }

        public ICreditCard getCreditCard(int index)
        {
            return this.creditCards[index];
        }
    }
}
