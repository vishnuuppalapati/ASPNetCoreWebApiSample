using FidoClientSdk.Clients;
using FidoClientSdk.Interfaces;
using Serilog;

var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
Directory.CreateDirectory(logDirectory); // Ensure Logs folder exists

var logFilePath = Path.Combine(logDirectory, "log.txt");

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Infinite, shared: true)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IFidoApiClient, FidoApiClient>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Only needed if you're using cookies or credentials
        });
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    Log.Information("Request: {Method} {Path}", context.Request.Method, context.Request.Path);
    await next.Invoke();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
