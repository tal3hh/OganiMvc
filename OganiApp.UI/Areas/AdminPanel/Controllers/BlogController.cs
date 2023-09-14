using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OganiApp.Core.Entities;
using OganiApp.Service.Services;
using OganiApp.Service.Services.Interface;
using System.Data;

namespace OganiApp.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    public class BlogController : Controller
    {
        private readonly IBlogService _Blogservice;
        private readonly IBlogDetailService _detailservice;
        private readonly IOwnerService _ownerserivce;

        public BlogController(IBlogService Blogservice, IBlogDetailService detailservice, IOwnerService ownerserivce)
        {
            _Blogservice = Blogservice;
            _detailservice = detailservice;
            _ownerserivce = ownerserivce;
        }


        public async Task<IActionResult> List(string search, string sort, int page = 1, int take = 15)
        {

            //View send
            TempData["sort"] = sort;

            var list = await _Blogservice.AllFilterAsync(search, sort, page, take);

            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var owners = await _ownerserivce.GetAllAsync();
            var blog = await _Blogservice.AllAsync();
            var blogdetalis = await _detailservice.AllProDetaIsNull();

            return View((new Blog(), new BlogDetail(), owners, blog, blogdetalis));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item1")] Blog model)
        {
            var owners = await _ownerserivce.GetAllAsync();
            var blog = await _Blogservice.AllAsync();
            var blogdetalis = await _detailservice.AllProDetaIsNull();

            if (!ModelState.IsValid) return View((model, new BlogDetail(), owners, blog, blogdetalis));
            if (!model.Photo.ContentType.Contains("image/")) return View((model, new BlogDetail(), owners, blog, blogdetalis));

            await _Blogservice.CreateAsync(model);

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Update(int id)
        {
            var owners = await _ownerserivce.GetAllAsync();
            var blogs = await _Blogservice.AllAsync();
            var updateblog = await _Blogservice.GetByUpdateIdAsync(id);

            var updateproDeta = await _detailservice.GetByUpdateIdAsync(id);

            return View((updateblog, updateproDeta, owners, blogs));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item1")] Blog model)
        {
            var owners = await _ownerserivce.GetAllAsync();
            var blogs = await _Blogservice.AllAsync();

            var updateproDeta = await _detailservice.GetByUpdateIdAsync(model.Id);
            if (!ModelState.IsValid) return View((model, updateproDeta, owners, blogs));
            if (!model.Photo.ContentType.Contains("image/")) return View((model, updateproDeta, owners, blogs));

            await _Blogservice.UpdateAsync(model);

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return RedirectToAction("ErrorPage", "Home", new { area = "" });

            await _Blogservice.RemoveAsync(id);

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) return RedirectToAction("ErrorPage", "Home", new { area = "" });

            var model = await _Blogservice.GetByIdAsync(id);

            return View(model);
        }
    }
}
