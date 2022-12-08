using DAL.EntityModels;
using System.Threading.Tasks;

namespace BAL.Services
{
    public interface ISlideService
    {
        Task<int> Add(string title, string text, int presentationId);

        Task<bool> Remove(int slideId);

        Task<Slide> GetById(int id);
    }
}
