using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BankApplication.Client;
using BankApplication.Interfaces;

namespace BankApplication.WebPages
{
    public class SignUpModel : PageModel
    {
        private readonly IClients _clients;
        SignUpModel(IClients clients) 
        {
            this._clients = clients;
        }
        public IEnumerable<IClientData> clients { get; set; } = null;
        public async Task AsyncOnGet()
        {
            clients = this._clients.GetClients();
        }
    }
}
