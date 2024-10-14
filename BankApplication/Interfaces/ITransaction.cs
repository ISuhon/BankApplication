using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Interfaces
{
    public interface ITransaction
    {
        public string? Payee { get; set; }
        public int ReceiverID { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set; }
    }
}
