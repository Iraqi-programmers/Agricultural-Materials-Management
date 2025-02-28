using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    //add PROCEDURE by(zaiun) اجتاج شرح عليه بعد مافهمته كله صح
    //Create By Abu Sanad
    public class clsUsersData
    {
        public static async Task<int?> AddAsync(string userName, string password, int? personId)
        {
            SqlParameter[] prameters =
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password),
                new SqlParameter("@PersonID", personId)
            };
            return await CRUD.AddAsync("SP_", prameters);
        }

        public static async Task<Dictionary<string, object>?> GetByUserNameAndPasswordAsync(string userName, string password)
        {
            SqlParameter[] prameters =
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password)
            };
            return await CRUD.GetAsync("SP_", prameters);
        }

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int userId)
            => await CRUD.GetByColumnValueAsync("SP_", "UserID", userId);

        public static async Task<Dictionary<string, object>?> GetByUserNameAsync(string userName) 
            => await CRUD.GetByColumnValueAsync("SP_GetUsersByUserName", "UserName", userName);

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllUsers");

        
        // انشاء ستورد بروسيجر يعدل بيانات البيرسن فقط الي بيهن قييم واليوزر
        public static async Task<bool> UpdateAsync(int? userId, string userName, string? password, bool isActive, string? fullName = null, string? nationalNum = null, string? phoneNumber = null, string? address = null)
        {
            SqlParameter[] prameters =
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password),
                new SqlParameter("@IsActive", isActive)
            };
            if (fullName != null) prameters.Append(new SqlParameter("@FullName", fullName));
            if (nationalNum != null) prameters.Append(new SqlParameter("@NationalNum", nationalNum));
            if (phoneNumber != null) prameters.Append(new SqlParameter("@PhoneNumber", phoneNumber));
            if (address != null) prameters.Append(new SqlParameter("@Address", address));

            return await CRUD.UpdateAsync("SP_UpdateUser", prameters);
        }

        public static async Task<bool> DeleteByIdAsync(int userId) => await CRUD.DeleteAsync("SP_DeleteUser", "UserID", userId);
    }

}
