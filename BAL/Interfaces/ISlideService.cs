﻿using DAL.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface ISlideService
    {
        Task<int> Add(int presentationId);

        Task<bool> Remove(int slideId);

        Task<Slide> GetById(int id);

        Task<IEnumerable<Slide>> GetAll(int presentationId);

        Task<Slide> Edit(int id, string title, string text);
    }
}
