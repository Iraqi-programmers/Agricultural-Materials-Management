using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsSalesDetailsData
    {
        public static async Task<int?> AddAsync(int stockId, uint quantity, DateTime warrantyDate, float price, float totaCost, int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@StockID", stockId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@WarrntyDate", warrantyDate),
                new SqlParameter("@Price", price),
                new SqlParameter("@TotaCost", totaCost),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.AddAsync("SP_", parameters);
        }

        // نحتاج ستورد
        public static async Task<Dictionary<string, object>?> GetByIdAsync(int detailId)
            => await CRUD.GetByColumnValueAsync("SP_", "SalesDetailID", detailId);

        public static async Task<List<Dictionary<string, object>>?> GetAllAsync(int saleId) 
            => await CRUD.GetAllAsListAsync("SP_", new SqlParameter[] { new SqlParameter("@SaleID", saleId) });

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_");
    }
}
