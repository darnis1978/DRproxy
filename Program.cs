using DRproxy.Hubs;
using DRproxy.Services;
using DRproxy.Services.PEPCO;
using DRproxy.Helper;


internal class Program
{
    private static void Main(string[] args)
    {
        var clientId = ConsoleHelper.CheckArguments(args);
        if (clientId == null) return;
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();


        // Add services to the container.
        builder.Services.AddSignalR();


        // Add other services
        builder.Services.AddSingleton<IMemeoryStorageService, MemeoryStorageService>();
        switch(clientId)
        {
            case "pepco":
                builder.Services.AddSingleton<IDigitalReceiptService, DigitalReceiptService_PEPCO>();
            break;
        }

        // Add CORS pplicy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
        });

        // Add Controlers
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("CORSPolicy");
        app.UseRouting();
        app.UseAuthorization();
        // Configure SignalR endpoint
        app.MapHub<MessageHub>("/connectort");
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}