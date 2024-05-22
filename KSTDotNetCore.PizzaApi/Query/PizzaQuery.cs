namespace KSTDotNetCore.PizzaApi.Query
{
    public class PizzaQuery
    {
        public static string PizzaOrderQuery { get; } =
            @"select po.*, p.Pizza, p.Price from [dbo].[Tbl_PizzaOrder] po
            inner join Tbl_Pizza p on p.PizzaId = po.PizzaId
            where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo;";

        public static string PizzaOrderDetailQuery { get; } =
            @"select pd.*, pe.PizzaExtraName, pe.Price from [dbo].[Tbl_PizzaOrderDetail] pd
            inner join Tbl_PizzaExtra pe on pe.PizzaExtraId = pd.PizzaExtraId
            where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo;";
    }
    
}
