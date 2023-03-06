using DAL.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IPresentationService
    {
        Task Create(string name, string userId, string image);

        Task<Presentation> GetById(int presentationId);

        Task<IEnumerable<Presentation>> GetAll(string userId);

        Task<bool> EditName(int id, string name);

        Task<bool> EditImage(int id, string image);

        Task<bool> Remove(int presentationId);
    }
}
