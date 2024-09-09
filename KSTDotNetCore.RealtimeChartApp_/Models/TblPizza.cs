using System;
using System.Collections.Generic;

namespace KSTDotNetCore.RealtimeChartApp_.Models;

public partial class TblPizza
{
    public int PizzaId { get; set; }

    public string Pizza { get; set; } = null!;

    public decimal Price { get; set; }
}
