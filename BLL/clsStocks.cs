using System.Data;
using BLL.Product;
using DAL;

namespace BLL
{
    public class clsStocks : absBaseEntity
    {
        public int Quantity { get; set; }
        public string Status { get; set; }
        public double PurchasePrice { get; set; } //
        public double SellingPrice { get; set; } // 
        public clsProduct ProductInfo { get; set; }

        public clsStocks(clsProduct productedInfo, int quantity, string status, double purchasePrice, double sellingPrice)
        {
            ProductInfo = productedInfo;
            Quantity = quantity;
            Status = status;
            PurchasePrice = purchasePrice;
            SellingPrice = sellingPrice;
        }

        private clsStocks(int stockId, int quantity, string status, double purchasePrice, double sellingPrice, clsProduct productInfo)
        {
            Id = stockId;
            Quantity = quantity;
            Status = status;
            PurchasePrice = purchasePrice;
            SellingPrice = sellingPrice;
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
            Id = await clsStocksData.AddAsync(ProductInfo.Id, Quantity, Status, SellingPrice);
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

        private async Task<bool> __UpdateAsync() => await clsStocksData.UpdateAsync(Id, ProductInfo.Id, Quantity, Status, SellingPrice);

        public static async Task<bool> DeleteAsync(int stockId) => await clsStocksData.DeleteAsync(stockId);

        internal static clsStocks FetchStockData(ref Dictionary<string, object> dict)
        {
            if (dict.TryGetValue("StockID", out var stockIdObj)) // تأكد من أن الاسم صحيح
            {
                int stockId = Convert.ToInt32(stockIdObj);
                // استخدم stockId هنا
            }
            else
            {
                Console.WriteLine("Warning: 'StockID' not found in dictionary!");
                // يمكنك تعيين قيمة افتراضية أو إدارة الخطأ بطريقة أخرى
            }
            return new clsStocks(
                (int)dict["StiockID"],
                (int)dict["Quantity"],
                (string)dict["Status"],
                (double)dict["PurchasePrice"],
                (double)dict["SellingPrice"],
                clsProduct.FetchProductData(dict) 
            );
        }
    }
}
