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
        
        public IActionResult FunnelChart()
        {
            FunnelChartModel model = new FunnelChartModel();
            model.Data = new List<int> { 1380, 1100, 990, 880, 740, 548, 330, 200 };
            model.Category = new List<string> {
                    "Sourced",
                    "Screened",
                    "Assessed",
                    "HR Interview",
                    "Technical",
                    "Verify",
                    "Offered",
                    "Hired"};
            return View(model);
        }
    }
}
