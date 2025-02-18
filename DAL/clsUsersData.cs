using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    //Create By Abu Sanad
    public class clsUsersData
    {
        // هنا احتمال نحتاج نمرر ايدي اليوزر الي ضاف اليوزر الجديد
        public static async Task<int?> AddNewUsers( string userName, string password, bool isActive,string FullName,string No,string PhoneNum,string Addrees, int? currentUserId)
        {
            SqlParameter[] prameter =
            {
                new SqlParameter("@UserName",userName),
                new SqlParameter("@Password",password),
                new SqlParameter("@IsActive",isActive),
                new SqlParameter("@FullName",FullName),
                new SqlParameter("@NationNo",No),
                new SqlParameter("@PhoneNumber",PhoneNum),
                new SqlParameter("@Adress",Addrees),
                new SqlParameter("@UserID",currentUserId)
            };

            return await CRUD.AddAsync("SP_AddPersonThenUser", prameter);
        }

        public static async Task<bool> UpdateUsers(int? userId, string userName, string password, bool isActive, int? currentUserId)
        {
            SqlParameter[] Prameter =
            {
                new SqlParameter("@UserID",userId),
                new SqlParameter("@UserName",userName),
                new SqlParameter("@Password",password),
                new SqlParameter("@IsActive",isActive),
                new SqlParameter("@UserID",currentUserId)
            };
            return await CRUD.UpdateAsync("SP_UpdateUser", Prameter);
        }

        public static async Task<bool> DeleteUserByIDAsync(int userId, int? currentUserId)
        {
            return await CRUD.DeleteAsync("SP_DeleteUser", "UserID", userId, "UserID", currentUserId);
        }

        public static async Task<object[]?> GetUserByIDAsync(int userId)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetUsersByID", "UserID", userId);
        }

        public static async Task<object[]?> GetUserByUserNameAsync(string userName)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetUsersByUserName", "UserName", userName);
        }

        public static async Task<DataTable?> GetAllUsersAsync()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_");
        }

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
