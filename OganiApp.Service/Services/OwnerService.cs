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
    public class OwnerService : IOwnerService
    {
        private readonly IUow _uow;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public OwnerService(IUow uow, AppDbContext context, IWebHostEnvironment env)
        {
            _uow = uow;
            _context = context;
            _env = env;
        }
        public List<Owner> GetOwners()
        {
            var list = _context.Owners.OrderBy(x => x.Id).ToList();

            return list;
        }

        public async Task<List<Owner>> GetAllAsync()
        {
            var entities = await _context.Owners.OrderByDescending(x => x.Id).ToListAsync();

            return entities;
        }

        public async Task CreateAsync(Owner model)
        {
            if (model != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo?.FileName;
                string path = Path.Combine(_env.WebRootPath, "AdminPanel/img/owner", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }
                model.Image = fileName;

                await _uow.GetRepository<Owner>().CreateAsync(model);

                await _uow.SaveChangesAsync();
            }
        }

        public async Task<Owner> GetById(int id)
        {
            var entity = await _uow.GetRepository<Owner>().FindAsync(id);

            return entity;
        }

        public async Task<Owner> GetByIdUpdate(int id)
        {
            var entity = await _uow.GetRepository<Owner>().FindAsync(id);

            return entity;
        }

        public async Task Update(Owner model)
        {
            var DbEntity = await _uow.GetRepository<Owner>().FindAsync(model.Id);

            if ((DbEntity != null) || (model != null))
            {
                string oldPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/owner", DbEntity.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo?.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/owner", fileName);
                using (FileStream stream = new FileStream(newPath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }
                model.Image = fileName;

                _uow.GetRepository<Owner>().Update(model, DbEntity);

                await _uow.SaveChangesAsync();
            }
        }

        public async Task Remove(int id)
        {
            var entity = await _uow.GetRepository<Owner>().FindAsync(id);

            if (entity != null)
            {
                var path = Path.Combine(_env.WebRootPath, "AdminPanel/img/owner", entity.Image);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                _uow.GetRepository<Owner>().Remove(entity);

                await _uow.SaveChangesAsync();
            }
        }
    }
}
