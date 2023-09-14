using Microsoft.EntityFrameworkCore;
using OganiApp.Core.Entities;
using OganiApp.Data.Contexts;
using OganiApp.Data.UniteOfWork;
using OganiApp.Service.Services.Interface;
using OganiApp.Service.Utilities.Paginations;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUow _uow;
        private readonly AppDbContext _context;
        public CommentService(IUow uow, AppDbContext context)
        {
            _uow = uow;
            _context = context;
        }

        public async Task<Paginate<Comment>> AllComments(string search, int page, int take)
        {
            var entities = from p in _context.Comments
                          .Include(x => x.Product)
                           select p;

            //Seacrh Filter
            if (!String.IsNullOrEmpty(search))
            {
                entities = from p in _context.Comments
                           .Include(x => x.Product)
                           .Where(x => x.Product.Name.Contains(search))
                           select p;
            }


            //Paginate
            var allCount = await entities.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities.Skip((page - 1) * take).Take(take).OrderByDescending(x => x.Id).ToListAsync();


            var result = new Paginate<Comment>(entities2, page, Totalpage);

            return result;
        }

        public async Task<List<Comment>> ProductComment(int id)
        {
            var comments = await _context.Comments.Include(x => x.Product).Where(x => x.ProductId == id).OrderByDescending(x => x.Id).ToListAsync();

            return comments;
        }

        public async Task CreateAsync(Comment model)
        {

            if (model != null)
            {
                await _uow.GetRepository<Comment>().CreateAsync(model);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _uow.GetRepository<Comment>().FindAsync(id);

            if (entity != null)
            {
                _uow.GetRepository<Comment>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
