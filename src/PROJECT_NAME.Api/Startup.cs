using System;
using System.IO;
using System.Reflection;
using Lamar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PROJECT_NAME.Api.Configurations.Extensions;
using PROJECT_NAME.Api.Middleware.EexceptionHandling;
using CorrelationId;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace PROJECT_NAME.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureContainer(ServiceRegistry services)
        {
            services.AddCorrelationId();
            services.AddHttpContextAccessor();
            services.AddMvc();
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddOptions();
            services.AddHttpClient();
            services.AddSwagger(Configuration);
            services.AddDependencyInjection(Configuration);
            services.AddControllers();
            services.AddHealthChecks();
            services.AddSwaggerGen(c =>
            {
                // This coupled with the properties in the csproj allow the swagger page to show additional comments for methods
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseSwaggerDocumentation(provider);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHealthChecks("/health");
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
