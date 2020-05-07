using System.Threading;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PROJECT_NAME.Application.Models;
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
        [Fact]
        public async void GetExampleById_Should_Return_Ok_Result()
        {

            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger>();

            mediatorMock
                .Setup(x => x.Send(It.IsAny<GetExampleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<Example>());
            var controller = new ExampleController(
                loggerMock.Object,
                mediatorMock.Object
            );

            var response = await controller.GetExampleById(1);

            Assert.IsType<OkObjectResult>(response.Result);
            mediatorMock.Verify(x => x.Send(It.IsAny<GetExampleByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once());

        }

        [Fact]
        public async void GetExampleById_Should_Return_Bad_Result_If_Invalid_Input()
        {

            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger>();

            mediatorMock
                .Setup(x => x.Send(It.IsAny<GetExampleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<Example>() { Type = QueryResultTypeEnum.InvalidInput });
            var controller = new ExampleController(
                loggerMock.Object,
                mediatorMock.Object
            );

            var response = await controller.GetExampleById(1);

            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        public async void UpdateExampleNameById_Should_Return_Ok_Result()
        {

            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger>();

            mediatorMock
                .Setup(x => x.Send(It.IsAny<UpdateExampleNameCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CommandResult<bool>());
            var controller = new ExampleController(
                loggerMock.Object,
                mediatorMock.Object
            );

            var response = await controller.UpdateExampleNameById(1, "newName");

            Assert.IsType<OkObjectResult>(response.Result);
            mediatorMock.Verify(x => x.Send(It.IsAny<UpdateExampleNameCommand>(), It.IsAny<CancellationToken>()), Times.Once());

        }

        [Fact]
        public async void UpdateExampleNameById_Should_Return_Bad_Result_If_Invalid_Input()
        {

            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger>();

            mediatorMock
                .Setup(x => x.Send(It.IsAny<UpdateExampleNameCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CommandResult<bool>() { Type = CommandResultTypeEnum.InvalidInput });
            var controller = new ExampleController(
                loggerMock.Object,
                mediatorMock.Object
            );

            var response = await controller.UpdateExampleNameById(1, "newName");

            Assert.IsType<BadRequestResult>(response.Result);
        }
    }
}
