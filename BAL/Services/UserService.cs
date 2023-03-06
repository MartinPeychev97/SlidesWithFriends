using BAL.Interfaces;
using DAL;
using DAL.EntityModels.User;
using Microsoft.EntityFrameworkCore;
using RandomNameGeneratorLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private readonly PlaceNameGenerator _placeNameGenerator;
        private readonly SlidesDbContext _dbContext;

        public UserService(PlaceNameGenerator placeNameGenerator, SlidesDbContext slidesDbContext)
        {
            this._placeNameGenerator = placeNameGenerator;
            this._dbContext = slidesDbContext;
        }
        public async Task<SlidesUser> GetById(string userId) =>
         await this._dbContext.Users.Where(u => u.Id == userId)
            .Include(u=>u.Presentations)
            .ThenInclude(p=>p.Slides).FirstAsync();
        public async Task<IEnumerable<string>> GenerateUsernames(int? count = 8)
        {
            var usernames = this._placeNameGenerator.GenerateMultiplePlaceNames((int)count).ToList();

            await EnsureNamesDoNotExist(usernames);

            return usernames;
        }

        private async Task EnsureNamesDoNotExist(List<string> generatedNames)
        {
            bool areValid = false;
            int index = 0;

            while (!areValid)
            {
                bool userNameExists = await this._dbContext.Users.AnyAsync(u => u.UserName == generatedNames[index]);

                if (userNameExists)
                {
                    generatedNames.RemoveAt(index);
                    var newUsername = this._placeNameGenerator.GenerateRandomPlaceName();
                    generatedNames.Add(newUsername);
                }
                else
                {
                    if (index == generatedNames.Count - 1)
                    {
                        areValid = true;
                    }

                    index++;
                }
            }
        }
    }
}
