using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services.Interface
{
    public interface IAdvertService
    {
        Task<List<Advert>> GetAllAsync();
        List<Advert> GetAdverts();
        Task CreateAsync(Advert model);
        Task<Advert> GetById(int id);
        Task<Advert> GetByIdUpdate(int id);
        Task Update(Advert dto);
        Task Remove(int id);
    }
}
