using Lamar;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PROJECT_NAME.Application.Models;
using PROJECT_NAME.Application.Commands.Example;
using PROJECT_NAME.Application.Queries.Example;

namespace PROJECT_NAME.Api.Configurations.Extensions
{
    public static class DependencyInjectionConfigurationExtensions
    {
        internal static void AddDependencyInjection(this ServiceRegistry services, IConfiguration configuration)
        {
            // Map the environment variables to an object that represents them
            ((IServiceCollection)services).Configure<EnvironmentConfiguration>(configuration);

            // https://jasperfx.github.io/lamar/documentation/ioc/registration/auto-registration-and-conventions/
            services.Scan(_ =>
            {
                _.TheCallingAssembly();
                _.Assembly("PROJECT_NAME.Application");
                _.Assembly("PROJECT_NAME.Infrastructure");
                _.AddAllTypesOf<IValidator>();
                _.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                _.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                _.WithDefaultConventions();
                _.LookForRegistries();
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Startup).Assembly));
        }
    }
}
