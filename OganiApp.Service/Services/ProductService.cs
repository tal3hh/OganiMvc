using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using OganiApp.Core.Entities;
using OganiApp.Data.Contexts;
using OganiApp.Data.UniteOfWork;
using OganiApp.Service.Services.Interface;
using OganiApp.Service.Utilities.Paginations;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUow _uow;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductService(IUow uow,  AppDbContext context, IWebHostEnvironment env)
        {
            _uow = uow;
            _context = context;
            _env = env;
        }

        public async Task<Paginate<Product>> CategoryProducts(string sort, int id, int page, int take)
        {
            var entities = from b in _context.Products?
                           .Where(x => x.CategoryId == id)
                           .Include(x => x.Category)
                           select b;

            //Sort Filter
            switch (sort)
            {
                case "price_desc":
                    entities = entities.OrderByDescending(x => x.Price);
                    break;
                case "price_asc":
                    entities = entities.OrderBy(x => x.Price);
                    break;
                case "name_desc":
                    entities = entities.OrderByDescending(x => x.Name);
                    break;
                default:
                    entities = entities.OrderBy(x => x.Name);
                    break;
            }

            //Paginate
            var allCount = await entities.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities.Skip((page - 1) * take).Take(take).ToListAsync();

            var result = new Paginate<Product>(entities2, page, Totalpage);

            return result;
        }

        public async Task<Paginate<Product>> AllHomeFilterAsync(string search, string sortOrder, int page, int take)
        {

            var entities = from p in _context.Products
                           .Include(x => x.Category)
                           .Include(x=> x.ProductDetails)
                           select p;

            //Seacrh Filter
            if (!String.IsNullOrEmpty(search))
            {
                entities = from p in _context.Products
                           .Where(x => x.Name.Contains(search))
                           .Include(x => x.Category)
                           .Include(x => x.ProductDetails)
                           select p;
            }

            //Sort Filter
            switch (sortOrder)
            {
                case "price_desc":
                    entities = entities.OrderByDescending(x => x.Price);
                    break;
                case "price_asc":
                    entities = entities.OrderBy(x => x.Price);
                    break;
                case "name_desc":
                    entities = entities.OrderByDescending(x => x.Name);
                    break;
                default:
                    entities = entities.OrderBy(x => x.Name);
                    break;
            }


            //Paginate
            var allCount = await entities.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities.Skip((page - 1) * take).Take(take).ToListAsync();

            var result = new Paginate<Product>(entities2, page, Totalpage);

            return result;
        }

        public async Task<Paginate<Product>> AllShopFilterAsync(int page, int take)
        {

            var entities = from p in _context.Products
                           .Include(x => x.Category)
                           .Include(x => x.ProductDetails)
                           select p;


            //Paginate
            var allCount = await entities.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities.Skip((page - 1) * take).Take(take).ToListAsync();

            var result = new Paginate<Product>(entities2, page, Totalpage);

            return result;
        }

        public async Task<Paginate<Product>> AllFilterAsync(string search, string sortOrder, int page, int take)
        {

            var entities = from p in _context.Products
                           .Include(x => x.Category)
                           .Include(x => x.ProductDetails)
                           select p;

            //Seacrh Filter
            if (!String.IsNullOrEmpty(search))
            {
                entities = from p in _context.Products
                           .Where(x => x.Name.Contains(search))
                           .Include(x => x.Category)
                           .Include(x => x.ProductDetails)
                           select p;
            }

            //Sort Filter
            switch (sortOrder)
            {
                case "count_desc":
                    entities = entities.OrderByDescending(x => x.Count);
                    break;
                case "count_asc":
                    entities = entities.OrderBy(x => x.Count);
                    break;
                case "price_desc":
                    entities = entities.OrderByDescending(x => x.Price);
                    break;
                default:
                    entities = entities.OrderBy(x => x.Price);
                    break;
            }


            //Paginate
            var allCount = await entities.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities.Skip((page - 1) * take).Take(take).ToListAsync();

            var result = new Paginate<Product>(entities2, page, Totalpage);

            return result;
        }

        public async Task<List<Product>> AllAsync()
        {
            var list = await _context.Products.Include(x => x.Category).Include(x => x.ProductDetails).OrderByDescending(x => x.Id).ToListAsync();

            return list;
        }

        public List<Product> GetAll()
        {
            var list = _context.Products.Include(x => x.Category).Include(x => x.ProductDetails).OrderByDescending(x => x.Id).ToList();

            return list;
        }

        public async Task<Product> GetByUpdateIdAsync(int id)
        {
            var entity = await _uow.GetRepository<Product>().FindAsync(id);

            return entity;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var entity = await _context.Products.Include(x => x.ProductDetails).Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task CreateAsync(Product model)
        {

            if (model != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string path = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }
                model.Image = fileName;

                await _uow.GetRepository<Product>().CreateAsync(model);

                await _uow.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Product model)
        {
            var DbEntity = await _uow.GetRepository<Product>().FindAsync(model.Id);
            var basket = await _context.Baskets.Where(x => x.ProductName == DbEntity.Name).FirstOrDefaultAsync();
            var favorite = await _context.Favorites.Where(x => x.ProductName == DbEntity.Name).FirstOrDefaultAsync();
            if ((DbEntity != null) || (model != null))
            {
                string oldPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", DbEntity.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", fileName);
                using (FileStream stream = new FileStream(newPath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }
                model.Image = fileName;

                _uow.GetRepository<Product>().Update(model, DbEntity);



                if (basket != null)
                {
                    var newbasket = new Basket();
                    newbasket.Id = basket.Id;
                    newbasket.ProductName = model.Name;
                    newbasket.Image = model.Image;
                    newbasket.AppUserId = basket.AppUserId;
                    newbasket.Price = model.Price;
                    newbasket.Count = model.Count;
                    _uow.GetRepository<Basket>().Update(newbasket, basket);

                }


                if (favorite != null)
                {
                    var newfav = new Favorite();
                    newfav.Id = favorite.Id;
                    newfav.AppUserId = favorite.AppUserId;
                    newfav.ProductName = model.Name;
                    newfav.Image = model.Image;
                    newfav.Price = model.Price;
                    _uow.GetRepository<Favorite>().Update(newfav, favorite);

                }
                await _uow.SaveChangesAsync();
            }

        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _uow.GetRepository<Product>().FindAsync(id);

            if (entity != null)
            {
                if (entity.ProductDetails != null)
                {
                    var detail = await _uow.GetRepository<ProductDetail>().SingleOrDefaultAsync(x => x.ProductId == id);
                    _uow.GetRepository<ProductDetail>().Remove(detail);
                    await _uow.SaveChangesAsync();
                }
                var path = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", entity.Image);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                _uow.GetRepository<Product>().Remove(entity);

                var basket = await _context.Baskets.Where(x => x.ProductName == entity.Name).FirstOrDefaultAsync();
                var favorite = await _context.Favorites.Where(x => x.ProductName == entity.Name).FirstOrDefaultAsync();
                if (basket != null) _uow.GetRepository<Basket>().Remove(basket);
                if (favorite != null) _uow.GetRepository<Favorite>().Remove(favorite);

                await _uow.SaveChangesAsync();
            }
        }
    }
}
