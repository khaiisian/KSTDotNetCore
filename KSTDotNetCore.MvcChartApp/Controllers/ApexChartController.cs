using KSTDotNetCore.MvcChartApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KSTDotNetCore.MvcChartApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PieChart()
        {
            PieChartModel model = new PieChartModel();
            model.Series = new List<int>() { 44, 55, 13, 43, 22 };
            model.Labels = new List<string>() { "Team A", "Team B", "Team C", "Team D", "Team E" };
            return View(model);
        }
    }
}
