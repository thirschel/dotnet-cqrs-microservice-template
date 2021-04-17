using System;
using System.Threading;
using PROJECT_NAME.Infrastructure.ExampleService;
using Moq;
using Serilog;
using Xunit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PROJECT_NAME.Domain.Models;
using Microsoft.Extensions.Options;
using Moq.Protected;
using Newtonsoft.Json;
using AutoMapper;
using PROJECT_NAME.Application.Models;

namespace PROJECT_NAME.Infrastructure.Tests.ExampleService
{
    public class ExampleServiceClientTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<ILogger> _loggerMock;
        private readonly Mock<HttpMessageHandler> _handlerMock;
        public ExampleServiceClientTests()
        {
            _loggerMock = new Mock<ILogger>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        }

        [Fact]
        public async void GetExampleById_CallsCorrectUrl()
        {
            // ARRANGE
            _handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new Example())),
                })
                .Verifiable();

            _httpClientFactoryMock.Setup(x => x.CreateClient(string.Empty)).Returns(new HttpClient(_handlerMock.Object));

            var environmentConfiguration = Options.Create(new EnvironmentConfiguration()
            {
                SERVICE_URL = "https://localhost"
            });
            var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<ExampleProfile>(); });
            var mapper = new Mapper(configuration);

            var exampleServiceClient = new ExampleServiceClient(
                _loggerMock.Object, 
                mapper,
                environmentConfiguration,
                _httpClientFactoryMock.Object);
            var exampleId = 1;
 
            // ACT
            await exampleServiceClient.GetExampleById(exampleId);

            // ASSERT
            var expectedUri = new Uri($"https://localhost/api/v1/examples/{exampleId}");

            _handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get
                        && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async void GetExampleById_ThrowsException_WhenRequestIsNotSuccessful()
        {
            // ARRANGE
            _handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Big bad thing happened."),
                    RequestMessage = new HttpRequestMessage()
                })
                .Verifiable();

            _httpClientFactoryMock.Setup(x => x.CreateClient(string.Empty)).Returns(new HttpClient(_handlerMock.Object));

            var environmentConfiguration = Options.Create(new EnvironmentConfiguration()
            {
                SERVICE_URL = "https://localhost"
            });
            var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<ExampleProfile>(); });
            var mapper = new Mapper(configuration);

            var exampleServiceClient = new ExampleServiceClient(
                _loggerMock.Object,
                mapper,
                environmentConfiguration,
                _httpClientFactoryMock.Object);
            var exampleId = 1;

            // ACT /  ASSERT
            await Assert.ThrowsAsync<Exception>(async () => await exampleServiceClient.GetExampleById(exampleId));
        }
    }
}
