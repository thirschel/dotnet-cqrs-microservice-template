using PROJECT_NAME.Application.Models;
using MediatR;

namespace PROJECT_NAME.Application.Commands.Example
{
    public class UpdateExampleNameCommand : IRequest<CommandResult<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}