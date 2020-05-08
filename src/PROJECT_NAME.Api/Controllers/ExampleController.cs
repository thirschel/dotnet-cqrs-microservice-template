using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PROJECT_NAME.Application.Models;
using PROJECT_NAME.Domain.Models;
using PROJECT_NAME.Application.Commands.Example;
using PROJECT_NAME.Application.Queries.Example;
using MediatR;
using Serilog;

namespace PROJECT_NAME.Api.Controllers
{
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public class ExampleController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public ExampleController(
            ILogger logger,
            IMediator mediator
        )
        {
            _logger = logger;
            _mediator = mediator;
        }

        /*
            ProducesResponseType helps Swagger be more verbose about what endpoints can return.
            Summary - Short description about the endpoint which will show on the Swagger pill next to the name of the endpoint
            Remarks - Long description or explanation about the endpoint which will show once the Swagger pill is opened
            Param - name attribute needs to match the method parameter name and will add a description column to each parameter in Swagger
        */

        /// <summary>
        /// Get an Example by it's ID
        /// </summary>
        /// <remarks>
        /// Retrieves an Example by the ID specified
        /// </remarks>
        /// <param name="id">ID of the example</param>
        [HttpGet]
        [ApiVersion("1")]
        [ApiExplorerSettings(GroupName = "v1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("examples/{id}")]
        public async Task<ActionResult<Example>> GetExampleById([FromRoute] int id)
        {
            var getExampleByIdQuery = new GetExampleByIdQuery()
            {
                Id = id,
            };
            /*
                Mediatr is not needed for an application to implement CQRS but it helps to 
                implement and conform to the CQRS model.
                While async in nature, performance is on par with other DI approaches (https://medium.com/swlh/dependency-injection-v-mediatr-a-simple-c-benchmark-32630ff864ea)
            */
            var result = await _mediator.Send(getExampleByIdQuery);

            if (result.Type == QueryResultTypeEnum.InvalidInput)
            {
                return new BadRequestResult();
            }

            if (result.Type == QueryResultTypeEnum.NotFound)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result.Result);
        }

        /// <summary>
        /// Update an Example's name by it's ID
        /// </summary>
        /// <remarks>
        /// Updates the Name of any Examples with the ID specified to the name specified
        /// </remarks>
        /// <param name="id">ID of the example</param>
        /// <param name="name">The new name for the Example</param>
        [HttpPut]
        [ApiVersion("2")]
        [ApiExplorerSettings(GroupName = "v2")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("{id}")]
        public async Task<ActionResult<bool>> UpdateExampleNameById([FromRoute] int id, [FromBody] string name)
        {
            var updateExampleNameCommand = new UpdateExampleNameCommand()
            {
                Id = id,
                Name = name,
            };
            var result = await _mediator.Send(updateExampleNameCommand);

            if (result.Type == CommandResultTypeEnum.InvalidInput)
            {
                return new BadRequestResult();
            }

            if (result.Type == CommandResultTypeEnum.NotFound)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result.Result);
        }

    }
}
