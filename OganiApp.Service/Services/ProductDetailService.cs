using OganiApp.Core.Entities;
using OganiApp.Data.Contexts;
using OganiApp.Data.UniteOfWork;
using OganiApp.Service.Services.Interface;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IUow _uow;
        private readonly AppDbContext _context;

        public ProductDetailService(IUow uow, AppDbContext context)
        {
            _uow = uow;
            _context = context;
        }

        public async Task<List<Product>> AllProDetaIsNull()
        {
            var list = await _uow.GetRepository<Product>().AllFilterAsync(x => x.ProductDetails == null, false);

            return list;
        }


        public async Task<ProductDetail> GetByUpdateIdAsync(int id)
        {
            var entity = await _uow.GetRepository<ProductDetail>().SingleOrDefaultAsync(x => x.ProductId == id);

            return entity;
        }

        public async Task CreateAsync(ProductDetail model)
        {

            if (model != null)
            {
                await _uow.GetRepository<ProductDetail>().CreateAsync(model);
                await _uow.SaveChangesAsync();
            }
        }


        public async Task UpdateAsync(ProductDetail model)
        {
            var DbEntity = await _uow.GetRepository<ProductDetail>().FindAsync(model.Id);

            if ((DbEntity != null) || (model != null))
            {
                _uow.GetRepository<ProductDetail>().Update(model, DbEntity);

                await _uow.SaveChangesAsync();
            }
        }


        public async Task RemoveAsync(int id)
        {
            var entity = await _uow.GetRepository<ProductDetail>().SingleOrDefaultAsync(x => x.ProductId == id);

            if (entity != null)
            {
                _uow.GetRepository<ProductDetail>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
