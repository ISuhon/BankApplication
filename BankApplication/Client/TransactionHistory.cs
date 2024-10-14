using BankApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Client
{
    public class TransactionHistory : ITransactionHistory
    {
        List<ITransaction> transactions = new List<ITransaction>();

        public List<ITransaction> getTransactions() 
        { 
            return this.transactions; 
        }

        public ITransaction getTransaction(int id) 
        {
            if(id > transactions.Count)
            {
                throw new IndexOutOfRangeException("This client doesn't have that many transactions." +
                    "Please input lesser number");
            }

            return transactions[id];
        }

        public void showAllTransactionHistory()
        {
            for (int i = 0; i < transactions.Count; i++) 
            {
                Console.WriteLine(transactions[i].ToString());
            }
        }

        public void addTransaction(ITransaction transaction)
        {
            transactions.Add(transaction);
        }

        public void showAllTransactions()
        {
            foreach (ITransaction transaction in transactions)
            {
                Console.WriteLine(transaction.ToString());
                Console.WriteLine("===========================\n");
            }
        }
    }
}
