using System;
using System.Collections.Generic;

namespace KSTDotNetCore.RealtimeChart_2.Models;

public partial class TblPieChartt
{
    public int PieChartId { get; set; }

    public string PieChartName { get; set; } = null!;

    public decimal PieChartValue { get; set; }
}
