using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using PROJECT_NAME.Application.Interfaces;
using PROJECT_NAME.Application.Models;
using PROJECT_NAME.Application.Queries.Example;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace PROJECT_NAME.Infrastructure.SqlServer
{
    public class ExampleRepository : IExampleRepository
    {
        private readonly IOptions<EnvironmentConfiguration> _configuration;
        private readonly IMapper _mapper;

        public ExampleRepository(IMapper mapper, IOptions<EnvironmentConfiguration> configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<int> UpdateExampleNameById(int id, string name)
        {
            var query = Sql.UpdateExampleNameById.Value;

            using (var conn = new SqlConnection(this._configuration.Value.SQL_CONNECTION_STRING))
            {
                //  conn.Open();

                /*
                    Using Dapper here provides a few advantages:
                    Better performance vs EF Core (https://exceptionnotfound.net/dapper-vs-entity-framework-core-query-performance-benchmarking-2019/)
                    More query control - Queries can be the exact syntax we specify vs the generated syntax from EF Core
                    Paramterization - Ties in to above, but parametrizing a query becomes trivial
                */
                // var rowsAffected = await conn.ExecuteScalarAsync<int>(query, new { Id = id, Name = name });

                // return rowsAffected;

                return 1;
            }
        }
    }
}
