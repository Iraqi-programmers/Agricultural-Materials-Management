using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsPurchasesDetailsData
    {
        public static async Task<int?> AddAsync(int purchaseId, int productId, decimal price, string status, int quantity, DateTime warrantyDate, int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PurchaseID", purchaseId),
                new SqlParameter("@ProductID", productId),
                new SqlParameter("@Price", price),
                new SqlParameter("@Status", status),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@WarrantyDate", warrantyDate),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.AddAsync("SP_", parameters);
        }

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllPurchaseDetails");
        
        public static async Task<Dictionary<string, object>?> GetByIdAsync(int purchaseId)
            => await CRUD.GetByColumnValue("SP_", new SqlParameter[] { new SqlParameter("@PurchaseID", purchaseId) });

        public static async Task<List<Dictionary<string, object>>?> GetByProductNameAsync(string productName)
            => await CRUD.GetAllAsListAsync("SP_", new SqlParameter[] { new SqlParameter("@ProductName", productName) });

        public static async Task<bool> UpdateAsync(int purchaseDetailId, decimal price, string status, int quantity, DateTime warrantyDate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PurchaseDetailID", purchaseDetailId),
                new SqlParameter("@Price", price),
                new SqlParameter("@Status", status),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@WarrantyDate", warrantyDate)
            };
            return await CRUD.UpdateAsync("SP_", parameters);
        }

        public static async Task<bool> DeleteAsync(int purchaseId) => await CRUD.DeleteAsync("SP_", "@PurchaseID", purchaseId);
    }
}
