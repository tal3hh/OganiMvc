using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OganiApp.Core.Entities;
using OganiApp.Service.Services.Interface;
using System.Data;

namespace OganiApp.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    public class OwnerController : Controller
    {
        private readonly IOwnerService _Ownerservice;

        public OwnerController(IOwnerService Ownerservice)
        {
            _Ownerservice = Ownerservice;
        }


        public async Task<IActionResult> List()
        {
            var owners = await _Ownerservice.GetAllAsync();

            return View(owners);
        }

        public async Task<IActionResult> Create()
        {
            var list = await _Ownerservice.GetAllAsync();

            return View((new Owner(), list));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item1")] Owner model)
        {
            var list = await _Ownerservice.GetAllAsync();
            if (!ModelState.IsValid) return View((model, list));
            if (!model.Photo.ContentType.Contains("image/")) return View((model, list));

            await _Ownerservice.CreateAsync(model);

            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Update(int id)
        {
            var dto = await _Ownerservice.GetByIdUpdate(id);
            var list = await _Ownerservice.GetAllAsync();

            return View((dto, list));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item1")] Owner model)
        {
            var list = await _Ownerservice.GetAllAsync();
            if (!ModelState.IsValid) return View((model, list));
            if (!model.Photo.ContentType.Contains("image/")) return View((model, list));

            await _Ownerservice.Update(model);

            return RedirectToAction("Update");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                await _Ownerservice.Remove(id);
                return RedirectToAction("List");
            }

            return RedirectToAction("ErrorPage", "Home", new { area = "" });
        }
    }
}
