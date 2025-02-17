using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsUsersData
    {
        // هنا احتمال نحتاج نمرر ايدي اليوزر الي ضاف اليوزر الجديد
        public static async Task<int?> AddNewUsers(int personId, string userName, string password, bool isActive, int currentUserId)
        {
            SqlParameter[] prameter =
            {
                new SqlParameter("@PersonID",personId),
                new SqlParameter("@UserName",userName),
                new SqlParameter("@Password",password),
                new SqlParameter("@IsActive",isActive),
                new SqlParameter("@UserID",currentUserId)
            };
            return await CRUD.AddAsync("SP_AddUser", prameter);
        }

        public static async Task<bool> UpdateUsers(int? userId, string userName, string password, bool isActive, int currentUserId)
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

        public static async Task<bool> DeleteUserByIDAsync(int userId, int currentUserId)
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

    //public class clsUsersData
    // {
    //     public static async Task<int?> AddNewUsers(int PersonID,string UserName,string Password,bool IsActive)
    //     {
    //         SqlParameter[] prameter =
    //         {
    //             new SqlParameter("@PersonID",PersonID),
    //             new SqlParameter("@UserName",UserName),
    //             new SqlParameter("@Password",Password),
    //             new SqlParameter("@IsActive",IsActive)
    //         };

    //        return await CRUD.AddAsync("SP_AddUser",prameter, CommandType.StoredProcedure);

    //     }

    //     public static async Task<bool>UpdateUsers(int? UserID,string UserName,string Password,bool IsActive)
    //     {
    //         SqlParameter[] Prameter =
    //         {
    //             new SqlParameter("@UserID",UserID),
    //             new SqlParameter("@UserName",UserName),
    //             new SqlParameter("@Password",Password),
    //             new SqlParameter("@IsActive",IsActive)

    //         };

    //         return await CRUD.UpdateAsync("SP_UpdateUser", Prameter, CommandType.StoredProcedure);
    //     }

    //     public static async Task<bool>DeleteUsers(int UserID)
    //     {
    //       return await  CRUD.DeleteAsync("SP_DeleteUser","UserID",UserID,CommandType.StoredProcedure);
    //     }

    //     public static async Task<object[]?> GetUserByID(int UserID)
    //     {
    //         return await CRUD.GetByColumnValueAsync("SP_GetUsersByID", "UserID", UserID, CommandType.StoredProcedure);
    //     }

    //     public static async Task<object[]?> GetUserByUserName(string UserName)
    //     {
    //         return await CRUD.GetByColumnValueAsync("SP_GetUsersByUserName", "UserName", UserName, CommandType.StoredProcedure);
    //     }

    //     public static async Task<DataTable?>GetAllUsers()
    //     {
    //         return await CRUD.GetAllAsDataTableAsync("",null, CommandType.StoredProcedure);
    //     }

    //     public static object? CheckIfUserNameAndPasswordExisst(string UserName, string Password)
    //     {
    //         SqlParameter[] pr =
    //         {
    //             new SqlParameter("@UserName",UserName),
    //             new SqlParameter("@Password",Password)
    //         };

    //         return  CRUD.Get("SP_AuthenticateUser", pr, CommandType.StoredProcedure);
    //     }

    // }
}
