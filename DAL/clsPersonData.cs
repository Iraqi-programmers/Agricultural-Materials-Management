using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsPersonData
    {
        public static async Task<int?> AddNewPersonAsync(string fullName, string nationalNum, string phoneNum, string address, int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@NationalNum", nationalNum),
                new SqlParameter("@PhoneNumber", phoneNum),
                new SqlParameter("@Address", address),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.AddAsync("SP_", parameters);
        }

        public static async Task<Dictionary<string, object>?> GetPersonInfoByIdAsync(int personId)
            => await CRUD.GetByColumnValueAsync("SP_", "PersonID", personId);
        public static async Task<Dictionary<string, object>?> GetPersonInfoByFullNameAsync(string fullName)
            => await CRUD.GetByColumnValueAsync("SP_", "FullName", fullName);
        public static async Task<Dictionary<string, object>?> GetPersonInfoByNationalNumAsync(string nationalNum)
            => await CRUD.GetByColumnValueAsync("SP_", "NationalNum", nationalNum);

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
        public static async Task<Dictionary<string, object>?> GetPersonFullDataByIDAsync(int personId)
            => await CRUD.GetByColumnValueAsync("SP_", "PersonID", personId);

        public static async Task<Dictionary<string, object>?> GetPersonFullDataByFullNameAsync(string fullName)
            => await CRUD.GetByColumnValueAsync("SP_", "FullName", fullName);

        public static async Task<DataTable?> GetAllPersonsAsDataTableAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_");
        public static async Task<List<Dictionary<string, object>>?> GetAllPersonsAsListAsync()
            => await CRUD.GetAllAsListAsync("SP_");

        /*
        CREATE PROCEDURE SP_GetAllPersonsFullDetails
        AS
        BEGIN
            SELECT * FROM vw_PersonDetails;
        END;
         */
        public static async Task<DataTable?> GetPersonFullDetailsByIDAsync(int personId)
            => await CRUD.GetAllAsDataTableAsync("SP_GetAllPersonsFullDetails", new SqlParameter[] { new SqlParameter("@PersonID", personId) });
        
        public static async Task<List<Dictionary<string, object>>?> GetAllPersonsFullDetailsAsync()
            => await CRUD.GetAllAsListAsync("SP_GetAllPersonsFullDetails");

        public static async Task<bool> IsPersonExistAsync(int personId)
            => await CRUD.IsExistAsync("SP_", "PersonID", personId);
        public static async Task<bool> IsPersonExistByFullNameAsync(string fullName)
            => await CRUD.IsExistAsync("SP_", "FullName", fullName);
        
        public static async Task<bool> IsPersonExistByNationalNumAsync(string nationalNum)
            => await CRUD.IsExistAsync("SP_", "NationalNum", nationalNum);

        public static async Task<bool> UpdatePersonDataAsync(int? personId, string fullName, string nationalNum, string phoneNum, string address, int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PersonID", personId),
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@NationalNum", nationalNum),
                new SqlParameter("@PhoneNumber", phoneNum),
                new SqlParameter("@Address", address),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.UpdateAsync("SP_", parameters);
        }

        public static async Task<bool> DeletePersonByIDAsync(int personId, int userId)
            => await CRUD.DeleteAsync("SP_", "PersonID", personId, "UserID", userId);
        public static async Task<bool> DeletePersonByFullNameAsync(string fullName, int userId)
            => await CRUD.DeleteAsync("SP_", "FullName", fullName, "UserID", userId);
        public static async Task<bool> DeletePersonByNationalNumAsync(string nationalNum, int userId)
            => await CRUD.DeleteAsync("SP_", "NationalNum", nationalNum, "UserID", userId);

        public static async Task<bool> DeleteMultiplePersonsAsync(List<int> personIDs, int userId)
            => await CRUD.DeleteRecordsByIdsAsync("SP_", "People", "PersonID", 0, personIDs, "UserID", userId);
    }
}
