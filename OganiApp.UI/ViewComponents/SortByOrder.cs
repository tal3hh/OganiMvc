using Microsoft.AspNetCore.Mvc;

namespace OganiApp.UI.ViewComponents
{
    public class SortByOrder : ViewComponent
    {
        public IViewComponentResult Invoke(string action)
        {
            ViewBag.Action = action;

            return View();
        }
    }
}
