using Microsoft.AspNetCore.Mvc;
using BankApplication.Interfaces;
using BankApplication.Client;
using BankApplication.DataBase;

namespace BankApplication.WebPages.Controllers
{
    public class RegisterController : Controller
    {
        private readonly BankContext _context;

        public RegisterController(BankContext context)
        {
            this._context = context;
        }
        public IActionResult Register(IClientData client)
        {
            if (_context.Clients.FirstOrDefault(c => c.PhoneNumber == client.PhoneNumber) != null)
            {
                ModelState.AddModelError(string.Empty, "This phone number is already register");
                return View(client);
            }

            if (_context.Clients.FirstOrDefault(c => c.Email == client.Email) != null)
            {
                ModelState.AddModelError(string.Empty, "This email number is already register");
                return View(client);
            }

            var newClient = new ClientData
            {
                FirstName = client.FirstName,
                MiddleName = client.MiddleName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                ClientBalance = client.ClientBalance
            };

            _context.Clients.Add(newClient);
            _context.SaveChanges();

            return RedirectToAction("SignUp");
        }
    }
}
