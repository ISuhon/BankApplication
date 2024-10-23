namespace BankApplication.Interfaces
{
    public interface IClientService
    {
        public Task<IClientData> GetClientById(int id);

        public IEnumerable<IClientData> GetClients();

        public Task<IClientData> AddNewClient(IClientData client);
    }
}
