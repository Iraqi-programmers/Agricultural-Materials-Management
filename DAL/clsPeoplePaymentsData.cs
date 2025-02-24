using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsPeoplePaymentsData
    {
        public static async Task<int?> AddAsync(double amount, int userId, int saleId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Amount", amount),
                new SqlParameter("@UserID", userId),
                new SqlParameter("@SaleID", saleId)
            };
            return await CRUD.AddAsync("SP_AddPeoplePayment", parameters);
        }

        public static async Task<Dictionary<string, object>?> GetAsync(int paymentId)
            => await CRUD.GetByColumnValueAsync("SP_GetPaymentByID", "PaymentID", paymentId);

        public static async Task<DataTable?> GetAllAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_GetAllPayment");

        public static async Task<bool> UpdateAsync(int paymentId, double amount, int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PaymentID", paymentId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.UpdateAsync("SP_UpdatePayment", parameters);
        }

        public static async Task<bool> DeleteAsync(int paymentId)
            => await CRUD.DeleteAsync("SP_DeletePaymentByID", "PaymentID", paymentId);
    }

}
