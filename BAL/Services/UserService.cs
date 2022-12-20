﻿using BAL.Interfaces;
using BAL.Models;
using DAL;
using Microsoft.EntityFrameworkCore;
using RandomNameGeneratorLibrary;
using System;
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

        public async Task<IEnumerable<string>> GenerateUsernames(int? count = 5)
        {
            var usernames = this._placeNameGenerator.GenerateMultiplePlaceNames((int)count).ToList();

            await EnsureNamesDoNotExist(usernames);

            return usernames;
        }

        private async Task EnsureNamesDoNotExist(List<string> generatedNames)
        {
            bool areValid = false;
            int index = 0;

            while (areValid == false)
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
