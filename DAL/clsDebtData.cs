using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsDebtData
    {
        public static async Task<int?> AddNewDebtAsync(int personId, decimal totalAmount, DateTime debtPaymentDate, int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PersonID", personId),
                new SqlParameter("@TotalAmount", totalAmount),
                new SqlParameter("@DebtPaymentDate", debtPaymentDate),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.AddAsync("SP_", parameters, CommandType.StoredProcedure);
        }

        public static async Task<List<object[]>?> GetDebtsByPersonIDAsync(int personId)
            => await CRUD.GetAllAsListAsync("SP_", new SqlParameter[] { new SqlParameter("@PersonID", personId) });

        // SP_GetDebtsByDateRange
        public static async Task<List<object[]>?> GetDebtsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            return await CRUD.GetAllAsListAsync("SP_GetDebtsByDateRange", parameters) ?? null;
        }

        public static async Task<object[]?> GetDebtInfoByIDAsync(int debtId)
            => await CRUD.GetByColumnValueAsync("SP_", "DebtID", debtId);
        public static async Task<object[]?> GetDebtInfoByPersonIDAsync(int personId)
            => await CRUD.GetByColumnValueAsync("SP_", "PersonID", personId);
        public static async Task<object[]?> GetDebtInfoByPersonFullNameAsync(string fullName)
            => await CRUD.GetByColumnValueAsync("SP_", "FullName", fullName);

        public static async Task<List<object[]>?> GetAllDebtsAsListAsync()
            => await CRUD.GetAllAsListAsync("SP_");

        public static async Task<DataTable?> GetAllDebtsAsDataTableAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_");

        public static async Task<bool> IsDebtExistAsync(int debtId)
            => await CRUD.IsExistAsync("SP_", "DebtID", debtId);

        public static async Task<bool> UpdateDebtDataAsync(int? debtId, decimal totalAmount, DateTime debtPaymentDate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@DebtID", debtId),
                new SqlParameter("@TotalAmount", totalAmount),
                new SqlParameter("@DebtPaymentDate", debtPaymentDate)
            };
            return await CRUD.UpdateAsync("SP_", parameters);
        }

        public static async Task<bool> DeleteDebtByIDAsync(int debtId, int userId)
            => await CRUD.DeleteAsync("SP_", "DebtID", debtId, "UserID", userId);

        public static async Task<bool> DeleteMultipleDebtsAsync(List<int> debtIDs, int userId)
            => await CRUD.DeleteRecordsByIdsAsync("SP_", "Debts", "DebtID", 0, debtIDs, "UserID", userId);
    }
}
