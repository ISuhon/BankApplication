using BankApplication.Interfaces;
using BankApplication.DataBase;
using BankApplication.Client;
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

        public async Task<IClientData> AddNewClient(IClientData client)
        {
            var newClient = new ClientData
            {
                FirstName = client.FirstName,
                MiddleName = client.MiddleName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                Balance = new ClientBalance()
            };

            await this._context.Clients.AddAsync(newClient);
            await this._context.SaveChangesAsync();

            return newClient;
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
