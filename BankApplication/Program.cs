using BankApplication.Client;
using BankApplication.DataBase;
using BankApplication.Interfaces;
using BankApplication.Middleware;
using BankApplication.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BankContext>(options => options.UseSqlServer("Server=DESKTOP-GN81J6L\\SQLEXPRESS; Database=ConsoleBank; Trusted_Connection=True; TrustServerCertificate=true;"));
builder.Services.AddScoped<IClientService, ClientServices>();
builder.Services.AddScoped<ICreditCard, ClientCreditCard>();
builder.Services.AddScoped<ITransactionsService, TransactionsService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Login"; 
});

builder.Services.AddScoped<ICreditCardService, CreditCardService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ClientMiddleware>();

app.MapGet("/", (HttpContext context) =>
{
    var greetings = context.User.Identity.Name?? "user";
    return $"Hello, {greetings}";
});

app.MapGet("/SignUp", () =>
{
    var html = @"<form method=""post"" action=""/SignUp"">
            <h1>Please sing up</h1>

            <label for=""f-name"">First name:</label><br>
            <input type=""text"" id=""f-name"" name=""f-name""><br>

            <label for=""l-name"">Last name:</label><br>
            <input type=""text"" id=""l-name"" name=""l-name""><br>

            <label for=""m-name"">Middle name:</label><br>
            <input type=""text"" id=""m-name"" name=""m-name""><br>

            <label for=""phone"">Phone:</label><br>
            <input type=""tel"" id=""phone"" placeholder=""999-99-9999"" pattern=""[0-9]{3}-[0-9]{2}-[0-9]{4}"" name=""phone""><br>

            <label for=""mail"">Mail:</label><br>
            <input type=""text"" id=""mail"" name=""mail""><br>

            <label for=""password"">Password:</label><br>
            <input type=""password"" id=""password"" name=""password""><br>

            <button type=""submit"">Sign up</button>
        </form>
        ";

    return Results.Content(html, "text/html");
});

app.MapPost("/SignUp", async (HttpContext context, IClientService clients) =>
{
    var form = await context.Request.ReadFormAsync();

    string firstName = form["f-name"]!;
    string middleName = form["m-name"]!;
    string lastName = form["l-name"]!;
    string phoneNumber = form["phone"]!;
    string email = form["mail"]!;
    string password = form["password"]!;

    var newClient = new ClientData
    {
        FirstName = firstName,
        MiddleName = middleName,
        LastName = lastName,
        PhoneNumber = phoneNumber,
        Email = email,
        Password = password,
        Balance = new ClientBalance()
    };

    await clients.AddNewClient(newClient);
    return Results.Redirect("/Index");
});

app.MapPost("/Login", async (HttpContext httpContext, BankContext bankContext) =>
{
    var form = await httpContext.Request.ReadFormAsync();

    string email = form["mail"]!;
    string password = form["password"]!;

    var client = bankContext.Clients.FirstOrDefault(c => c.Email == email && c.Password == password);

    if (client != null)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, client.Email),
                new Claim("ClientId", client.Id.ToString())
            };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authenticationProperties = new AuthenticationProperties
        {
            IsPersistent = true
        };

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);
    }

    return Results.LocalRedirect("/");
});

app.MapGet("/Login", () =>
{
    var html = @"<form method=""post"" action=""/Login"">
            <h1>Please sing up</h1>

            <label for=""mail"">Mail:</label><br>
            <input type=""text"" id=""mail"" name=""mail""><br>

            <label for=""password"">Password:</label><br>
            <input type=""password"" id=""password"" name=""password""><br>

            <button type=""submit"">Login</button>
        </form>
        ";

    return Results.Content(html, "text/html");
});

app.UseMiddleware<ClientMiddleware>();

app.Run();