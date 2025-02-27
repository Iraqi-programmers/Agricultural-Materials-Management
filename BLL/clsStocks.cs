using BLL.Product;
using DAL;
using System.ComponentModel;
using System.Data;

namespace BLL
{
    [Description("Create By Abu Sanad")]
    public class clsStocks : absClassesHelperBasc
    {
        public int Quantity { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public clsProduct ProductInfo { get; set; }

        private clsStocks(int stockID, int quantity, string status, decimal price, clsProduct productInfo)
        {
            Id = stockID;
            Quantity = quantity;
            Status = status;
            this.Price = price;
            this.ProductInfo = productInfo;
        }

        public clsStocks(clsProduct productedInfo, int quantity, string status, decimal price)
        {
            Id = null;
            this.ProductInfo = productedInfo;
            this.Quantity = quantity;
            this.Status = status;
            this.Price = price;
        }

        public static async Task<clsStocks?> GetStockByID(int StockID)
        {
            var obj = await clsStocksData.GetStockByID(StockID);
            if (obj != null) return null;
            else
                return __FetchStockData(ref obj);
        }

        public static async Task<bool> DeleteStockAsync(int StockID) => await clsStocksData.DeleteStock(StockID);

        public static async Task<bool> UpdateStockQuantityAsync(int stockId, int quantity) => await clsStocksData.UpdateStockQuantity(stockId, quantity);

        public static async Task<DataTable?> GetAllStocksAsync() => await clsStocksData.GetAllStocks();

        public async Task<bool> AddAsync()
        {
            Id = await clsStocksData.AddNewStock(ProductInfo.Id ?? null, this.Quantity, this.Status, this.Price);
            return Id != null;
        }
        public async Task<bool> UpdateAsync()
        => await clsStocksData.UpdateStock(Id, ProductInfo.Id ?? null, this.Quantity, this.Status, this.Price);


        //التعديل هنا بعد اكتمال كلاس البرودكت
        private static clsStocks __FetchStockData(ref Dictionary<string, object> dict)
        {
            return new clsStocks(
                (int)dict["StiockID"],
                (int)dict["Quantity"],
                (string)dict["Status"],
                (decimal)dict["Price"], new clsProduct() //Defuld Constrctor
                                                         //clsProduct.FetchProductData(ref dict)
            );
        }

    }
}
