using OganiApp.Core.Entities;
using OganiApp.Service.Utilities.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        List<Category> GetCategories();
        Task CreateAsync(Category model);
        Task<Category> GetById(int id);
        Task<Category> GetByIdUpdate(int id);
        Task Update(Category dto);
        Task Remove(int id);
    }
}
