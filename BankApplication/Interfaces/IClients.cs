namespace BankApplication.Interfaces
{
    public interface IClients
    {
        public Task<IClientData> GetClientById(int id);

        public IEnumerable<IClientData> GetClients();
    }
}
