using System.Threading;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PROJECT_NAME.Application.Models;
using PROJECT_NAME.Domain.Models;
using PROJECT_NAME.Application.Commands.Example;
using PROJECT_NAME.Application.Queries.Example;
using PROJECT_NAME.Api.Controllers;
using Moq;
using Serilog;
using Xunit;

namespace PROJECT_NAME.Api.Tests
{
    public class ExampleControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger> _loggerMock;
        public ExampleControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger>();
        }

        [Fact]
        public async void GetExampleById_ShouldReturnOkResult()
        {
            // ARRANGE     
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetExampleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<Example>());
            var controller = new ExampleController(
                _loggerMock.Object,
                _mediatorMock.Object
            );

            // ACT
            var response = await controller.GetExampleById(1);

            // ASSERT
            Assert.IsType<OkObjectResult>(response.Result);
            _mediatorMock.Verify(x => x.Send(It.IsAny<GetExampleByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void GetExampleById_ShouldReturnNotFoundResult_WhenQueryResultNotFound()
        {
            // ARRANGE     
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetExampleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<Example>() { Type = QueryResultTypeEnum.NotFound });
            var controller = new ExampleController(
                _loggerMock.Object,
                _mediatorMock.Object
            );

            // ACT
            var response = await controller.GetExampleById(1);

            // ASSERT
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void GetExampleById_ShouldReturnBadResult_WhenInvalidInput()
        {
            // ARRANGE     
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetExampleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<Example>() { Type = QueryResultTypeEnum.InvalidInput });
            var controller = new ExampleController(
                _loggerMock.Object,
                _mediatorMock.Object
            );

            // ACT
            var response = await controller.GetExampleById(1);

            // ASSERT
            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        public async void UpdateExampleNameById_ShouldReturnOkResult()
        {
            // ARRANGE      
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<UpdateExampleNameCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CommandResult<bool>());
            var controller = new ExampleController(
                _loggerMock.Object,
                _mediatorMock.Object
            );

            // ACT
            var response = await controller.UpdateExampleNameById(1, "newName");

            // ASSERT
            Assert.IsType<OkObjectResult>(response.Result);
            _mediatorMock.Verify(x => x.Send(It.IsAny<UpdateExampleNameCommand>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void UpdateExampleNameById_ShouldReturnNotFoundResult_WhenCommandResultNotFound()
        {
            // ARRANGE 
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<UpdateExampleNameCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CommandResult<bool>() { Type = CommandResultTypeEnum.NotFound });
            var controller = new ExampleController(
                _loggerMock.Object,
                _mediatorMock.Object
            );

            // ACT
            var response = await controller.UpdateExampleNameById(1, "newName");

            // ASSERT
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void UpdateExampleNameById_ShouldReturnBad_Result_WhenInvalidInput()
        {
            // ARRANGE 
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<UpdateExampleNameCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CommandResult<bool>() { Type = CommandResultTypeEnum.InvalidInput });
            var controller = new ExampleController(
                _loggerMock.Object,
                _mediatorMock.Object
            );

            // ACT
            var response = await controller.UpdateExampleNameById(1, "newName");

            // ASSERT
            Assert.IsType<BadRequestResult>(response.Result);
        }
    }
}
