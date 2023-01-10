using BAL.Models.Presentation;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IUsernameGenerator
    {
        public Task<PresentationUsername> GenerateUsername(int presentationId,string userId);
    }
}
