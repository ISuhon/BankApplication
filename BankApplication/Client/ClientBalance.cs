using BankApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApplication.Client
{
    public class ClientBalance : BankApplication.Interfaces.IClientBalance
    {
        [NotMapped]
        public IListofCreditCards? creditCards { get; set; }
        public string? Surname { get; set; }

        [NotMapped]
        public ICredits? Credits { get; set; }

        [Key]
        [ForeignKey("ClientData")]
        public int Id { get; set; } // Primary key

        [NotMapped]
        public IClientData? ClientData { get; set; }

        public List<ClientCreditCard>? CreditCardsForDB { get; set; }
        public List<CreditData>? CreditsForDB { get; set; }

        public ClientBalance()
        {
            this.CreditCardsForDB = creditCards?.getCreditCards().Cast<ClientCreditCard>().ToList();
            this.CreditsForDB = Credits?.getCredits().Cast<CreditData>().ToList();
        }

        public void setSurname(string? surname)
        {
            this.Surname = surname;
        }

        public override string ToString()
        {
            return $"ID : {this.Id}\n" +
                $"Surname : {this.Surname}\n" +
                $"\n========================\n";
        }
    }
}
