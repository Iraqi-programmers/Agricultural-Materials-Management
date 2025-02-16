using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsDebtData
    {
        public static async Task<int?> AddNewDebtAsync(int personId, decimal totalAmount, DateTime debtPaymentDate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PersonID", personId),
                new SqlParameter("@TotalAmount", totalAmount),
                new SqlParameter("@DebtPaymentDate", debtPaymentDate),
                new SqlParameter("@UserID", clsUsersData.CurrentUserAuditInfo.UserId)
            };
            return await CRUD.AddAsync("SP_", parameters, CommandType.StoredProcedure);
        }

        public static async Task<List<object[]>?> GetDebtsByPersonIDAsync(int personId)
            => await CRUD.GetAllAsListAsync("SP_", new SqlParameter[] { new SqlParameter("@PersonID", personId) }, CommandType.StoredProcedure);

        // SP_GetDebtsByDateRange
        public static async Task<List<object[]>> GetDebtsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            return await CRUD.GetAllAsListAsync("SP_GetDebtsByDateRange", parameters, CommandType.StoredProcedure) ?? new List<object[]>();
        }

        public static async Task<object[]?> GetDebtInfoByIDAsync(int debtId)
            => await CRUD.GetByColumnValueAsync("SP_", "DebtID", debtId, CommandType.StoredProcedure);
        public static async Task<object[]?> GetDebtInfoByPersonIDAsync(int personId)
            => await CRUD.GetByColumnValueAsync("SP_", "PersonID", personId, CommandType.StoredProcedure);
        public static async Task<object[]?> GetDebtInfoByPersonFullNameAsync(string fullName)
            => await CRUD.GetByColumnValueAsync("SP_", "FullName", fullName, CommandType.StoredProcedure);

        public static async Task<List<object[]>?> GetAllDebtsAsListAsync()
            => await CRUD.GetAllAsListAsync("SP_", type: CommandType.StoredProcedure);

        public static async Task<DataTable?> GetAllDebtsAsDataTableAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_", type: CommandType.StoredProcedure);

        public static async Task<bool> IsDebtExistAsync(int debtId)
            => await CRUD.IsExistAsync("SP_", "DebtID", debtId, CommandType.StoredProcedure);

        public static async Task<bool> UpdateDebtDataAsync(int? debtId, decimal totalAmount, DateTime debtPaymentDate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@DebtID", debtId),
                new SqlParameter("@TotalAmount", totalAmount),
                new SqlParameter("@DebtPaymentDate", debtPaymentDate)
            };
            return await CRUD.UpdateAsync("SP_", parameters, CommandType.StoredProcedure);
        }

        public static async Task<bool> DeleteDebtByIDAsync(int debtID)
            => await CRUD.DeleteAsync("SP_", "DebtID", debtID, clsUsersData.CurrentUserAuditInfo, CommandType.StoredProcedure);

        public static async Task<bool> DeleteMultipleDebtsAsync(List<int> debtIDs)
            => await CRUD.DeleteRecordsByIdsAsync("SP_", "Debts", "DebtID", 0, debtIDs, clsUsersData.CurrentUserAuditInfo, CommandType.StoredProcedure);
    }
}
