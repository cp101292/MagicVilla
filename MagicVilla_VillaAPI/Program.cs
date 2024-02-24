using System.Text;
using MagicVilla_VillaAPI;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Configuring the logger configuration using Serilog
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo
//    .File("log/Villas.txt", rollingInterval: RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddTransient<IVillaRepository, VillaRepository>();
builder.Services.AddTransient<IVillaNumberRepository, VillaNumberRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
//Any number of mapping can reside inside the Mapping config file. 
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddResponseCaching();

builder.Services.AddApiVersioning(
    option=>
    {
        option.AssumeDefaultVersionWhenUnspecified = true;
        option.DefaultApiVersion = new ApiVersion(1, 0);
        option.ReportApiVersions = true;
    });

builder.Services.AddVersionedApiExplorer(
    option =>
    {
        option.GroupNameFormat = "'v'VVV";
        option.SubstituteApiVersionInUrl = true;
    });

var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


// Add services to the container.
// To enable support for the Application/XMl. 
builder.Services.AddControllers(option =>
{
    option.CacheProfiles.Add("Default30",
        new CacheProfile()
        {
            Duration = 30
        });
    //  option => option.ReturnHttpNotAcceptable = true
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();


//builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
            "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement( new OpenApiSecurityRequirement()
    {
        {   
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
        
    });
    options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version   = "v1.0", 
            Title = "Magic Villa v1",
            Description = "API to manage villa = v1.0",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "DotNetLearning",
                Url = new Uri("https://mylearning.com")
            },

            License = new OpenApiLicense
            {
                Name = "Example license",
                Url = new Uri("https://example.com/licence")
            }
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2.0",
        Title = "Magic Villa V2",
        Description = "API to manage villa V2",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "DotNetLearning",
            Url = new Uri("https://mylearning.com")
        },

        License = new OpenApiLicense
        {
            Name = "Example license",
            Url = new Uri("https://example.com/licence")
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        option =>
        {
            option.SwaggerEndpoint("/swagger/v1/swagger.json", "Magic_VillaV1");
            option.SwaggerEndpoint("/swagger/v2/swagger.json", "Magic_VillaV2");
        });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
