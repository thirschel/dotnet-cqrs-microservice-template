using PROJECT_NAME.Application.Models;
using PROJECT_NAME.Domain.Models;
using MediatR;

namespace PROJECT_NAME.Application.Queries.Example
{
    public class GetExampleByIdQuery : IRequest<QueryResult<Domain.Models.Example>>
    {
        public int Id { get; set; }
    }
}