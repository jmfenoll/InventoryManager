using InventoryManager.Application;
using InventoryManager.Infraestructure;
using InventoryManager.Infraestructure.CommandsRepository;
using InventoryManager.Infraestructure.QueriesRepository;
using InventoryManager.WebApi;
using InventoryManager.WebApi.Secure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddTransient<IInventoryApplication, InventoryApplication>();
builder.Services.AddTransient<IInventoryQueriesRepository, InventoryQueriesRepository>();
builder.Services.AddTransient<IInventoryCommandsRepository, InventoryCommandsRepository>();
builder.Services.AddSingleton<InventoryContext>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services
    .AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });





builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("BasicAuthentication",new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build());
}); 


builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Inventory Manager",
        Description = "An Application from manage inventory for multiple warehouses.",
        TermsOfService = new Uri("https://github.com/jmfenoll/InventoryManager"),
        Contact = new OpenApiContact
        {
            Name = "Contact me",
            Url = new Uri("https://github.com/jmfenoll/InventoryManager")
        }
    });


    option.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter user and password",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="basic"
                }
            },
            new string[]{}
        }
    });

    var xmlFilename=$"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


Log.Logger = new LoggerConfiguration().WriteTo.File(
    "..\\logs\\log.txt",
    rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.LoadInitialData();

app.Run();



// Make the implicit Program class public so test projects can access it
public partial class Program { }