using BAL.Models.Slide;
using DAL.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface ISlideService
    {
        Task<Slide> AddTitleSlide(int presentationId);

        Task<Slide> AddImageSlide(int presentationId, string image);

        Task<bool> Remove(int slideId);

        Task<Slide> GetById(int id);

        Task<IEnumerable<Slide>> GetAll(int presentationId);

        Task<bool> EditTitle(int id, string title);

        Task<bool> EditText(int id, string text);

        Task<bool> EditOnDragAndDrop(int firstId, int secondId);

        Task EditBackground(EditSlideBackgroundInputModel model);
        Task<Slide> AddRatingSlide(int presentationId, int rating);
        Task<Slide> AddWordCloudSlide(int presentationId);
    }
}
