using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using Newtonsoft.Json;

namespace DAL
{
    public class clsPurchasesData
    {
        public static async Task<int?> AddAsync(string details, DateTime purchaseDate, int supplierId, double totalPrice, double? totalPaid, bool isDebt, int userId)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@Details", details),
                new SqlParameter("@PurchaseDate", purchaseDate),
                new SqlParameter("@SupplierID", supplierId),
                new SqlParameter("@TotalPrice", totalPrice),
                new SqlParameter("@TotalPaid", totalPaid),
                new SqlParameter("@IsDebt", isDebt),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.AddAsync("SP_AddPurchaseWithDetails", parameters);
        }

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int purchaseId)
        {
            var result = await CRUD.GetByColumnValueAsync("SP_GetPurchaseWithDetailsById", "PurchaseID", purchaseId);
            if (result == null) return null;

            if (result.ContainsKey("PurchaseDetailsJson") && result["PurchaseDetailsJson"] != DBNull.Value)
            {
                string detailsJson = result["PurchaseDetailsJson"].ToString()!;
                result["PurchaseDetails"] = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(detailsJson) ?? new List<Dictionary<string, object>>();
            }
            return result;
        }

        public static async Task<DataTable?> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            return await CRUD.GetAllAsDataTableAsync("SP_GetPurchasesByDateRange", parameters);
        }

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllPurchases");

        public static async Task<bool> UpdateAsync(string details, DateTime purchaseDate, int? purchaseId, int supplierId, double totalPrice, double? totalPaid, bool isDebt, int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Details", details),
                new SqlParameter("@PurchaseID", purchaseId),
                new SqlParameter("@PurchaseDate", purchaseDate),
                new SqlParameter("@SupplierID", supplierId),
                new SqlParameter("@TotalPrice", totalPrice),
                new SqlParameter("@TotalPaid", totalPaid),
                new SqlParameter("@IsDebt", isDebt),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.UpdateAsync("SP_UpdatePurchaseDetails", parameters);
        }

        public static async Task<bool> DeleteAsync(int? purchaseId)
        {
            if (purchaseId == null) return false;
            return await CRUD.DeleteAsync("SP_DeletePurchase", "@PurchaseID", purchaseId);
        }
    }
}
