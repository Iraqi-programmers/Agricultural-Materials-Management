using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private clsStocks(int stockID, int productID, int quantity, string status)
        {
            StockID = stockID;
            ProductID = productID;
            Quantity = quantity;
            Status = status;
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
