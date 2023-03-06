using DAL.EntityModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<string>> GenerateUsernames(int? count = 8);
        public Task<SlidesUser> GetById(string userId);
    }
}
