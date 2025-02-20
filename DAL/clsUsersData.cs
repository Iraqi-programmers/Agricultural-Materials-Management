using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    //add PROCEDURE by(zaiun) اجتاج شرح عليه بعد مافهمته كله صح
    //Create By Abu Sanad
    public class clsUsersData
    {
        //public static async Task<int?> AddNewUsersAsync(string userName, string password, bool isActive, string fullName, string no, string phoneNum, string addrees)
        //{
        //    SqlParameter[] prameter =
        //    {
        //        new SqlParameter("@UserName", userName),
        //        new SqlParameter("@Password", password),
        //        new SqlParameter("@IsActive", isActive),
        //        new SqlParameter("@FullName", fullName),
        //        new SqlParameter("@NationalNum", no),
        //        new SqlParameter("@PhoneNumber", phoneNum),
        //        new SqlParameter("@Address", addrees)
        //    };

        //    return await CRUD.AddAsync("SP_AddPersonThenUser", prameter);
        //}

        public static async Task<Dictionary<string, object>?> GetUserByUserNameAndPasswordAsync(string userName, string password)
        {
            SqlParameter[] prameters =
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password)
            };
            return await CRUD.GetAsync("SP_", prameters);
        }
        public static async Task<int?> AddNewUsersAsync(string userName, string password, int? personId)
        {
            SqlParameter[] prameters =
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password),
                new SqlParameter("@PersonID", personId),
            };
            return await CRUD.AddAsync("SP_AddPersonThenUser", prameters);
        }

        public static async Task<int?> AddNewUsersAsync(string userName, string password, string fullName, string? nationalNum, string? phoneNum, string? addrees)
        {
            SqlParameter[] prameters =
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password),
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@NationalNum", nationalNum),
                new SqlParameter("@PhoneNumber", phoneNum),
                new SqlParameter("@Address", addrees)
            };
            return await CRUD.AddAsync("SP_AddPersonThenUser", prameters);
        }

        public static async Task<bool> UpdateUsersAsync(int userId, string userName, string password, bool isActive)
        {
            SqlParameter[] prameters =
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password),
                new SqlParameter("@IsActive", isActive)
            };
            return await CRUD.UpdateAsync("SP_UpdateUser", prameters);
        }

        public static async Task<bool> DeleteUserByIDAsync(int userId)
          => await CRUD.DeleteAsync("SP_DeleteUser", "UserID", userId);
        
        public static async Task<Dictionary<string, object>?> GetUserByIDAsync(int userId)
          => await CRUD.GetByColumnValueAsync("SP_GetUsersByID", "UserID", userId);
        
        public static async Task<Dictionary<string, object>?> GetUserByUserNameAsync(string userName)
          => await CRUD.GetByColumnValueAsync("SP_GetUsersByUserName", "UserName", userName);
        
        public static async Task<DataTable?> GetAllUsersAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_GetAllUsers");
        
        public static async Task<Dictionary<string, object>?> CheckIfUserNameAndPasswordExisstAsync(string userName, string password)
        {
            SqlParameter[] prameters =
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password)
            };
            return await CRUD.GetAsync("SP_AuthenticateUser", prameters);
        }

    }

}
