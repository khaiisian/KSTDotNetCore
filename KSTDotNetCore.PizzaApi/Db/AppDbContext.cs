using KSTDotNetCore.PizzaApi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace KSTDotNetCore.PizzaApi.Db;

internal class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if you use MySQL or Oracle or other change name behind Use.
        optionsBuilder.UseSqlServer(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
    }
    public DbSet<PizzaModel> Pizzas { get; set; }

    public DbSet<PizzaExtraModel> PizzaExtra { get; set; }

    public DbSet<PizzaOrderModel> PizzaOrder { get; set;}

    public DbSet<PizzaOrderDetailModel> PizzaOrderDetail { get; set; }
}

[Table("Tbl_Pizza")]
public class PizzaModel
{
    [Key]
    [Column("PizzaId")]
    public int Id { get; set; }
    [Column("Pizza")]
    public string Name { get; set; }
    [Column("Price")]
    public decimal Price { get; set; }
}


[Table("Tbl_PizzaExtra")]
public class PizzaExtraModel
{
    [Column("PizzaExtraId")]
    public int Id { get; set; }

    [Column("PizzaExtraName")]
    public string Name { get; set; }

    [Column("Price")]
    public decimal Price {  get; set; }
    [NotMapped]
    public string priceStr { get { return "$"+Price; } }
}


public class OrderRequest
{
    public int PizzaId { get; set; }    
    public int[] Extras { get; set; }
}

[Table("Tbl_PizzaOrder")]
public class PizzaOrderModel
{
    [Key]
    public int PizzaOrderId { get; set; }
    public string PizzaOrderInvoiceNo { get; set; }
    public int PizzaId { get; set; }
    public decimal TotalAmount { get; set; }
}

[Table("Tbl_PizzaOrderDetail")]
public class PizzaOrderDetailModel
{
    [Key]
    public int PizzaOrderDetailId { get; set; }
    public string PizzaOrderInvoiceNo { get; set; }
    public int PizzaExtraId { get;set; }
}

public class OrderResponse
{
    public string Message { get; set; }
    public string InvoiceNum {  get; set; }
    public decimal TotalAmount { get; set; }

}