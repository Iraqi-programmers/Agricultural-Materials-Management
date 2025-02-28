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

        public clsStocks(clsProduct productedInfo, int quantity, string status, decimal price)
        {
            this.ProductInfo = productedInfo;
            this.Quantity = quantity;
            this.Status = status;
            this.Price = price;
        }

        private clsStocks(int stockId, int quantity, string status, decimal price, clsProduct productInfo)
        {
            Id = stockId;
            Quantity = quantity;
            Status = status;
            this.Price = price;
            this.ProductInfo = productInfo;
        }

        public static async Task<clsStocks?> GetByIDAsync(int stockId)
        {
            var obj = await clsStocksData.GetStockByID(stockId);
            if (obj == null) return null;
            return FetchStockData(ref obj);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsStocksData.GetAllStocks();

        //private static async Task<bool> __UpdateAsync(int stockId, int quantity) => await clsStocksData.UpdateStockQuantity(stockId, quantity);

        //private async Task<bool> __AddAsync()
        //{
        //    Id = await clsStocksData.AddNewStock(ProductInfo.Id ?? null, this.Quantity, this.Status, this.Price);
        //    return Id != null;
        //}

        //public async Task<bool> UpdateAsync()
        //    => await clsStocksData.UpdateStock(Id, ProductInfo.Id ?? null, this.Quantity, this.Status, this.Price);

        public static async Task<bool> DeleteAsync(int stockId) => await clsStocksData.DeleteStock(stockId);

        //التعديل هنا بعد اكتمال كلاس البرودكت
        internal static clsStocks FetchStockData(ref Dictionary<string, object> dict)
        {
            return new clsStocks(
                (int)dict["StiockID"],
                (int)dict["Quantity"],
                (string)dict["Status"],
                (decimal)dict["Price"], 
                new clsProduct() //Defuld Constrctor
                                 //clsProduct.FetchProductData(ref dict)
            );
        }
    }
}
