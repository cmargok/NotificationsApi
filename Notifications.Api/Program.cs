using Azure.Identity;
using Notifications.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

var Enviroment = builder.Environment.IsDevelopment(); 

var keyVaultEndpoint = Environment.GetEnvironmentVariable("KeyVaultUrl")!.ToString();

builder.Services.AddOptionsConfigs(builder.Configuration, Enviroment);

builder.Services.RegisterServices();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
