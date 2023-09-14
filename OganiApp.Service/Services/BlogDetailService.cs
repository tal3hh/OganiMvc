using OganiApp.Core.Entities;
using OganiApp.Data.Contexts;
using OganiApp.Data.UniteOfWork;
using OganiApp.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services
{
    public class BlogDetailService : IBlogDetailService
    {
        private readonly IUow _uow;
        private readonly AppDbContext _context;

        public BlogDetailService(IUow uow, AppDbContext context)
        {
            _uow = uow;
            _context = context;
        }

        public async Task<List<Blog>> AllProDetaIsNull()
        {
            var list = await _uow.GetRepository<Blog>().AllFilterAsync(x => x.BlogDetails == null, false);

            return list;
        }


        public async Task<BlogDetail> GetByUpdateIdAsync(int id)
        {
            var entity = await _uow.GetRepository<BlogDetail>().SingleOrDefaultAsync(x => x.BlogId == id);

            return entity;
        }

        public async Task CreateAsync(BlogDetail model)
        {

            if (model != null)
            {
                await _uow.GetRepository<BlogDetail>().CreateAsync(model);
                await _uow.SaveChangesAsync();
            }
        }


        public async Task UpdateAsync(BlogDetail model)
        {
            var DbEntity = await _uow.GetRepository<BlogDetail>().FindAsync(model.Id);

            if ((DbEntity != null) || (model != null))
            {
                _uow.GetRepository<BlogDetail>().Update(model, DbEntity);

                await _uow.SaveChangesAsync();
            }
        }


        public async Task RemoveAsync(int id)
        {
            var entity = await _uow.GetRepository<BlogDetail>().SingleOrDefaultAsync(x => x.BlogId == id);

            if (entity != null)
            {
                _uow.GetRepository<BlogDetail>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
