using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    //add PROCEDURE by(zaiun) اجتاج شرح عليه بعد مافهمته كله صح
    //Create By Abu Sanad
    public class clsUsersData
    {
        public static async Task<int?> AddNewUsers(string userName, string password, bool isActive, string FullName, string No, string PhoneNum, string Addrees)
        {
            SqlParameter[] prameter =
            {
                new SqlParameter("@UserName",userName),
                new SqlParameter("@Password",password),
                new SqlParameter("@IsActive",isActive),
                new SqlParameter("@FullName",FullName),
                new SqlParameter("@NationalNum",No),
                new SqlParameter("@PhoneNumber",PhoneNum),
                new SqlParameter("@Address",Addrees)
            };

            return await CRUD.AddAsync("SP_AddPersonThenUser", prameter);
        }

        public static async Task<bool> UpdateUsers(int userId, string userName, string password, bool isActive)
        {
            SqlParameter[] Prameter =
            {
                new SqlParameter("@UserID",userId),
                new SqlParameter("@UserName",userName),
                new SqlParameter("@Password",password),
                new SqlParameter("@IsActive",isActive)
            };
            return await CRUD.UpdateAsync("SP_UpdateUser", Prameter);
        }

        public static async Task<bool> DeleteUserByIDAsync(int userId)
          => await CRUD.DeleteAsync("SP_DeleteUser", "UserID", userId);
        

        public static async Task<Dictionary<string, object>?> GetUserByIDAsync(int userId)
          => await CRUD.GetByColumnValueAsync("SP_GetUsersByID", "UserID", userId);
        

        public static async Task<Dictionary<string, object>?> GetUserByUserNameAsync(string userName)
          => await CRUD.GetByColumnValueAsync("SP_GetUsersByUserName", "UserName", userName);
        

        public static async Task<DataTable?> GetAllUsersAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_GetAllUsers");
        

        public static async Task<object?> CheckIfUserNameAndPasswordExisst(string UserName, string Password)
        {
            SqlParameter[] pr =
            {
                new SqlParameter("@UserName",UserName),
                new SqlParameter("@Password",Password)
            };
            return await CRUD.GetAsync("SP_AuthenticateUser", pr) ?? null;
        }

    }

}
