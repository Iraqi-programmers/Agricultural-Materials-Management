using DAL;
using System.ComponentModel;
using System.Data;

namespace BLL
{
    [Description("Create By Abu Sanad")]
    class clsStocks
    {
       public enum Mod { AddNew,Update}

        public Mod mod = Mod.AddNew;

        public int? StockID { get; private set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int WarrintyID { get; set; }
        public string Status { get; set; }
        public bool IsReturned { get; set; }
        public decimal Price { get; set; }


       // clsWarrinty WarrintyInfo
        //clsProductInfo ProductInfo
        private clsStocks(int stockID, int productID, int quantity, string status,bool IsReturned,decimal Price,int Warrinty)
        {
            StockID = stockID;
            ProductID = productID;
            Quantity = quantity;
            Status = status;
            this.WarrintyID = Warrinty;
            this.Price = Price;
            this.IsReturned = IsReturned;
            //ProductInfo=clsProductInfo.GetProductByID(ProductID);
           // WarrintyInfo= clsWarrintyInfo.GetWarrintyByID(WarrintyID)
            mod = Mod.Update ;

        }

        public clsStocks()
        {
            this.StockID = -1;
            this.ProductID = -1;
            this.Quantity = -1;
            this.Status = string.Empty;
            this.Price = -1;
            this.WarrintyID = -1;
            this.IsReturned = false;
            mod = Mod.AddNew;
        }

        public static async Task<clsStocks> GetStockByID(int StockID)
        {
            try
            {
                var obj = await clsStocksData.GetStockByID(StockID);

                if (obj != null)
                {
                    return new clsStocks(
                 Convert.ToInt32(obj[0]),
                 Convert.ToInt32(obj[1]),
                 Convert.ToInt32(obj[2]),
                  obj[3].ToString() ?? "",
                  Convert.ToBoolean(obj[4]),
                  Convert.ToDecimal(obj[5]),
                  Convert.ToInt32(obj[6])
                   );
                }
                throw new Exception("Stock not Found.!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentEx:" + ex);
                return null;
            }
            catch (Exception exo)
            {
                Console.WriteLine("Erorr:" + exo);
                return null;
            }
        }

        public static async Task<bool> DeleteStock(int StockID)
        {
            try
            {
                return await clsStocksData.DeleteStock(StockID);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentEx:" + ex);
                return false;
            }
            catch (Exception exo)
            {
                Console.WriteLine("Erorr:" + exo);
                return false;
            }
        }

        public static async Task<bool> UpdateStockQuantity(int stockId, int quantity)
        {
            return await clsStocksData.UpdateStockQuantity(stockId, quantity);
        }

        public static async Task<DataTable?> GetAllStocks()
        {
            try
            {
                return await clsStocksData.GetAllStocks();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentEx:" + ex);
                return null;
            }
            catch (Exception exo)
            {
                Console.WriteLine("Erorr:" + exo);
                return null;
            }
        }

        private async Task<bool>AddNew()
        {
            try
            {
                this.StockID = await clsStocksData.AddNewStock(this.ProductID, this.Quantity, this.Status,this.IsReturned,this.Price,this.WarrintyID);

                return this.StockID.HasValue;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentEx:" + ex);
                return false;
            }
            catch (Exception exo)
            {
                Console.WriteLine("Erorr:" + exo);
                return false;
            }
        }

        private  async Task<bool>Update()
        {
            try
            {
                return await clsStocksData.UpdateStock(this.StockID, this.ProductID, this.Quantity, this.Status,this.IsReturned,this.Price,this.WarrintyID);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentEx:" + ex);
                return false;
            }
            catch (Exception exo)
            {
                Console.WriteLine("Erorr:" + exo);
                return false;
            }
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
