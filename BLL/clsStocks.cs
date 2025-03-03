using System.Data;
using BLL.Product;
using DAL;

namespace BLL
{
    public class clsStocks : absBaseEntity
    {
        public int Quantity { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public clsProduct ProductInfo { get; set; }

        public clsStocks(clsProduct productedInfo, int quantity, string status, double price)
        {
            ProductInfo = productedInfo;
            Quantity = quantity;
            Status = status;
            Price = price;
        }

        private clsStocks(int stockId, int quantity, string status, double price, clsProduct productInfo)
        {
            Id = stockId;
            Quantity = quantity;
            Status = status;
            Price = price;
            ProductInfo = productInfo;
        }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsStocksData.AddAsync(ProductInfo.Id, Quantity, Status, Price);
            return Id != null;
        }

        public static async Task<clsStocks?> GetByIdAsync(int stockId)
        {
            var dict = await clsStocksData.GetByStockIdAsync(stockId);
            if (dict == null) return null;
            return FetchStockData(ref dict);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsStocksData.GetAllAsync();

        //private static async Task<bool> __UpdateAsync(int stockId, int quantity) => await clsStocksData.UpdateStockQuantity(stockId, quantity);

        private async Task<bool> __UpdateAsync() => await clsStocksData.UpdateAsync(Id, ProductInfo.Id, Quantity, Status, Price);

        public static async Task<bool> DeleteAsync(int stockId) => await clsStocksData.DeleteAsync(stockId);

        internal static clsStocks FetchStockData(ref Dictionary<string, object> dict)
        {
            return new clsStocks(
                (int)dict["StiockID"],
                (int)dict["Quantity"],
                (string)dict["Status"],
                (double)dict["Price"], 
                clsProduct.FetchProductData(dict) 
            );
        }
    }
}
