using BankApplication.Middleware;
public class Program {

    public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            appBuilder.UseDeveloperExceptionPage();
        }

        // ������ ���� ������� middleware � ������
        appBuilder.UseMiddleware<CreditCardMiddlware>();
        appBuilder.UseMiddleware<PostRequest>();

        // ������ middleware �� ������������� (���� �������)
        appBuilder.UseRouting();

        appBuilder.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("������� �������");
            });
        });

        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
    
}