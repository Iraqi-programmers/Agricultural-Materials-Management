using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsSupplierPymentsData
    {
        public static async Task<int?> AddAsync(decimal amount, int? supplierId, DateTime pymentDate, int purchaseId, int? userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Amount", amount),
                new SqlParameter("@SupplierID", supplierId),
                new SqlParameter("@PaymentDate", pymentDate),
                new SqlParameter("@PurchaseID",purchaseId),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.AddAsync("SP_AddSupplierPayment", parameters);
        }

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int supplierId)
            => await CRUD.GetByColumnValueAsync("SP_GetSupplierPaymentsBySupplier", "@SupplierID", supplierId);

        public static async Task<bool> UpdateAsync(int? supplierPaymentId, decimal amount, DateTime pymentDate, int purchaseId, int? userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SupplierPaymentID", supplierPaymentId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@PaymentDate", pymentDate),
                new SqlParameter("@PurchaseID",purchaseId),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.UpdateAsync("SP_UpdateSupplierPayment", parameters);
        }

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllSupplierPayments");

        public static async Task<bool> DeleteAsync(int supplierPaymentId) => await CRUD.DeleteAsync("SP_DeleteSupplierPayment", "@SupplierPaymentID", supplierPaymentId);
    }
}
