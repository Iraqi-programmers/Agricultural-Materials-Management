using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsSalesDetailsData
    {
        public static async Task<int?> AddNewSaleDetailAsync(int stockId, uint quantity, DateTime warrantyDate, float price, float totaCost)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@StockID", stockId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@WarrntyDate", warrantyDate),
                new SqlParameter("@Price", price),
                new SqlParameter("@TotaCost", totaCost),
                new SqlParameter("@Quantity", quantity)
            };
            return await CRUD.AddAsync("SP_", parameters, CommandType.StoredProcedure);
        }

        public static async Task<object[]?> GetSaleDetailInfoByIDAsync(int detailId)
            => await CRUD.GetByColumnValueAsync("SP_", "DetailID", detailId, CommandType.StoredProcedure);

        public static async Task<List<object[]>?> GetAllSaleDetailsAsListAsync()
            => await CRUD.GetAllAsListAsync("SP_", type: CommandType.StoredProcedure);

        public static async Task<DataTable?> GetAllSaleDetailsAsDataTableAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_", type: CommandType.StoredProcedure);

        public static async Task<bool> IsSaleDetailExistAsync(int detailID)
            => await CRUD.IsExistAsync("SP_", "DetailID", detailID, CommandType.StoredProcedure);

        public static async Task<bool> UpdateSaleDetailDataAsync(int? detailID, int stockID, int quantity, DateTime warrantyDate, float price, float totaCost)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@DetailID", detailID),
                new SqlParameter("@StockID", stockID),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@WarrntyDate", warrantyDate),
                new SqlParameter("@Price", price),
                new SqlParameter("@TotaCost", totaCost),
                new SqlParameter("@UserID", clsUsersData.CurrentUserAuditInfo.UserId)
            };
            return await CRUD.UpdateAsync("SP_", parameters, CommandType.StoredProcedure);
        }

        public static async Task<bool> DeleteSaleDetailByIDAsync(int detailID)
            => await CRUD.DeleteAsync("SP_", "DetailID", detailID, clsUsersData.CurrentUserAuditInfo, CommandType.StoredProcedure);

        public static async Task<bool> DeleteMultipleSaleDetailsAsync(List<int> detailIDs)
            => await CRUD.DeleteRecordsByIdsAsync("SP_", "SalesDetails", "DetailID", 0, detailIDs, clsUsersData.CurrentUserAuditInfo, CommandType.StoredProcedure);
    }
}
