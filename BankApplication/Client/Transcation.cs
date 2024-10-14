using BankApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BankApplication.Client
{
    public class Transaction : ITransaction
    {
        internal event Message _message;
        public Transaction(string? payee, int receiverID, DateTime transactionDate, double transactionAmount) 
        {
            this.Payee = payee;
            this.ReceiverID = receiverID;
            this.TransactionDate = transactionDate; 
            this.TransactionAmount = transactionAmount;

            //this._message += MessageOfSuccessfulTransaction;
            //this._message("Transaction done successful");
        }
        public string? Payee { get; set; }
        public int ReceiverID { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set;}
        public int CreditCardID { get; set; } // Foreign key
        public ClientCreditCard ClientCreditCard { get; set; }

        [Key]
        public int Id { get; set; } // Primary key

        public override string? ToString()
        {
            return $"Payee : {Payee}\n" +
                $"ReceiverID : {ReceiverID}\n" +
                $"Transaction Date : {TransactionDate}\n" +
                $"Transaction Amount : {TransactionAmount}\n" +
                $"========================\n";
        }

        void MessageOfSuccessfulTransaction(string message) => Console.WriteLine(message);
    }
}
