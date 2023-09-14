using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services.Interface
{
    public interface IBasketService
    {
        Task CreateAsync(int userId, Product dto);
        Task RemoveAsync(int id);
        Task<List<Basket>> GetAllAsync(int userID);
    }
}
