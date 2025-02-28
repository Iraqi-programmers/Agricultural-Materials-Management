using System.Data;
using DAL;

namespace BLL
{
    public class clsSalesDetails : absClassesHelperBasc
    {
        public int SaleId { get; set; }
        public clsStocks Stock { get; private set; }
        public DateTime? WarrantyDate { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalCost { get; set; }

        public clsSalesDetails(clsStocks stock, double price, int quantity, double totalCost, uint period = 0)
        {
            Stock = stock;
            WarrantyDate = period > 0 ? GetDateAfterDays(period) : null;
            Price = price;
            Quantity = quantity;
            TotalCost = totalCost;
        }

        private clsSalesDetails(int? detailId, int saleId, clsStocks stock, double price, int quantity, double totalCost, DateTime? warrantyDate)
        {
            Id = detailId;
            SaleId = saleId;
            Stock = stock;
            Price = price;
            Quantity = quantity;
            TotalCost = totalCost;
            WarrantyDate = warrantyDate;
        }

        public static async Task<clsSalesDetails?> GetByIdAsync(int detailId)
        {
            var dict = await clsSalesDetailsData.GetByIdAsync(detailId);
            if (dict == null) return null;
            return FetchSalesDetailsData(ref dict);
        }

        //public clsSalesDetails(List<clsSalesDetails> salesDetailsList)
        //{
        //    SalesDetailsList = salesDetailsList;
        //}

        //public clsSalesDetails? GetByStockId(int stockId)
        //{
        //    var detail = SalesDetailsList.FirstOrDefault(d => d.StockId == stockId);
        //    if (detail != null)
        //    {
        //        Id = detail.Id;
        //        SaleId = detail.SaleId;
        //        WarrantyDate = detail.WarrantyDate;
        //        Price = detail.Price;
        //        Quantity = detail.Quantity;
        //        TotalCost = detail.TotalCost;
        //        return this;
        //    }
        //    return null;
        //}

        //public static bool UpdateProductDetail(int stockId, DateTime? warrantyDate = null, double? price = null, int? quantity = null, double? totalCost = null)
        //{
        //    var detail = SalesDetailsList.FirstOrDefault(d => d.StockId == stockId);
        //    if (detail != null)
        //    {
        //        detail.WarrantyDate = warrantyDate ?? detail.WarrantyDate;
        //        detail.Price = price ?? detail.Price;
        //        detail.Quantity = quantity ?? detail.Quantity;
        //        detail.TotalCost = totalCost ?? detail.TotalCost;
        //        return true;
        //    }
        //    return false;
        //}

        //public static bool RemoveProdectDetailFromList(int stockId)
        //{
        //    var detail = SalesDetailsList.FirstOrDefault(d => d.StockId == stockId);
        //    if (detail != null)
        //    {
        //        SalesDetailsList.Remove(detail);
        //        return true;
        //    }
        //    return false;
        //}

        //public bool UpdateWarrantyDate(byte day, byte month, ushort year)
        //{
        //    int currentYear = DateTime.Now.Year;

        //    if (day > 31 || month > 12 || year < currentYear)
        //        return false;
        //    try
        //    {
        //        WarrantyDate = new DateTime(year, month, day);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public static bool ClearSalesDetailsList()
        //{
        //    SalesDetailsList.Clear();
        //    return SalesDetailsList.Count == 0;
        //}

        public static async Task<DataTable?> GetAllAsync() => await clsSalesDetailsData.GetAllAsync();


        //public static async Task<clsSalesDetails?> LoadDetailsAsync(int saleId)
        //{
        //    var dataList = await clsSalesDetailsData.GetAllAsync(saleId);

        //    if (dataList == null || dataList.Count == 0)
        //        return null;

        //    List<clsSalesDetails> detailsList = dataList.Select(data => new clsSalesDetails(
        //        (int)data["DetailID"],
        //        saleId,  
        //        (int)data["StockID"], 
        //        (double)data["Price"], 
        //        (int)data["Quantity"], 
        //        (double)data["TotalCost"],
        //        data["WarrantyDate"] != DBNull.Value ? Convert.ToDateTime(data["WarrantyDate"]) : null)
        //    ).ToList();

        //    if (detailsList.Count == 0) return null;

        //    var firstDetail = detailsList.First();

        //    clsSalesDetails salesDetails = new clsSalesDetails(
        //        firstDetail.Id,
        //        firstDetail.SaleId,
        //        firstDetail.StockId,
        //        firstDetail.Price,
        //        firstDetail.Quantity,
        //        firstDetail.TotalCost,
        //        firstDetail.WarrantyDate
        //    );

        //    if (detailsList.Count > 1) detailsList.RemoveAt(0);

        //    SalesDetailsList = detailsList;

        //    return salesDetails;
        //}

        private DateTime? GetDateAfterDays(uint days) => DateTime.Now.AddDays(days);

        //public ushort CalculateTotalDays(byte years = 0, byte months = 0, byte days = 0)
        //    => (ushort)((years > 0 ? years * 365 : 0) + (months > 0 ? months * 30 : 0) + days); 

        internal static clsSalesDetails FetchSalesDetailsData(ref Dictionary<string, object> dict)
        {
            return new clsSalesDetails(
                (int)dict["SalesDetailID"],
                (int)dict["SaleID"],
                clsStocks.FetchStockData(ref dict),
                (double)dict["Price"],
                (int)dict["Quantity"],
                (double)dict["TotalCost"],
                (DateTime)dict["Date"]
            );
        }
    }
}
