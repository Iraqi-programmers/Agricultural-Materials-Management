
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
    public class clsStocksData
    {


        public static async Task<int?> AddNewStock(int ProductID, int Quantity, string Status)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@ProductID", ProductID),
            new SqlParameter("@Quantity", Quantity),
            new SqlParameter("@Status", Status)
        };

            return await CRUD.AddAsync("SP_AddStock", parameters, CommandType.StoredProcedure);
        }

        public static async Task<bool> UpdateStock(int StockID, int ProductID, int Quantity, string Status)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@StockID", StockID),
            new SqlParameter("@ProductID", ProductID),
            new SqlParameter("@Quantity", Quantity),
            new SqlParameter("@Status", Status)
        };

            return await CRUD.UpdateAsync("SP_UpdateStock", parameters, CommandType.StoredProcedure);
        }

        public static async Task<bool> DeleteStock(int StockID)
        {
            return await CRUD.DeleteAsync("SP_DeleteStock", "StockID", StockID, CommandType.StoredProcedure);
        }

        public static async Task<object?> GetStockByID(int StockID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetStockByID", "StockID", StockID, CommandType.StoredProcedure);
        }

        public static async Task<DataTable?> GetAllStocks()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllStocks", null, CommandType.StoredProcedure);
        }

        public static async Task<object?> GetStockByProductID(int ProductID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetStockByProductID", "ProductID", ProductID, CommandType.StoredProcedure);
        }

        public static async Task<DataTable?> GetProductByStatus(string Status)
        {
            SqlParameter[] pr =
            {
                new SqlParameter("@Status",Status)
            };

            return await CRUD.GetAllAsDataTableAsync("SP_GetStockByStatus", pr, CommandType.StoredProcedure);
        }




    }
}
