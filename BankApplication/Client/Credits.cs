using BankApplication.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Client
{
    public class Credits : ICredits
    {
        private IEnumerable<ICredits> Events()
        {
            yield return new Credits();
        }

        public IEnumerator<ICredits> GetEnumerator()
        {
            return Events().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private List<ICreditData> credits = new List<ICreditData>();

        public List<ICreditData> getCredits()
        {
            return this.credits;
        }

        public ICreditData getCreditByID(int ID)
        {
            if (ID > this.credits.Capacity) 
            {
                throw new IndexOutOfRangeException("This client doesn't have that many credits. " +
                    "Please input lesser number");
            }

            return this.credits[ID];
        }

        public void showCredits()
        {
            foreach(var credit in this.credits)
            {
                Console.WriteLine(credit.ToString());
                Console.WriteLine("===========================\n");
            }
        }

        public void addCredit(ICreditData credit) 
        {
            this.credits.Add(credit);
        }
    }
}
