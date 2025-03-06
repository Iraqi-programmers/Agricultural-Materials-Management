using System.Data;
using DAL;

namespace BLL
{
    public class clsSalesDetails : absBaseEntity
    {
        public int SaleId { get; set; }
        public clsStocks Stock { get; private set; }
        public DateTime? WarrantyDate { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalCost { get; set; }
        public double Profit { get; set; } //


        public clsSalesDetails(clsStocks stock, double price, int quantity, double totalCost, double profit, uint period = 0)
        {
            Stock = stock;
            WarrantyDate = period > 0 ? GetDateAfterDays(period) : null;
            Price = price;
            Quantity = quantity;
            TotalCost = totalCost;
            Profit = profit;
        }

        internal clsSalesDetails(int? detailId, int saleId, clsStocks stock, double price, int quantity, double totalCost, double profit, DateTime? warrantyDate)
        {
            Id = detailId;
            SaleId = saleId;
            Stock = stock;
            Price = price;
            Quantity = quantity;
            TotalCost = totalCost;
            Profit = profit;
            WarrantyDate = warrantyDate;
        }

        public static async Task<clsSalesDetails?> GetByIdAsync(int detailId)
        {
            var dict = await clsSalesDetailsData.GetByIdAsync(detailId);
            if (dict == null) return null;
            return FetchSalesDetailsData(ref dict);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsSalesDetailsData.GetAllAsync();

        private DateTime? GetDateAfterDays(uint days) => DateTime.Now.AddDays(days);

        internal static clsSalesDetails FetchSalesDetailsData(ref Dictionary<string, object> dict)
        {
            return new clsSalesDetails(
                (int)dict["SalesDetailID"],
                (int)dict["SaleID"],
                clsStocks.FetchStockData(ref dict),
                (double)dict["Price"],
                (int)dict["Quantity"],
                (double)dict["TotalCost"],
                (double)dict["Profit"],
                (DateTime)dict["Date"]
            );
        }
    }
}
