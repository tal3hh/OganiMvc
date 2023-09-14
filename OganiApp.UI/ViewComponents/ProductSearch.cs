using Microsoft.AspNetCore.Mvc;
using OganiApp.Core.Entities;

namespace OganiApp.UI.ViewComponents
{
    public class ProductSearch : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var product = new Product();

            return View(product);
        }
    }
}
