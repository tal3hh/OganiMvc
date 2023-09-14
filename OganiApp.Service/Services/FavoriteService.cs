using Microsoft.EntityFrameworkCore;
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
    public class FavoriteService : IFavoriteSerivce
    {
        private readonly IUow _uow;
        private readonly AppDbContext _context;
        public FavoriteService(IUow uow, AppDbContext context)
        {
            _uow = uow;
            _context = context;
        }


        public async Task CreateAsync(int userId, Product dto)
        {
            var test = await _context.Favorites.Where(x => x.AppUserId == userId).Where(x => x.ProductName == dto.Name).FirstOrDefaultAsync();

            if (test == null)
            {
                var entity = new Favorite();
                entity.ProductName = dto.Name;
                entity.Image = dto.Image;
                entity.Price = dto.Price;
                entity.AppUserId = userId;

                await _uow.GetRepository<Favorite>().CreateAsync(entity);
                await _uow.SaveChangesAsync();
            }

        }


        public async Task<List<Favorite>> GetAllAsync(int userID)
        {
            return await _context.Favorites.Include(x => x.AppUser).Where(x => x.AppUserId == userID).ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _uow.GetRepository<Favorite>().FindAsync(id);
            _uow.GetRepository<Favorite>().Remove(entity);
            await _uow.SaveChangesAsync();
        }
    }
}
