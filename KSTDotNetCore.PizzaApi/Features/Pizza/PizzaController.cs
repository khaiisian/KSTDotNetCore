using KSTDotNetCore.PizzaApi.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace KSTDotNetCore.PizzaApi.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public PizzaController()
        {
            _appDbContext = new AppDbContext();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var lst = await _appDbContext.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Extras")]
        public async Task<IActionResult> GetExtrasAsync()
        {
            var lst = await _appDbContext.PizzaExtra.ToListAsync();
            return Ok(lst);
        }

        [HttpPost("OrderRequest")]
        public async Task<IActionResult> OrderAsync(OrderRequest orderRequest)
        {
            var item = await _appDbContext.Pizzas.FirstOrDefaultAsync(x => x.Id == orderRequest.PizzaId);
            var total = item.Price;

            if(orderRequest.Extras.Length> 0)
            {
                var lstextras = await _appDbContext.PizzaExtra.Where(x => orderRequest.Extras.Contains(x.Id)).ToListAsync();
                total+=lstextras.Sum(x=>x.Price); 
            }

            var invoicenum = DateTime.Now.ToString("yyyyMMddHHmmss");
            PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
            {
                PizzaId = orderRequest.PizzaId,
                PizzaOrderInvoiceNo = invoicenum,
                TotalAmount = total
            };

            List<PizzaOrderDetailModel> pizzaOrderDetailModels = orderRequest.Extras.Select(x => new PizzaOrderDetailModel{
                PizzaExtraId = x,
                PizzaOrderInvoiceNo = invoicenum
            }).ToList();

            await _appDbContext.PizzaOrderDetail.AddRangeAsync(pizzaOrderDetailModels);
            await _appDbContext.PizzaOrder.AddAsync(pizzaOrderModel);
            await _appDbContext.SaveChangesAsync();

            OrderResponse oderResponse = new OrderResponse()
            {
                Message = "Thank you for your order",
                InvoiceNum = invoicenum,
                TotalAmount = total
            };

            return Ok(oderResponse);

        }
    }

    
}
