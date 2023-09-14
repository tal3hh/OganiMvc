using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services.Interface
{
    public interface IProductDetailService
    {
        Task<List<Product>> AllProDetaIsNull();
        Task CreateAsync(ProductDetail model);
        Task UpdateAsync(ProductDetail model);
        Task<ProductDetail> GetByUpdateIdAsync(int id);
        Task RemoveAsync(int id);
    }
}
