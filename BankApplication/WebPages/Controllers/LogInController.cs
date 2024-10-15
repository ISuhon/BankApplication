using BankApplication.Client;
using BankApplication.DataBase;
using BankApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.WebPages.Controllers
{
    public class LogInController : Controller
    {
        private readonly BankContext _context;

        public LogInController(BankContext context)
        {
            this._context = context;
        }
        public IActionResult LogIn(IClientData client)
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
