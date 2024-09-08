using System;
using System.Collections.Generic;

namespace KSTDotNetCore.ConsoleAppEFCore.Models;

public partial class TblPieChart
{
    public int PieChartId { get; set; }

    public string? PieChartName { get; set; }

    public decimal? PieChartValue { get; set; }
}
