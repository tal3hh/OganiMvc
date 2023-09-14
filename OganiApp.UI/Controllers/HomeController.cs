using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OganiApp.Core.Entities;
using OganiApp.Service.Services;
using OganiApp.Service.Services.Interface;

namespace OganiApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryservice;
        private readonly IProductService _productservice;
        private readonly IBlogService _blogservice;
        private readonly IAdvertService _advertservice;
        private readonly IBasketService _basketService;
        private readonly IFavoriteSerivce _favoriteService;
        private readonly IContactService _contactservice;
        private readonly UserManager<AppUser> _usermanager;

        public HomeController(ICategoryService categoryservice, IProductService productservice, IBlogService blogservice, IAdvertService advertservice, IBasketService basketservice, IFavoriteSerivce favortiteService, UserManager<AppUser> usermanager, IContactService contactservice)
        {
            _categoryservice = categoryservice;
            _productservice = productservice;
            _blogservice = blogservice;
            _advertservice = advertservice;
            _basketService = basketservice;
            _favoriteService = favortiteService;
            _usermanager = usermanager;
            _contactservice = contactservice;
        }



        public async Task<IActionResult> HomePage()
        {
            //Lasted
            ViewBag.Category1 = "fruit";
            ViewBag.Category2 = "vegetable";
            ViewBag.Category3 = "fresh meat";

            //Result
            ViewBag.Category4 = "dried more";
            ViewBag.Category5 = "fast food";
            ViewBag.Category6 = "drink";


            var categories = await _categoryservice.GetAllAsync();
            var products = await _productservice.AllAsync();
            var blogs = await _blogservice.AllAsync();
            var adverts = await _advertservice.GetAllAsync();

            return View((categories, products, blogs, adverts));
        }


        #region Contact

        public IActionResult ContactPage()
        {
            return View(new Contact());
        }
        [HttpPost]
        public async Task<IActionResult> ContactPage(Contact model)
        {
            if (!ModelState.IsValid) return View(model);

            await _contactservice.CreateAsync(model);

            return View();
        }

        #endregion


        #region Basket

        public async Task<IActionResult> BasketAdd(int id)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var user = await _usermanager.GetUserAsync(User);
            var dto = await _productservice.GetByIdAsync(id);

            await _basketService.CreateAsync(user.Id, dto);

            return RedirectToAction("HomePage");
        }

        public async Task<IActionResult> BasketList()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var user = await _usermanager.GetUserAsync(User);
            var list = await _basketService.GetAllAsync(user.Id);

            return View(list);
        }

        public async Task<IActionResult> BasketRemove(int id)
        {
            await _basketService.RemoveAsync(id);

            return RedirectToAction("BasketList");
        }

        #endregion


        #region Favorite

        public async Task<IActionResult> FavoriteAdd(int id)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var user = await _usermanager.GetUserAsync(User);
            var dto = await _productservice.GetByIdAsync(id);

            await _favoriteService.CreateAsync(user.Id, dto);

            return RedirectToAction("HomePage");
        }

        public async Task<IActionResult> FavoriteList()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var user = await _usermanager.GetUserAsync(User);

            var list = await _favoriteService.GetAllAsync(user.Id);

            return View(list);
        }

        public async Task<IActionResult> FavoriteRemove(int id)
        {
            await _favoriteService.RemoveAsync(id);

            return RedirectToAction("FavoriteList");
        }

        #endregion
    }
}
