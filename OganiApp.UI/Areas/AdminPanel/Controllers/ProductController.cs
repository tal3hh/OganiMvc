using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OganiApp.Core.Entities;
using OganiApp.Service.Services.Interface;
using System.Collections.Generic;
using System.Data;

namespace Web.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    public class ProductController : Controller
    {
        private readonly IProductService _productservice;
        private readonly IProductDetailService _detailservice;
        private readonly ICategoryService _categoryservice;

        public ProductController(IProductService productservice, ICategoryService categoryservice, IProductDetailService detailservice)
        {
            _productservice = productservice;
            _categoryservice = categoryservice;
            _detailservice = detailservice;
        }


        public async Task<IActionResult> List(string search, string sort, int page = 1, int take = 15)
        {
            ViewBag.Price = String.IsNullOrEmpty(sort) ? "price_desc" : "";
            ViewBag.Count = sort == "count_asc" ? "count_desc" : "count_asc";

            //View send
            TempData["sort"] = sort;

            var list = await _productservice.AllFilterAsync(search, sort, page, take);

            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryservice.GetAllAsync();
            var products = await _productservice.AllAsync();
            var prodetanull = await _detailservice.AllProDetaIsNull();

            return View((new Product(), new ProductDetail(), categories, products, prodetanull));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item1")] Product model)
        {
            var categories = await _categoryservice.GetAllAsync();
            var products = await _productservice.AllAsync();
            var prodetalist = await _detailservice.AllProDetaIsNull();

            if (!ModelState.IsValid) return View((model, new ProductDetail(), categories, products, prodetalist));
            if (!model.Photo.ContentType.Contains("image/")) return View((model, new ProductDetail(), categories, products, prodetalist));

            await _productservice.CreateAsync(model);

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Update(int id)
        {
            var categories = await _categoryservice.GetAllAsync();
            var products = await _productservice.AllAsync();
            var updateproduct = await _productservice.GetByUpdateIdAsync(id);

            var updateproDeta = await _detailservice.GetByUpdateIdAsync(id);

            return View((updateproduct,updateproDeta, categories, products));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item1")] Product model)
        {
            var categories = await _categoryservice.GetAllAsync();
            var products = await _productservice.AllAsync();

            var updateproDeta = await _detailservice.GetByUpdateIdAsync(model.Id);
            if (!ModelState.IsValid) return View((model, updateproDeta, categories, products));
            if (!model.Photo.ContentType.Contains("image/")) return View((model, updateproDeta, categories, products));

            await _productservice.UpdateAsync(model);

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return RedirectToAction("ErrorPage", "Home", new { area = "" });

            await _productservice.RemoveAsync(id);

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) return RedirectToAction("ErrorPage", "Home", new { area = "" });

            var model = await _productservice.GetByIdAsync(id);

            return View(model);
        }
    }
}
