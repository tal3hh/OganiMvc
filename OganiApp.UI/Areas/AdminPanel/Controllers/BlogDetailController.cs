using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OganiApp.Core.Entities;
using OganiApp.Service.Services.Interface;
using System.Data;

namespace OganiApp.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    public class BlogDetailController : Controller
    {
        private readonly IBlogDetailService _detailservice;
        private readonly IBlogService _Blogservice;

        public BlogDetailController(IBlogDetailService detailservice, IBlogService Blogservice)
        {
            _detailservice = detailservice;
            _Blogservice = Blogservice;
        }

        public async Task<IActionResult> Create()
        {
            var prodetalist = await _detailservice.AllProDetaIsNull();
            var Blogs = await _Blogservice.AllAsync();

            return View((prodetalist, new BlogDetail(), Blogs));
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item2")] BlogDetail model)
        {
            var prodetalist = await _detailservice.AllProDetaIsNull();
            var Blogs = await _Blogservice.AllAsync();

            if (!ModelState.IsValid) return View((prodetalist, model, Blogs));

            await _detailservice.CreateAsync(model);

            return RedirectToAction("List", "Blog", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Update(int id)
        {
            var update = await _detailservice.GetByUpdateIdAsync(id);
            if (update == null) return RedirectToAction("Index", "Dashboard");
            var list = await _detailservice.AllProDetaIsNull();
            var Blogs = await _Blogservice.AllAsync();

            return View((list, update, Blogs));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item2")] BlogDetail model)
        {
            var list = await _detailservice.AllProDetaIsNull();
            var Blogs = await _Blogservice.AllAsync();

            if (!ModelState.IsValid) return View((list, model, Blogs));

            await _detailservice.UpdateAsync(model);

            return RedirectToAction("List", "Blog", new { area = "AdminPanel" });
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                await _detailservice.RemoveAsync(id);
                return RedirectToAction("List", "Blog", new { area = "AdminPanel" });
            }

            return RedirectToAction("ErrorPage", "Home", new { area = "" });
        }
    }
}
