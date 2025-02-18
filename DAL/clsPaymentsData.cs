using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsPaymentsData
    {
        public static async Task<int?> AddNewPaymentAsync(int personId, double amount, int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PersonID", personId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.AddAsync("SP_", parameters);
        }

        public static async Task<object[]?> GetPaymentInfoByIDAsync(int paymentId)
            => await CRUD.GetByColumnValueAsync("SP_", "PaymentID", paymentId);

        public static async Task<DataTable?> GetAllPaymentsAsDataTableAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_");

        public static async Task<List<object[]>?> GetAllPaymentsAsListAsync()
            => await CRUD.GetAllAsListAsync("SP_");

        public static async Task<bool> IsPaymentExistAsync(int paymentId)
            => await CRUD.IsExistAsync("SP_", "PaymentID", paymentId);

        public static async Task<bool> UpdatePaymentDataAsync(int paymentId, double amount, int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PaymentID", paymentId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.UpdateAsync("SP_", parameters);
        }

        public static async Task<bool> DeletePaymentByIDAsync(int paymentId, int userId)
            => await CRUD.DeleteAsync("SP_", "PaymentID", paymentId, "UserID", userId);

        //public static async Task<bool> DeleteMultiplePaymentsAsync(List<int> paymentIDs, int userId)
        //    => await CRUD.DeleteRecordsByIdsAsync("SP_", "PeoplePayments", "PaymentID", 0, paymentIDs, "UserID", userId);
    }
}
