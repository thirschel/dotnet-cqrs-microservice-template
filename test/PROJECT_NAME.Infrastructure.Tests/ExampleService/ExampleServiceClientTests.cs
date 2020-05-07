using System;
using System.Threading;
using PROJECT_NAME.Infrastructure.ExampleService;
using Moq;
using Serilog;
using Xunit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PROJECT_NAME.Application.Models;
using Microsoft.Extensions.Options;
using Moq.Protected;
using Newtonsoft.Json;
using AutoMapper;

namespace PROJECT_NAME.Infrastructure.Tests.ExampleService
{
    public class ExampleServiceClientTests
    {

        [Fact(Skip = "Not Implemented Yet")]
        public async void GetExampleById_Calls_Correct_Url()
        {
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
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

            var loggerMock = new Mock<ILogger>();

            var httpClient = new HttpClient(handlerMock.Object);

            IOptions<EnvironmentConfiguration> environmentConfiguration = Options.Create<EnvironmentConfiguration>(new EnvironmentConfiguration());
            var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<ExampleProfile>(); });
            var mapper = new Mapper(configuration);

            
            var tenant = "tenant";
            var contentId = "1";

            var exampleServiceClient = new ExampleServiceClient(
                loggerMock.Object, 
                mapper,
                environmentConfiguration,
                httpClient);
            var exampleId = 1;
 
            // ACT
            await exampleServiceClient.GetExampleById(exampleId);

            // ASSERT

            var expectedUri = new Uri($"http://localhost/api/v1/examples/{exampleId}");

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get
                        && req.RequestUri == expectedUri // to this uri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}
