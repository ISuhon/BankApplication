using BankApplication.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Interfaces
{
    public interface IClientBalance
    {
        public IListofCreditCards? creditCards { get; set; }
        public ICredits? Credits { get; set; }
        public string? Surname { get; set; }

        public void setSurname(string? surname)
        {
            this.Surname = surname;
        }
    }
}
