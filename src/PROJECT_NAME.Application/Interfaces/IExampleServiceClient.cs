using PROJECT_NAME.Application.Models;
using System.Threading.Tasks;

namespace PROJECT_NAME.Application.Interfaces
{
    public interface IExampleServiceClient
    {
        Task<Example> GetExampleById(int id);
    }
}