using BankApplication.Middleware;
public class Program {

    public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            appBuilder.UseDeveloperExceptionPage();
        }

        // Додаємо наші кастомні middleware в конвеєр
        appBuilder.UseMiddleware<CreditCardMiddlware>();
        appBuilder.UseMiddleware<PostRequest>();

        // Додаємо middleware за замовчуванням (якщо потрібно)
        appBuilder.UseRouting();

        appBuilder.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Головна сторінка");
            });
        });

        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
    
}