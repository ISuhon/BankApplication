using BankApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Client
{
    public class CreditData : ICreditData
    {
        internal event Message _message;
        public CreditData(double creditSum, CreditType creditType, CreditStatus creditStatus, DateTime creditEndDate)
        {
            this.CreditSum = creditSum;
            this.CreditType = creditType;
            this.CreditStatus = creditStatus;
            this.CreditEndDate = creditEndDate;

            //this._message += MessageOfCreatedCredit;
            //this._message("Created credit : " + this);
        }
        public double CreditSum { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public CreditType CreditType { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public CreditStatus CreditStatus { get; set; }
        public DateTime CreditEndDate { get; set;  }
        public int BalanceID { get; set; } // Foreign key

        [Key]
        public int Id { get; set; } // Primary key
        public ClientBalance Balance { get; set; }

        public override string? ToString()
        {
            return $"Credit sum: {CreditSum}\n" + 
                $"Credit type: {CreditType.ToString()}\n" +
                $"Credit status: {CreditStatus.ToString()}\n" + 
                $"Credit end date: {CreditEndDate}\n" +
                $"========================\n";
        }

        void MessageOfCreatedCredit(string message) => Console.WriteLine(message);
    }

    public enum CreditType
    {
        REVOLVING_CREDIT, INSTALLMENT_CREDIT, OPEN_CREDIT
    }

    public enum CreditStatus
    {
        ACTIVE, OVERDUE, CLOSED
    }
}
