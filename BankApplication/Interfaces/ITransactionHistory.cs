using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Interfaces
{
    public interface ITransactionHistory
    {
        public List<ITransaction> getTransactions();

        public ITransaction getTransaction(int id);

        public void addTransaction(ITransaction transaction);

        public void showAllTransactions();
    }
}
