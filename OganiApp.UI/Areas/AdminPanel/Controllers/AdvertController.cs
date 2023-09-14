using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OganiApp.Core.Entities;
using OganiApp.Service.Services.Interface;
using System.Data;

namespace OganiApp.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    public class AdvertController : Controller
    {
        private readonly IAdvertService _advertservice;

        public AdvertController(IAdvertService advert)
        {
            _advertservice = advert;
        }


        public async Task<IActionResult> List()
        {
            var categories = await _advertservice.GetAllAsync();

            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            var list = await _advertservice.GetAllAsync();

            return View((new Advert(), list));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item1")] Advert model)
        {
            var list = await _advertservice.GetAllAsync();
            if (!ModelState.IsValid) return View((model, list));
            if (!model.Photo.ContentType.Contains("image/")) return View((model, list));

            await _advertservice.CreateAsync(model);

            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Update(int id)
        {
            var dto = await _advertservice.GetByIdUpdate(id);
            var list = await _advertservice.GetAllAsync();

            return View((dto, list));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item1")] Advert model)
        {
            var list = await _advertservice.GetAllAsync();
            if (!ModelState.IsValid) return View((model, list));
            if (!model.Photo.ContentType.Contains("image/")) return View((model, list));

            await _advertservice.Update(model);

            return RedirectToAction("Update");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                await _advertservice.Remove(id);
                return RedirectToAction("List");
            }

            return RedirectToAction("ErrorPage", "Home", new { area = "" });
        }
    }
}
