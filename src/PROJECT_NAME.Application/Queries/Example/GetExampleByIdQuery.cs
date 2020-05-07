using PROJECT_NAME.Application.Models;
using MediatR;

namespace PROJECT_NAME.Application.Queries.Example
{
    public class GetExampleByIdQuery : IRequest<QueryResult<Models.Example>>
    {
        public int Id { get; set; }
    }
}