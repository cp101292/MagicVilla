using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Configuring the logger configuration using Serilog
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo
//    .File("log/Villas.txt", rollingInterval: RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();

// Add services to the container.
// To enable support for the Application/XMl. 
builder.Services.AddControllers(option => option.ReturnHttpNotAcceptable = true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();


//builder.Services.AddControllers().AddNewtonsoftJson();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
