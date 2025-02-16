using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsPaymentsData
    {
        public static async Task<int?> AddNewPaymentAsync(int? personId, decimal amount, DateTime date)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PersonID", personId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@Date", date),
                new SqlParameter("@UserID", clsUsersData.CurrentUserAuditInfo.UserId)
            };
            return await CRUD.AddAsync("SP_", parameters, CommandType.StoredProcedure);
        }
        // لتسجل الدفع للزبائن العابرين
        public static async Task<int?> AddNewPaymentAsync(decimal amount, DateTime date)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Amount", amount),
                new SqlParameter("@Date", date),
                new SqlParameter("@UserID", clsUsersData.CurrentUserAuditInfo.UserId)
            };
            return await CRUD.AddAsync("SP_", parameters, CommandType.StoredProcedure);
        }

        public static async Task<object[]?> GetPaymentInfoByIDAsync(int paymentId)
            => await CRUD.GetByColumnValueAsync("SP_", "PaymentID", paymentId, CommandType.StoredProcedure);

        public static async Task<DataTable?> GetAllPaymentsAsDataTableAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_", type: CommandType.StoredProcedure);

        public static async Task<List<object[]>?> GetAllPaymentsAsListAsync()
            => await CRUD.GetAllAsListAsync("SP_", type: CommandType.StoredProcedure);

        public static async Task<bool> IsPaymentExistAsync(int paymentId)
            => await CRUD.IsExistAsync("SP_", "PaymentID", paymentId, CommandType.StoredProcedure);

        public static async Task<bool> UpdatePaymentDataAsync(int? paymentId, decimal amount, DateTime date)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PaymentID", paymentId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@Date", date),
                new SqlParameter("@UserID", clsUsersData.CurrentUserAuditInfo.UserId)
            };
            return await CRUD.UpdateAsync("SP_", parameters, CommandType.StoredProcedure);
        }

        public static async Task<bool> DeletePaymentByIDAsync(int paymentId)
            => await CRUD.DeleteAsync("SP_", "PaymentID", paymentId, clsUsersData.CurrentUserAuditInfo, CommandType.StoredProcedure);

        public static async Task<bool> DeleteMultiplePaymentsAsync(List<int> paymentIDs)
            => await CRUD.DeleteRecordsByIdsAsync("SP_", "PeoplePayments", "PaymentID", 0, paymentIDs, clsUsersData.CurrentUserAuditInfo, CommandType.StoredProcedure);
    }
}
