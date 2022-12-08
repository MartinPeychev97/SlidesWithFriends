using DAL.EntityModels;
using System.Threading.Tasks;

namespace BAL.Services
{
    public interface IPresentationService
    {
        Task<Presentation> GetById(int id);
    }
}
