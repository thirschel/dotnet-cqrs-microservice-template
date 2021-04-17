using PROJECT_NAME.Application.Models;
using MediatR;

namespace PROJECT_NAME.Application.Commands.Readiness
{
    public class PerformReadinessCheckCommand : IRequest<CommandResult<string>>
    {
    }
}
