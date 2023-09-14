using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiApp.Service.Services.Interface;

namespace OganiApp.UI.ViewComponents
{
    public class BlogSideBar : ViewComponent
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IBlogService blogService;

        public BlogSideBar(ICategoryService categoryService, IProductService productService, IBlogService blogService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            
            ViewBag.Category1 = "fruit";
            ViewBag.Category2 = "vegetable";

            var categories = categoryService.GetCategories();
            var blogs = blogService.GetAll();
            var products = productService.GetAll();

            return View((categories, blogs, products));
        }
    }
}
