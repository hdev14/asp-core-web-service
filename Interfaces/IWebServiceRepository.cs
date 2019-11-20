using System.Threading.Tasks;
using web_service.Models;

namespace web_service.Repositories
{
    public interface IWebServiceRepository
    {
        Task CreateModelAsync(IModel model);
        Task<bool> UpdateModelAsync(int id, IModel model);
        Task<bool> DeleteModelAsync(int id);
        Task<IModel> FindModelAsync(int id);
        Task<IModel> FindModelsAsync();
    }
}