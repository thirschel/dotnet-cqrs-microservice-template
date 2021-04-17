using System.Threading;
using System.Threading.Tasks;
using PROJECT_NAME.Application.Models;
using MediatR;
using Microsoft.Extensions.Options;
using Serilog;


namespace PROJECT_NAME.Application.Commands.Readiness
{
    public class PerformReadinessCheckCommandHandler : IRequestHandler<PerformReadinessCheckCommand, CommandResult<string>>
    {
        private readonly EnvironmentConfiguration _configuration;
        private readonly ILogger _logger;

        public PerformReadinessCheckCommandHandler(
            ILogger logger,
            IOptions<EnvironmentConfiguration> configuration)
        {
            _logger = logger;
            _configuration = configuration.Value;
        }

        public async Task<CommandResult<string>> Handle(PerformReadinessCheckCommand command, CancellationToken cancellationToken)
        {
            // Not using a validator here because we want to dynamically check the environment variables that are added or removed
            var type = _configuration.GetType();

            foreach (var property in type.GetProperties())
            {
                if (property.GetValue(_configuration, null) == null)
                {
                    return new CommandResult<string>(result: $"Configuration Error. Property {property.Name} is null.", type: CommandResultTypeEnum.InvalidInput);
                }
            }
            // Here we can check if we can connect to the database or other dependent services

            return new CommandResult<string>(result: string.Empty, type: CommandResultTypeEnum.Success);
        }
    }
}
