using KSTDotNetCore.PizzaApi.Db;
using KSTDotNetCore.PizzaApi.Query;
using KSTDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections.Generic;

namespace KSTDotNetCore.PizzaApi.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly DapperService _dapperService;

        public PizzaController()
        {
            _appDbContext = new AppDbContext();
            _dapperService = new DapperService(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
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

            if (orderRequest.Extras.Length > 0)
            {
                var lstextras = await _appDbContext.PizzaExtra.Where(x => orderRequest.Extras.Contains(x.Id)).ToListAsync();
                total += lstextras.Sum(x => x.Price);
            }

            var invoicenum = DateTime.Now.ToString("yyyyMMddHHmmss");
            PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
            {
                PizzaId = orderRequest.PizzaId,
                PizzaOrderInvoiceNo = invoicenum,
                TotalAmount = total
            };

            List<PizzaOrderDetailModel> pizzaOrderDetailModels = orderRequest.Extras.Select(x => new PizzaOrderDetailModel {
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

        //[HttpGet("CheckOrder/ {invoicenum}")]
        //public async Task<IActionResult> CheckOrderAsync(string invoicenum)
        //{
        //    var order = await _appDbContext.PizzaOrder.FirstOrDefaultAsync(x=>x.PizzaOrderInvoiceNo == invoicenum);    
        //    var lst = await _appDbContext.PizzaOrderDetail.Where(x=>x.PizzaOrderInvoiceNo == invoicenum).ToListAsync();

        //    return Ok(new
        //    {
        //        Order = order,
        //        OrderDetail = lst
        //    });


        [HttpGet("CheckOrder/ {invoiceno})")]
        public IActionResult GetOrder(string invoiceno)
        {
            var item = _dapperService.QueryFirstOrDefault<PizzaOrderInvoiceModel>
                (
                    PizzaQuery.PizzaOrderQuery,
                    new { PizzaOrderInvoiceNo = invoiceno}
                );

            var lst = _dapperService.Query<PizzaOrderInvoiceDetailModel>
                (
                    PizzaQuery.PizzaOrderDetailQuery,
                    new { PizzaOrderInvoiceNo = invoiceno }
                );

            var model = new PizzaOrderInvoiceResponse
            {
                Order = item,
                Detail = lst
            };

            return Ok(model);
        }
    }
}
