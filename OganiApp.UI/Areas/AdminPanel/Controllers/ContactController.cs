using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OganiApp.Service.Services.Interface;
using System.Data;

namespace OganiApp.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    public class ContactController : Controller
    {
        private readonly IContactService _contactservice;

        public ContactController(IContactService contactservice)
        {
            _contactservice = contactservice;
        }


        public async Task<IActionResult> List()
        {
            var categories = await _contactservice.GetAllAsync();

            return View(categories);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _contactservice.RemoveAsync(id);

            return RedirectToAction("List");
        }
    }
}
