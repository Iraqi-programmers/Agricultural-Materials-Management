
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
        // هنا المفروض البرودكت ايدي ما يقبل null
        public static async Task<int?> AddAsync(int? productId, int quantity, string status, decimal price)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProductID", productId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@Status", status),
                new SqlParameter("@Price", price)
            };
            return await CRUD.AddAsync("SP_AddStock", parameters);
        }

        public static async Task<bool> UpdateAsync(int? stockId, int? productId, int quantity, string status, decimal price)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@StockID", stockId),
                new SqlParameter("@ProductID", productId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@Status", status),
                new SqlParameter("@Price", price),
            };
            return await CRUD.UpdateAsync("SP_UpdateStock", parameters);
        }

        public static async Task<bool> UpdateAsync(int stockId, int quantity)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@StockID", stockId),
                new SqlParameter("@Quantity", quantity)
            };
            return await CRUD.UpdateAsync("SP_UpdateStockQuantity", parameters);
        }

        public static async Task<bool> DeleteAsync(int stockId)
            => await CRUD.DeleteAsync("SP_DeleteStock", "StockID", stockId);
        
        public static async Task<Dictionary<string, object>?> GetByStockIdAsync(int stockId)
            => await CRUD.GetByColumnValueAsync("SP_GetStockByID", "StockID", stockId);
        
        public static async Task<DataTable?> GetAllAsync() 
            => await CRUD.GetAllAsDataTableAsync("SP_GetAllStocks");

        public static async Task<Dictionary<string, object>?> GetByProductIdAsync(int productId)
            => await CRUD.GetByColumnValueAsync("SP_GetStockByProductID", "ProductID", productId);

        public static async Task<DataTable?> GetAllStockByProductIdAsync(int productId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProductID", productId)
            };
            return await CRUD.GetAllAsDataTableAsync("SP_GetStockByProductID", parameters);
        }

        public static async Task<DataTable?> GetAllProductsByStatusAsync(string status)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Status", status)
            };
            return await CRUD.GetAllAsDataTableAsync("SP_GetStockByStatus", parameters);
        }
    }
}
