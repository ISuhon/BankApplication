using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Interfaces
{
    public interface ICredits : IEnumerable<ICredits>
    {
        List<ICreditData> getCredits();
        public void addCredit(ICreditData credit);

        public ICreditData getCreditByID(int ID);
        public void showCredits();
    }
}
