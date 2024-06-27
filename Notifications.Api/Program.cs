using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Notifications.Api.Controllers;
using Notifications.Api.Middleware;
using Notifications.API.Configurations;
using System.Net.Http;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptionsConfigs(builder.Configuration);

builder.Services.RegisterServices();
// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
        };
    });
builder.Services.AddScoped<ICorrelationIdSentinel, CorrelationIdSentinel>();
builder.Services.AddHttpClient<IModerna, Moderna>((o,p) =>
{
    var corr = p.GetRequiredService<ICorrelationIdSentinel>();

    o.DefaultRequestHeaders.Add(corr.GetHeaderName(), corr.Get());

    return new Moderna(o);
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<CorrelationIdMiddleware>();

app.Run();
