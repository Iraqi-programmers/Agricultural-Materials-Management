using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsPersonData
    {
        public static async Task<int?> AddNewPersonAsync(string fullName, string nationalNum, string phoneNum, string address)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@NationalNum", nationalNum),
                new SqlParameter("@PhoneNumber", phoneNum),
                new SqlParameter("@Address", address),
                new SqlParameter("@UserID", clsUsersData.CurrentUserAuditInfo.UserId)
            };
            return await CRUD.AddAsync("SP_", parameters, CommandType.StoredProcedure);
        }

        public static async Task<object[]?> GetPersonInfoByIDAsync(int personId)
            => await CRUD.GetByColumnValueAsync("SP_", "PersonID", personId, CommandType.StoredProcedure);
        public static async Task<object[]?> GetPersonInfoByFullNameAsync(string fullName)
            => await CRUD.GetByColumnValueAsync("SP_", "FullName", fullName, CommandType.StoredProcedure);
        public static async Task<object[]?> GetPersonInfoByNationalNumAsync(string nationalNum)
            => await CRUD.GetByColumnValueAsync("SP_", "NationalNum", nationalNum, CommandType.StoredProcedure);

        /*
        CREATE OR ALTER VIEW vw_PersonDetails AS
        SELECT 
            p.PersonID,
            p.FullName,
            p.NationalNum,
            p.PhoneNumber,
            p.Address,

            -- بيانات المدفوعات
            py.Amount AS PaymentAmount,
            py.Date AS PaymentDate,
            py.UserID AS PaymentUserID,
            u1.UserName AS PaymentUserName,

            -- بيانات الديون
            d.DebtAmount,
            d.DebtPaymentDate,

            -- بيانات المبيعات
            s.SaleID,
            s.SaleDate,
            s.UserID AS SaleUserID,
            u2.UserName AS SaleUserName,

            -- تفاصيل المبيعات
            sd.StockID,
            sd.Quantity,
            sd.WarrintyDate,
            sd.Price, 
            sd.TotalCost

        FROM Person p
        LEFT JOIN Payments py ON p.PersonID = py.PersonID
        LEFT JOIN Users u1 ON py.UserID = u1.UserID
        LEFT JOIN Debts d ON p.PersonID = d.PersonID
        LEFT JOIN Sales s ON p.PersonID = s.PersonID
        LEFT JOIN Users u2 ON s.UserID = u2.UserID
        LEFT JOIN SaleDetails sd ON s.SaleID = sd.SaleID;

         */
        /*
        CREATE PROCEDURE SP_GetPersonFullDetailsByID
            @PersonID INT
        AS
        BEGIN
            SELECT * FROM vw_PersonDetails WHERE PersonID = @PersonID;
        END;
         */
        public static async Task<object[]?> GetPersonFullDataByIDAsync(int personId)
            => await CRUD.GetByColumnValueAsync("SP_", "PersonID", personId, CommandType.StoredProcedure);

        public static async Task<object[]?> GetPersonFullDataByFullNameAsync(string fullName)
            => await CRUD.GetByColumnValueAsync("SP_", "FullName", fullName, CommandType.StoredProcedure);

        public static async Task<DataTable?> GetAllPersonsAsDataTableAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_", type: CommandType.StoredProcedure);
        public static async Task<List<object[]>?> GetAllPersonsAsListAsync()
            => await CRUD.GetAllAsListAsync("SP_", type: CommandType.StoredProcedure);

        /*
        CREATE PROCEDURE SP_GetAllPersonsFullDetails
        AS
        BEGIN
            SELECT * FROM vw_PersonDetails;
        END;
         */
        public static async Task<DataTable?> GetPersonFullDetailsByIDAsync(int personId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PersonID", personId)
            };
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllPersonsFullDetails", parameters, CommandType.StoredProcedure);
        }

        public static async Task<List<object[]>?> GetAllPersonsFullDetailsAsync()
            => await CRUD.GetAllAsListAsync("SP_GetAllPersonsFullDetails", type: CommandType.StoredProcedure);

        public static async Task<bool> IsPersonExistAsync(int personId)
            => await CRUD.IsExistAsync("SP_", "PersonID", personId, CommandType.StoredProcedure);
        public static async Task<bool> IsPersonExistAsync(string fullName)
            => await CRUD.IsExistAsync("SP_", "FullName", fullName, CommandType.StoredProcedure);

        public static async Task<bool> UpdatePersonDataAsync(int? personId, string fullName, string nationalNum, string phoneNum, string address)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PersonID", personId),
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@NationalNum", nationalNum),
                new SqlParameter("@PhoneNumber", phoneNum),
                new SqlParameter("@Address", address),
                new SqlParameter("@UserID", clsUsersData.CurrentUserAuditInfo.UserId)
            };
            return await CRUD.UpdateAsync("SP_", parameters, CommandType.StoredProcedure);
        }

        public static async Task<bool> DeletePersonByIDAsync(int personId)
            => await CRUD.DeleteAsync("SP_", "PersonID", personId, clsUsersData.CurrentUserAuditInfo, CommandType.StoredProcedure);
        public static async Task<bool> DeletePersonByFullNameAsync(string fullName)
            => await CRUD.DeleteAsync("SP_", "FullName", fullName, clsUsersData.CurrentUserAuditInfo, CommandType.StoredProcedure);
        public static async Task<bool> DeletePersonByNationalNumAsync(string nationalNum)
            => await CRUD.DeleteAsync("SP_", "NationalNum", nationalNum, clsUsersData.CurrentUserAuditInfo, CommandType.StoredProcedure);

        public static async Task<bool> DeleteMultiplePersonsAsync(List<int> personIDs)
            => await CRUD.DeleteRecordsByIdsAsync("SP_", "People", "PersonID", 0, personIDs, clsUsersData.CurrentUserAuditInfo, CommandType.StoredProcedure);
    }
}
