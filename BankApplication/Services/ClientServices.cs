using BankApplication.Interfaces;
using BankApplication.DataBase;
using System.Data.Entity;

namespace BankApplication.Services
{
    public class ClientServices : IClients
    {
        private readonly BankContext _context;

        public ClientServices(BankContext context)
        {
            this._context = context;
        }

        public async Task<IClientData> GetClientById(int id)
        {
            return await this._context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public IEnumerable<IClientData> GetClients()
        {
            return this._context.Clients.AsEnumerable();
        }
    }
}
