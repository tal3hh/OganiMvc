using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OganiApp.Service.Services.Interface;
using System.Data;

namespace OganiApp.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    public class DashboardController : Controller
    {
        private readonly IProductService _productservice;
        private readonly ICategoryService _categoryservice;

        public DashboardController(IProductService productservice, ICategoryService categoryservice)
        {
            _productservice = productservice;   
            _categoryservice = categoryservice;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productservice.AllAsync();
            var categories = await _categoryservice.GetAllAsync();

            return View((categories,products));
        }

        

    }
}
