using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Notifications.Application.Email;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Email.Contracts.Factory;
using Notifications.Application.Models.Email.Settings;
using Notifications.Infraestruture.Email;
using System.Text;

namespace Notifications.API.Configurations
{
    public static class AuthConfigs
    {
        public static IServiceCollection AddAuthJwt(this IServiceCollection services, IConfiguration config)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["JwtSettings:Issuer"],
                        ValidAudience = config["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!))
                    };
                });

            return services;

        }

        
    }

    public static class DependencyInjectionExtensions
    {        
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            //email manager
            services.AddScoped<IEmailManager, EmailManager>(provider =>
            {
                var mailservice = Application.Utils.EnumMailServices.NetMail;

                var Options = provider.GetRequiredService<IOptions<CredentialsKeySettings>>().Value;

                var instance = new EmailManager(
                    provider.GetRequiredService<IEmailFactory>(), 
                    Options.Services.FirstOrDefault(o => o.ServiceName == mailservice.ToString())!, 
                   // provider.GetRequiredService<ISecretsManager>(),
                    mailservice
                    );

                return instance;
            });

            //factory to instance the mailservice library to use
            services.AddScoped<IEmailFactory, EmailFactory>();

            //azure secrets
           // services.AddScoped<ISecretsManager, SecretsManager>();
            return services;
        }
    }

    public static class OptionsConfigs
    {
        public static IServiceCollection AddOptionsConfigs(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddOptions<CredentialsKeySettings>()
                .BindConfiguration("MailSettings")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            //
            //services.Configure<CredentialsKeySettings>(config.GetSection("MailSettings"));
            return services;
        }
    }
}