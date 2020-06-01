using AutoMapper;
using Bogus;
using PROJECT_NAME.Domain.Models;
using PROJECT_NAME.Infrastructure.ExampleService;
using Xunit;

namespace PROJECT_NAME.Infrastructure.Tests.ExampleService
{
    public class ExampleProfileTests
    {
        [Fact]
        public void Can_Map_From_ExampleEntity_To_Example()
        {
            // ARRANGE
            var faker = new Faker<ExampleEntity>()
                .RuleFor(o => o.Id, f => f.Random.Int())
                .RuleFor(o => o.Name, f => f.Random.String());

            var entity = faker.Generate();
            var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<ExampleProfile>(); });
            var mapper = new Mapper(configuration);

            // ACT
            var example = mapper.Map<Example>(entity);

            // ASSERT
            Assert.Equal(example.Id, entity.Id);
            Assert.Equal(example.Name, entity.Name);
        }
    }
}