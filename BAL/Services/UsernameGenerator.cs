using BAL.Interfaces;
using BAL.Models.Presentation;
using BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class UsernameGenerator : IUsernameGenerator
    {
        private readonly string baseApiUrl = "https://countriesnow.space/api/v0.1/";
        private readonly string flagsPath = $"countries/flag/images";
        private readonly string capitalsPath = $"countries/capital";
        private readonly IPresentationService presentationService;

        private readonly HttpClient _httpClient;
        private readonly Random _random;

        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private static readonly List<PresentationUsername> _generatedUsernames = new List<PresentationUsername>();
        private static readonly Dictionary<int, Dictionary<string, PresentationUsername>> _usernamesByPresentation = new Dictionary<int, Dictionary<string, PresentationUsername>>();

        public UsernameGenerator(HttpClient httpClient, Random random, IPresentationService presentationService)
        {
            this._httpClient = httpClient;
            this._random = random;
            this.presentationService = presentationService;
        }

        public async Task<PresentationUsername> GenerateUsername(int presentationId, string userId)
        {
            var presentation = await this.presentationService.GetById(presentationId);

            if (presentation == null)
            {
                throw new ArgumentException("Inexistent presentation");
            }

            if (!_generatedUsernames.Any())
            {
                await this.InitUsernames();
            }

            if (!_usernamesByPresentation.ContainsKey(presentationId))
            {
                _usernamesByPresentation.Add(presentationId, new Dictionary<string, PresentationUsername>());
            }

            var currentPresentation = _usernamesByPresentation[presentationId];

            if (currentPresentation.ContainsKey(userId))
            {
                return currentPresentation[userId];
            }

            while (true)
            {
                var randomIndex = this._random.Next(0, _generatedUsernames.Count - 1);

                var username = _generatedUsernames.ElementAt(randomIndex);

                if (!currentPresentation.Any(x => x.Value.AsCountry == username.AsCountry))
                {
                    currentPresentation.Add(userId, username);

                    return username;
                }
            }
        }

        private async Task<IEnumerable<CountryWithFlag>> GetCountriesWithFlags()
        {
            string json = await _httpClient.GetStringAsync($"{this.baseApiUrl}{this.flagsPath}");

            var response = JsonSerializer.Deserialize<ApiResponse<CountryWithFlag>>(json, jsonSerializerOptions);

            return response.Data;
        }

        private async Task<IEnumerable<CountryWithCapital>> GetCountriesWithCapitals()
        {
            string json = await _httpClient.GetStringAsync($"{this.baseApiUrl}{this.capitalsPath}");

            var response = JsonSerializer.Deserialize<ApiResponse<CountryWithCapital>>(json, jsonSerializerOptions);

            return response.Data;
        }

        private async Task InitUsernames()
        {
            var countriesWithFlags = await this.GetCountriesWithFlags();
            var countriesWithCapitals = await this.GetCountriesWithCapitals();

            var capitalsAndFlags = countriesWithFlags
                .Join(countriesWithCapitals,
                      f => f.Name,
                      c => c.Name,
                      (f, c) => new PresentationUsername
                      {
                          AsCountry = f.Name,
                          AsCapital = c.Capital,
                          CountryFlag = f.Flag,
                      });

            _generatedUsernames.AddRange(capitalsAndFlags);
        }
    }
}
