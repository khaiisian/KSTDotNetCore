using KSTDotNetCore.RealtimeChart_2.Hubs;
using KSTDotNetCore.RealtimeChart_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace KSTDotNetCore.RealtimeChart_2.Controllers
{
    public class PieChartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<ChartHub> _hubContext;

        public PieChartController(AppDbContext context, IHubContext<ChartHub> hubContext)
        {
            _context = context;
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

        public async Task<IActionResult> Save(TblPieChartt reqModel)
        {
            await _context.AddAsync(reqModel);
            await _context.SaveChangesAsync();

            var lst = _context.TblPieChartts.AsNoTracking().ToList();
            var data = lst.Select(x => new PieChartModel
            {
                name = x.PieChartName,
                y = x.PieChartValue,
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
