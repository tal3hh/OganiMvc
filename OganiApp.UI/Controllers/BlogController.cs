using Microsoft.AspNetCore.Mvc;
using OganiApp.Service.Services.Interface;

namespace OganiApp.UI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogservice;
        private readonly IBlogDetailService _detailservice;

        public BlogController(IBlogService blogservice, IBlogDetailService detailservice)
        {
            _blogservice = blogservice;
            _detailservice = detailservice;
        }

        public async Task<IActionResult> BlogPage(string search, int page = 1, int take = 6)
        {
            var blogs = await _blogservice.AllHomeFilterAsync(search, page, take);

            return View(blogs);
        }

        public async Task<IActionResult> BlogDetailsPage(int id)
        {
            var blog = await _blogservice.GetByIdAsync(id);

            return View(blog);
        }
    }
}
