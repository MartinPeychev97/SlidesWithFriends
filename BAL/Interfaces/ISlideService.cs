using DAL.EntityModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface ISlideService
    {
        Task<Slide> AddTitleSlide(int presentationId);

        Task<Slide> AddImageSlide(int presentationId, IFormFile image);

        Task<bool> Remove(int slideId);

        Task<Slide> GetById(int id);

        Task<IEnumerable<Slide>> GetAll(int presentationId);

        Task<Slide> Edit(int id, string title, string text);
    }
}
