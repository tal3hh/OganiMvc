using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
    public class AdvertService : IAdvertService
    {
        private readonly IUow _uow;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AdvertService(IUow uow, AppDbContext context, IWebHostEnvironment env)
        {
            _uow = uow;
            _context = context;
            _env = env;
        }
        public List<Advert> GetAdverts()
        {
            var list = _context.Adverts.OrderBy(x => x.Id).ToList();

            return list;
        }

       

        public async Task<List<Advert>> GetAllAsync()
        {
            var entities = await _context.Adverts.OrderByDescending(x => x.Id).ToListAsync();

            return entities;
        }

        public async Task CreateAsync(Advert model)
        {
            if (model != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo?.FileName;
                string path = Path.Combine(_env.WebRootPath, "AdminPanel/img/advert", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }
                model.Image = fileName;

                await _uow.GetRepository<Advert>().CreateAsync(model);

                await _uow.SaveChangesAsync();
            }
        }

        public async Task<Advert> GetById(int id)
        {
            var entity = await _uow.GetRepository<Advert>().FindAsync(id);

            return entity;
        }

        public async Task<Advert> GetByIdUpdate(int id)
        {
            var entity = await _uow.GetRepository<Advert>().FindAsync(id);

            return entity;
        }

        public async Task Update(Advert model)
        {
            var DbEntity = await _uow.GetRepository<Advert>().FindAsync(model.Id);

            if ((DbEntity != null) || (model != null))
            {
                string oldPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/advert", DbEntity.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo?.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/advert", fileName);
                using (FileStream stream = new FileStream(newPath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }
                model.Image = fileName;

                _uow.GetRepository<Advert>().Update(model, DbEntity);

                await _uow.SaveChangesAsync();
            }
        }

        public async Task Remove(int id)
        {
            var entity = await _uow.GetRepository<Advert>().FindAsync(id);

            if (entity != null)
            {
                var path = Path.Combine(_env.WebRootPath, "AdminPanel/img/advert", entity.Image);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                _uow.GetRepository<Advert>().Remove(entity);

                await _uow.SaveChangesAsync();
            }
        }
    }
}
