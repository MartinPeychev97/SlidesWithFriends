using Common.InputModels.Presentation;
using DAL.EntityModels;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IPresentationService
    {
        Task<Presentation> GetById(int id);

        Task EditBackground(PresentationBackgroundInputModel model);

        Task<bool> EditName(int id, string name);
    }
}
