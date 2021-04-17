using System.Threading.Tasks;
using AutoMapper;
using PROJECT_NAME.Application.Interfaces;
using PROJECT_NAME.Domain.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using PROJECT_NAME.Application.Models;
using Serilog;

namespace PROJECT_NAME.Infrastructure.ExampleService
{
    public class ExampleServiceClient : IExampleServiceClient
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IOptions<EnvironmentConfiguration> _configuration;
        private readonly HttpClient _httpClient;


        public ExampleServiceClient(
            ILogger logger,
            IMapper mapper,
            IOptions<EnvironmentConfiguration> configuration,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration.Value.SERVICE_URL);
        }

        public async Task<Example> GetExampleById(int id)
        {
            var requestUri = new Uri(new Uri(_configuration.Value.SERVICE_URL), $"api/v1/examples/{id}");
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                _logger.Error($"Received status code {response.StatusCode} - {response.RequestMessage.RequestUri}:{responseMessage}");
                throw new Exception(responseMessage);
            }
            var content =  await response.Content.ReadAsStringAsync();
            var entity = JsonConvert.DeserializeObject<ExampleEntity>(content);
            return _mapper.Map<Example>(entity);
        }
    }
}
