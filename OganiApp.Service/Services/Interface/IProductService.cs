using OganiApp.Core.Entities;
using OganiApp.Service.Utilities.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services.Interface
{
    public interface IProductService
    {
        Task<Paginate<Product>> AllFilterAsync(string search, string sortOrder, int page, int take);
        Task<Paginate<Product>> AllHomeFilterAsync(string search, string sortOrder, int page, int take);
        Task<Paginate<Product>> CategoryProducts(string sort, int id, int page, int take);
        Task<Paginate<Product>> AllShopFilterAsync(int page, int take);
        Task<List<Product>> AllAsync();
        List<Product> GetAll();
        Task<Product> GetByUpdateIdAsync(int id);
        Task<Product> GetByIdAsync(int id);
        Task CreateAsync(Product model);
        Task UpdateAsync(Product model);
        Task RemoveAsync(int id);
    }
}
