using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiApp.Core.Entities;
using OganiApp.Service.Services.Interface;

namespace OganiApp.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productservice;
        private readonly IProductDetailService _detailservice;
        private readonly ICommentService _commnetserivce;

        public ProductController(IProductService productservice, IProductDetailService detailservice, ICommentService commnetserivce)
        {
            _productservice = productservice;
            _detailservice = detailservice;
            _commnetserivce = commnetserivce;
        }



        #region ProductList

        public async Task<IActionResult> ProductPage(string search, string sort, int page = 1, int take = 12)
        {
            ViewBag.Name = String.IsNullOrEmpty(sort) ? "name_desc" : "";
            ViewBag.Price = (sort == "price_desc") ? "price_asc" : "price_desc";

            //View send
            TempData["sort"] = sort;

            var list = await _productservice.AllHomeFilterAsync(search, sort, page, take);

            ViewBag.Count = (await _productservice.AllAsync()).Count();

            return View(list);
        }

        #endregion


        #region ShopPage

        public async Task<IActionResult> ShopPage(int page = 1, int take = 6)
        {

            var products = await _productservice.AllShopFilterAsync(page, take);

            ViewBag.Count = (await _productservice.AllAsync()).Count();

            return View(products);
        }

        #endregion


        #region CategoryProduct
        public async Task<IActionResult> ProductCategoryPage(string sort, int id, int page = 1, int take = 12)
        {
            ViewBag.Name = String.IsNullOrEmpty(sort) ? "name_desc" : "";
            ViewBag.Price = (sort == "price_desc") ? "price_asc" : "price_desc";

            //View send
            TempData["sort"] = sort;

            var list = await _productservice.CategoryProducts(sort, id, page, take);

            ViewBag.Count = (await _productservice.AllAsync()).Count();

            return View(list);
        }

        #endregion


        #region DetailANDComment

        public async Task<IActionResult> ProductDetailsPage(int id)
        {
            var productdetail = await _productservice.GetByIdAsync(id);

            var productlist = await _productservice.AllAsync();

            var commentlist = await _commnetserivce.ProductComment(id);

            TempData["ProId"] = id;

            return View((productdetail, productlist, commentlist, new Comment()));
        }
        [HttpPost]
        public async Task<IActionResult> ProductDetailsPage([Bind(Prefix = "Item4")] Comment model)
        {
            if (TempData["ProId"] == null) return RedirectToAction("HomePage", "Home");
            model.ProductId = (int)TempData["ProId"];

            var productdetail = await _productservice.GetByIdAsync(model.ProductId);
            var productlist = await _productservice.AllAsync();
            var commentlist = await _commnetserivce.ProductComment(model.ProductId);

            if (!ModelState.IsValid) return View((productdetail, productlist, commentlist, model));

            await _commnetserivce.CreateAsync(model);

            return RedirectToAction("ProductDetailsPage");
        }

        #endregion
    }
}
