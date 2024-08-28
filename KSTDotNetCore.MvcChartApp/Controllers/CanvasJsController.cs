using Microsoft.AspNetCore.Mvc;

namespace KSTDotNetCore.MvcChartApp.Controllers
{
    public class CanvasJsController : Controller
    {
        public IActionResult LineChart()
        {
            return View();
        }
    }
}
