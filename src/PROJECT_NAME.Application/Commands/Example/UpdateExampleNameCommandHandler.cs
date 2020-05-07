using System.Threading;
using System.Threading.Tasks;
using PROJECT_NAME.Application.Interfaces;
using PROJECT_NAME.Application.Models;
using FluentValidation;
using MediatR;
using Serilog;


namespace PROJECT_NAME.Application.Commands.Example
{
    public class UpdateExampleNameCommandHandler : IRequestHandler<UpdateExampleNameCommand, CommandResult<bool>>
    {
        private readonly IValidator<UpdateExampleNameCommand> _validator;
        private readonly IExampleRepository _exampleRepository;
        private readonly ILogger _logger;

        public UpdateExampleNameCommandHandler(
            ILogger logger,
            IExampleRepository exampleRepository,
            IValidator<UpdateExampleNameCommand> validator)
        {
            _logger = logger;
            _exampleRepository = exampleRepository;
            _validator = validator;
        }

        public async Task<CommandResult<bool>> Handle(UpdateExampleNameCommand command, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(command);

            if (!validation.IsValid)
            {
                _logger.Error("Update Example Name Command with id: {id} produced errors on validation {Errors}", command.Id, validation.ToString());
                return new CommandResult<bool>(result: false, type: CommandResultTypeEnum.InvalidInput);
            }
            var rowsAffected = await _exampleRepository.UpdateExampleNameById(command.Id, command.Name);

            if (rowsAffected == 0)
            {
                return new CommandResult<bool>(result: false, type: CommandResultTypeEnum.NotFound);
            }
            return new CommandResult<bool>(result: true, type: CommandResultTypeEnum.Success);
        }
    }
}