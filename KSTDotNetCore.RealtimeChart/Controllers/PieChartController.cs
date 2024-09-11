using KSTDotNetCore.RealtimeChart.Hubs;
using KSTDotNetCore.RealtimeChart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace KSTDotNetCore.RealtimeChart.Controllers
{
    public class PieChartController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHubContext <ChartHub> _hubContext;

        public PieChartController(AppDbContext db, IHubContext<ChartHub> hubContext)
        {
            _db = db;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task <IActionResult> Save(TblPieChartt reqModel)
        {
            await _db.AddAsync(reqModel);
            await _db.SaveChangesAsync();

            var lst = await _db.TblPieChartts.ToListAsync();
            var data = lst.Select(x => new PieChartModel
            {
                name = x.PieChartName,
                y = x.PieChartValue
            }).ToList();

            await _hubContext.Clients.All.SendAsync("ReceivePieChart", data);

            return RedirectToAction("Index");
        }
    }
}

public class PieChartModel
{
    public string name { get; set; }
    public decimal y { get; set; }
}