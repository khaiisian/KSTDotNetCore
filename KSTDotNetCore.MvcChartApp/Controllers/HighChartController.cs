using Microsoft.AspNetCore.Mvc;

namespace KSTDotNetCore.MvcChartApp.Controllers
{
    public class HighChartController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
    }
}
