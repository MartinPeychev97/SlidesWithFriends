using DAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IRatingService
    {
        Task<IEnumerable<Rating>> GetAll(int presentationId);
        Task<Rating> GetById(int id);
        int CalculateAverageRating(int presentationId);
        Task<Rating> AddRating(int presentationId, int ratingValue, string userId);
        Task<bool> EditRating(int id, int ratingValue);
        Task<bool> Remove(int ratingId);
    }
}
