using KSTDotNetCore.RealtimeChartApp_.Models;
using Microsoft.AspNetCore.Mvc;

namespace KSTDotNetCore.RealtimeChartApp_.Controllers
{
    public class PieChartController : Controller
    {
        private readonly AppDbContext _db;

        public PieChartController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task <IActionResult> Save(TblPieChart reqModel)
        {
            await _db.AddAsync(reqModel);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
