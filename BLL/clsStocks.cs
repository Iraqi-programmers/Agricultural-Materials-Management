using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    class clsStocks
    {
       public enum Mod { AddNew,Update}

        public Mod mod = Mod.AddNew;

        public int StockID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }

        //clsProductInfo ProductInfo
        private clsStocks(int stockID, int productID, int quantity, string status)
        {
            StockID = stockID;
            ProductID = productID;
            Quantity = quantity;
            Status = status;
            //ProductInfo=clsProductInfo.GetProductByID(ProductID);
            mod = Mod.Update ;

        }

        public clsStocks()
        {
            this.StockID = -1;
            this.ProductID = -1;
            this.Quantity = -1;
            this.Status = string.Empty;
            mod = Mod.AddNew;
        }

        public static async Task<clsStocks> GetStockByID(int StockID)
        {
            var obj = await clsStocksData.GetStockByID(StockID);

            if (obj != null)
            {
                return new clsStocks(
             Convert.ToInt32(obj[0]),
             Convert.ToInt32(obj[1]),
             Convert.ToInt32(obj[2]),
              obj[3].ToString()??""
               );
            }
            throw new Exception("Stock not Found.!");
        }

        public static async Task<bool> DeleteStock(int StockID)
        {
            return await clsStocksData.DeleteStock(StockID);
        }

        public static async Task<bool> UpdateStockQuantity(int stockId, int quantity)
        {
            return await clsStocksData.UpdateStockQuantity(stockId, quantity);
        }

        public static async Task<DataTable?> GetAllStocks()
        {
            return await clsStocksData.GetAllStocks();
        }

        private async Task<bool>AddNew()
        {
            this.StockID = await clsStocksData.AddNewStock(this.ProductID, this.Quantity, this.Status) ?? -1;

            return this.StockID !=-1;
        }

        private  async Task<bool>Update()
        {
            return await clsStocksData.UpdateStock(this.StockID, this.ProductID,this.Quantity, this.Status);
        }

        public async Task<bool>Save()
        {
            switch(mod)
            {
                case Mod.AddNew:
                    return await AddNew();

                case Mod.Update:
                        return await Update();
                    
            }
            return false;
        }

    }
}
