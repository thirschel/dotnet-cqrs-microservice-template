using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Lamar;

namespace PROJECT_NAME.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class AutoMapperRegistry : ServiceRegistry
    {
        // https://gist.github.com/wshirey/71d1d0d5ff6d7ceeefd2
        public AutoMapperRegistry()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.AddAllTypesOf(typeof(Profile));
            });

            For<MapperConfiguration>().Use(ctx =>
            {
                var profiles = ctx.GetAllInstances<Profile>();

                var config = new MapperConfiguration(cfg =>
                {
                    foreach (var profile in profiles)
                    {
                        cfg.AddProfile(profile);
                    }
                });
                return config;
            }).Singleton();
            For<IMapper>().Use(ctx => ctx.GetInstance<MapperConfiguration>().CreateMapper(ctx.GetInstance)).Singleton();
        }
    }
}