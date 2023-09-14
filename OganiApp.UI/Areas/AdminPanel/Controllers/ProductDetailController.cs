using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OganiApp.Core.Entities;
using OganiApp.Service.Services.Interface;
using System.Data;

namespace Web.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailService _detailservice;
        private readonly IProductService _productservice;

        public ProductDetailController(IProductDetailService detailservice, IProductService productservice)
        {
            _detailservice = detailservice;
            _productservice = productservice;
        }

        public async Task<IActionResult> Create()
        {
            var prodetalist = await _detailservice.AllProDetaIsNull();
            var products = await _productservice.AllAsync();

            return View((prodetalist, new ProductDetail(), products));
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix ="Item2")] ProductDetail model)
        {
            var prodetalist = await _detailservice.AllProDetaIsNull();
            var products = await _productservice.AllAsync();

            if (!ModelState.IsValid) return View((prodetalist, model, products));

            await _detailservice.CreateAsync(model);

            return RedirectToAction("List", "Product", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Update(int id)
        {
            var update = await _detailservice.GetByUpdateIdAsync(id);
            var list = await _detailservice.AllProDetaIsNull();
            var products = await _productservice.AllAsync();

            return View((list,update,products));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix ="Item2")] ProductDetail model)
        {
            var list = await _detailservice.AllProDetaIsNull();
            var products = await _productservice.AllAsync();

            if(!ModelState.IsValid) return View((list, model, products));

            await _detailservice.UpdateAsync(model);

            return RedirectToAction("List", "Product", new { area = "AdminPanel" });
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                await _detailservice.RemoveAsync(id);
                return RedirectToAction("List", "Product", new { area = "AdminPanel" });
            }

            return RedirectToAction("ErrorPage", "Home", new { area = "" });
        }
    }
}
