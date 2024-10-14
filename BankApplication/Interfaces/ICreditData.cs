using BankApplication.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Interfaces
{
    public interface ICreditData
    {
        public double CreditSum { get; set; }
        public Client.CreditType CreditType { get; set; }
        public Client.CreditStatus CreditStatus { get; set; }
        public DateTime CreditEndDate { get; set; }
    }
}
