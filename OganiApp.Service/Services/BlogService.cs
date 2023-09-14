using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using OganiApp.Core.Entities;
using OganiApp.Data.Contexts;
using OganiApp.Data.UniteOfWork;
using OganiApp.Service.Services.Interface;
using OganiApp.Service.Utilities.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUow _uow;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogService(IUow uow, AppDbContext context, IWebHostEnvironment env)
        {
            _uow = uow;
            _context = context;
            _env = env;
        }

        public async Task<Paginate<Blog>> AllHomeFilterAsync(string search, int page, int take)
        {

            var entities = from p in _context.Blogs
                           .Include(x => x.BlogDetails)
                           .Include(x => x.Owner)
                           select p;

            //Seacrh Filter
            if (!String.IsNullOrEmpty(search))
            {
                entities = from p in _context.Blogs
                           .Where(x => x.Title.Contains(search))
                           .Include(x => x.BlogDetails)
                           .Include(x => x.Owner)
                           select p;
            }


            //Paginate
            var allCount = await entities.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities.Skip((page - 1) * take).Take(take).ToListAsync();

            var result = new Paginate<Blog>(entities2, page, Totalpage);

            return result;
        }

        public async Task<Paginate<Blog>> AllFilterAsync(string search, string sortOrder, int page, int take)
        {

            var entities = from p in _context.Blogs
                           .Include(x => x.BlogDetails)
                           .Include(x => x.Owner)
                           select p;

            //Seacrh Filter
            if (!String.IsNullOrEmpty(search))
            {
                entities = from p in _context.Blogs
                           .Where(x => x.Title.Contains(search))
                           .Include(x => x.BlogDetails)
                           .Include(x => x.Owner)
                           select p;
            }

            //Sort Filter
            switch (sortOrder)
            {
                case "name_desc":
                    entities = entities.OrderByDescending(x => x.Title);
                    break;
                default:
                    entities = entities.OrderByDescending(x => x.Title);
                    break;
            }


            //Paginate
            var allCount = await entities.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities.Skip((page - 1) * take).Take(take).ToListAsync();

            var result = new Paginate<Blog>(entities2, page, Totalpage);

            return result;
        }

        public async Task<List<Blog>> AllAsync()
        {
            var list = await _context.Blogs.Include(x => x.BlogDetails).Include(x => x.Owner).OrderByDescending(x => x.Id).ToListAsync();

            return list;
        }

        public List<Blog> GetAll()
        {
            return _context.Blogs.Include(x => x.BlogDetails).Include(x => x.Owner).OrderByDescending(x => x.Id).ToList();
        }

        public async Task<Blog> GetByUpdateIdAsync(int id)
        {
            var entity = await _uow.GetRepository<Blog>().FindAsync(id);

            return entity;
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            var entity = await _context.Blogs.Include(x => x.BlogDetails).Include(x => x.Owner).SingleOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task CreateAsync(Blog model)
        {

            if (model != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string path = Path.Combine(_env.WebRootPath, "AdminPanel/img/blog", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }
                model.Image = fileName;

                await _uow.GetRepository<Blog>().CreateAsync(model);

                await _uow.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Blog model)
        {
            var DbEntity = await _uow.GetRepository<Blog>().FindAsync(model.Id);

            if ((DbEntity != null) || (model != null))
            {
                string oldPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/blog", DbEntity.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/blog", fileName);
                using (FileStream stream = new FileStream(newPath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }
                model.Image = fileName;

                _uow.GetRepository<Blog>().Update(model, DbEntity);

                await _uow.SaveChangesAsync();
            }

        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _uow.GetRepository<Blog>().FindAsync(id);

            if (entity != null)
            {
                if (entity.BlogDetails != null)
                {
                    var detail = await _uow.GetRepository<BlogDetail>().SingleOrDefaultAsync(x => x.BlogId == id);
                    _uow.GetRepository<BlogDetail>().Remove(detail);
                    await _uow.SaveChangesAsync();
                }
                var path = Path.Combine(_env.WebRootPath, "AdminPanel/img/blog", entity.Image);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                _uow.GetRepository<Blog>().Remove(entity);

                await _uow.SaveChangesAsync();
            }
        }
    }
}
