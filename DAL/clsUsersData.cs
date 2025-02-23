﻿using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    //add PROCEDURE by(zaiun) اجتاج شرح عليه بعد مافهمته كله صح
    //Create By Abu Sanad
    public class clsUsersData
    {
        public static async Task<Dictionary<string, object>?> GetUserByUserNameAndPasswordAsync(string userName, string password)
        {
            SqlParameter[] prameters =
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password)
            };
            return await CRUD.GetAsync("SP_", prameters);
        }

        public static async Task<Dictionary<string, object>?> GetUserByIdAsync(int userId)
            => await CRUD.GetByColumnValueAsync("SP_", "UserID", userId);

        public static async Task<Dictionary<string, object>?> GetUserByUserNameAsync(string userName)
          => await CRUD.GetByColumnValueAsync("SP_GetUsersByUserName", "UserName", userName);

        public static async Task<DataTable?> GetAllUsersAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_GetAllUsers");

        public static async Task<Dictionary<string, object>?> AddNewUserAsync(string userName, string? password, int? personId = null, string? fullName = null, string? nationalNum = null, string? phoneNumber = null, string? address = null)
        {
            SqlParameter[] prameters =
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password),
            };
            if (personId != null) prameters.Append(new SqlParameter("@PersonID", personId));
            else
            {
                if (fullName != null) prameters.Append(new SqlParameter("@FullName", fullName));
                if (nationalNum != null) prameters.Append(new SqlParameter("@NationalNum", nationalNum));
                if (phoneNumber != null) prameters.Append(new SqlParameter("@PhoneNumber", phoneNumber));
                if (address != null) prameters.Append(new SqlParameter("@Address", address));
            }
            // الستورد بروسيجر تستقبل يوزرنيم وباسورد
            // والبيرسن ايدي اذا كان يحتوي على قيمة يتم ارفاق قيمته مع ريكورد اليوزر 
            // وفي حال لا يحتوي على قيمة ياخذ البيرسن نيم وباقي البيانات ان وجدت وينشئ ريكورد بيرسن ويرجع دكشنري يحتوي على ايدي اليوزر وايدي البيرسن المضاف
            // في حال لم تنجح ترجع نال
            return await CRUD.GetAsync("SP_", prameters);
        }

        //public static async Task<int?> AddNewUsersAsync(string userName, string password, int? personId = null, string? fullName = null, string? nationalNum = null, string? phoneNumber = null, string? address = null)
        //{
        //    SqlParameter[] prameters =
        //    {
        //        new SqlParameter("@UserName", userName),
        //        new SqlParameter("@Password", password),
        //    };
        //    if (personId != null) prameters.Append(new SqlParameter("@PersonID", personId));
        //    else
        //    {
        //        if (fullName != null) prameters.Append(new SqlParameter("@FullName", fullName));
        //        if (nationalNum != null) prameters.Append(new SqlParameter("@NationalNum", nationalNum));
        //        if (phoneNumber != null) prameters.Append(new SqlParameter("@PhoneNumber", phoneNumber));
        //        if (address != null) prameters.Append(new SqlParameter("@Address", address));
        //    }
        //    return await CRUD.AddAsync("SP_AddPersonThenUser", prameters);
        //}

        // انشاء ستورد بروسيجر يعدل بيانات البيرسن فقط الي بيهن قييم واليوزر
        
        public static async Task<bool> UpdateUsersAsync(int? userId, string userName, string? password, bool isActive, string? fullName = null, string? nationalNum = null, string? phoneNumber = null, string? address = null)
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

        public static async Task<bool> DeleteUserByIdAsync(int userId)
            => await CRUD.DeleteAsync("SP_DeleteUser", "UserID", userId);
    }

}
