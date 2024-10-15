using BankApplication.Middleware;
using BankApplication.Client;
using BankApplication.DataBase;
using BankApplication.Interfaces;
using BankApplication.Migrations;
using BankApplication.Services;
using BankApplication.WebPages;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddServices();

var app =  builder.Build();

app.MapGet("/", () =>
{
    return Results.Redirect("Index");
});

app.MapPost("/SignUp", async (HttpContext context, IClients clients) =>
{
    var form = await context.Request.ReadFormAsync();

    string firstName = form["FirstName"]!;
    string middleName = form["MiddleName"]!;
    string lastName = form["LastName"]!;
    string phoneNumber = form["PhoneNumber"]!;
    string email = form["Email"]!;

    var newClient = new ClientData
    {
        FirstName = firstName,
        MiddleName = middleName,
        LastName = lastName,
        PhoneNumber = phoneNumber,
        Email = email,
        Balance = new ClientBalance()
    };

    await clients.AddNewClient(newClient);

    return Results.Redirect("/Index");
});

app.UseMiddleware<CreditCardMiddlware>();

app.MapRazorPages();

app.Run();