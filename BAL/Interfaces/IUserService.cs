using BAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<string>> GenerateUsernames(int? count = 8);
    }
}
