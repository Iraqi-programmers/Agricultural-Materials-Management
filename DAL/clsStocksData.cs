
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    //Create By Abu Sanad
    public class clsStocksData
    {
        public static async Task<Dictionary<string,object>?> AddNewStock(int? ProductID, int Quantity, string Status,bool IsReturned,decimal Price)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@ProductID", ProductID),
            new SqlParameter("@Quantity", Quantity),
            new SqlParameter("@Status", Status),
            new SqlParameter("@IsReturned", IsReturned),
            new SqlParameter("@Price", Price)
            
        };

            return await CRUD.GetAsync("SP_AddStock", parameters);
        }

        public static async Task<bool> UpdateStock(int? StockID, int? ProductID, int Quantity, string Status,bool IsReturned, decimal Price)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@StockID", StockID),
                new SqlParameter("@ProductID", ProductID),
                new SqlParameter("@Quantity", Quantity),
                new SqlParameter("@Status", Status),
                new SqlParameter("@IsReturned", IsReturned),
                new SqlParameter("@Price", Price),

            };

            return await CRUD.UpdateAsync("SP_UpdateStock", parameters);
        }

        public static async Task<bool> UpdateStockQuantity(int stockId, int quantity)
        {
            SqlParameter[] parameters =
           {
            new SqlParameter("@StockID", stockId),
            new SqlParameter("@Quantity", quantity)

           };

            return await CRUD.UpdateAsync("SP_UpdateStockQuantity", parameters);
        }

        public static async Task<bool> DeleteStock(int StockID)
         => await CRUD.DeleteAsync("SP_DeleteStock", "StockID", StockID);
        

        public static async Task<Dictionary<string, object>?> GetStockByID(int StockID)
        => await CRUD.GetByColumnValueAsync("SP_GetStockByID", "StockID", StockID);
        
        public static async Task<DataTable?> GetAllStocks() 
        => await CRUD.GetAllAsDataTableAsync("SP_GetAllStocks");
        
        public static async Task<Dictionary<string, object>?> GetStockByProductID(int ProductID)    
         => await CRUD.GetByColumnValueAsync("SP_GetStockByProductID", "ProductID", ProductID);
       
        public static async Task<DataTable?> GetAllStockByProductID(int ProductID)
        {
            SqlParameter[] pr =
            {
                new SqlParameter("@ProductID",ProductID)
            };

            return await CRUD.GetAllAsDataTableAsync("SP_GetStockByProductID",pr);
        }

        public static async Task<DataTable?> GetProductByStatus(string Status)
        {
            SqlParameter[] pr =
            {
                new SqlParameter("@Status",Status)
            };

            return await CRUD.GetAllAsDataTableAsync("SP_GetStockByStatus", pr);
        }

    }
}
