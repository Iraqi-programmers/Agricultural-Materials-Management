using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsSalesDetailsData
    {
        public static async Task<int?> AddNewSaleDetailAsync(int stockId, uint quantity, DateTime warrantyDate, float price, float totaCost, int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@StockID", stockId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@WarrntyDate", warrantyDate),
                new SqlParameter("@Price", price),
                new SqlParameter("@TotaCost", totaCost),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.AddAsync("SP_", parameters);
        }

        public static async Task<object[]?> GetSaleDetailInfoByIDAsync(int detailId)
            => await CRUD.GetByColumnValueAsync("SP_", "DetailID", detailId);

        public static async Task<List<object[]>?> GetAllSaleDetailsAsListAsync()
            => await CRUD.GetAllAsListAsync("SP_");

        public static async Task<DataTable?> GetAllSaleDetailsAsDataTableAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_");

        public static async Task<bool> IsSaleDetailExistAsync(int detailId)
            => await CRUD.IsExistAsync("SP_", "DetailID", detailId);

        public static async Task<bool> UpdateSaleDetailDataAsync(int? detailId, int stockId, int quantity, DateTime warrantyDate, float price, float totaCost, int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@DetailID", detailId),
                new SqlParameter("@StockID", stockId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@WarrntyDate", warrantyDate),
                new SqlParameter("@Price", price),
                new SqlParameter("@TotaCost", totaCost),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.UpdateAsync("SP_", parameters);
        }

        public static async Task<bool> DeleteSaleDetailByIDAsync(int detailId, int userId)
            => await CRUD.DeleteAsync("SP_", "DetailID", detailId, "UserID", userId);

        public static async Task<bool> DeleteMultipleSaleDetailsAsync(List<int> detailIDs, int userId)
            => await CRUD.DeleteRecordsByIdsAsync("SP_", "SalesDetails", "DetailID", 0, detailIDs, "UserID", userId);
    }
}
