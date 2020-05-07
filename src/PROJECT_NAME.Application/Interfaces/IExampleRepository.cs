using System.Threading.Tasks;

namespace PROJECT_NAME.Application.Interfaces
{
    public interface IExampleRepository
    {
        Task<int> UpdateExampleNameById(int id, string name);
    }
}