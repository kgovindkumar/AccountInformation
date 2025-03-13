using AccountService.BusinessLogic;
using AccountService.Interface;
using AccountService.Model;
using AccountService.Services;
using Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var secret = "This is my private key for bank application";
var key = Encoding.ASCII.GetBytes(secret);
// Add services to the container.

builder.Services.AddControllers();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var consulConfig = builder.Configuration.GetSection("ConsulConfig").Get<ConsulConfig>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAccountManager, AccountManager>();
builder.Services.AddSingleton<ITransactionManager, TransactionManager>();
builder.Services.AddSingleton<IAuthenticationManager>(new AuthenticateManager("This is my keyghjghjghgggggggggggggggggggg"));
builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
{
    consulConfig.Address = new Uri(consulConfig.Address.ToString());
}));

builder.Services.AddHostedService<ConsulHostedService>();

builder.Services.AddAuthentication(
    x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options => {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false
        };

    });
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

app.Run();
