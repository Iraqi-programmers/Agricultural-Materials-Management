﻿using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsPersonData
    {
        public static async Task<int?> AddAsync(string fullName, string? nationalNum, string? phoneNum, string? address)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@NationalNum", nationalNum),
                new SqlParameter("@PhoneNumber", phoneNum),
                new SqlParameter("@Address", address)
            };
            return await CRUD.AddAsync("SP_", parameters);
        }

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int personId)
            => await CRUD.GetByColumnValueAsync("SP_", "PersonID", personId);
        public static async Task<Dictionary<string, object>?> GetByFullNameAsync(string fullName)
            => await CRUD.GetByColumnValueAsync("SP_", "FullName", fullName);
        public static async Task<Dictionary<string, object>?> GetByNationalNumAsync(string nationalNum)
            => await CRUD.GetByColumnValueAsync("SP_", "NationalNum", nationalNum);

        public static async Task<Dictionary<string, object>?> GetFullDataByIdAsync(int personId)
            => await CRUD.GetByColumnValueAsync("SP_", "PersonID", personId);
        public static async Task<Dictionary<string, object>?> GetFullDataByFullNameAsync(string fullName)
            => await CRUD.GetByColumnValueAsync("SP_", "FullName", fullName);
        public static async Task<Dictionary<string, object>?> GetFullDataPhoneNumAsync(string phoneNum)
            => await CRUD.GetByColumnValueAsync("SP_", "PhoneNumber", phoneNum);

        public static async Task<DataTable?> GetAllAsDataTableAsync() => await CRUD.GetAllAsDataTableAsync("SP_");
        public static async Task<List<Dictionary<string, object>>?> GetAllAsListAsync() => await CRUD.GetAllAsListAsync("SP_");

        public static async Task<bool> IsExistAsync(int personId)
            => await CRUD.IsExistAsync("SP_", "PersonID", personId);
        public static async Task<bool> IsExistByFullNameAsync(string fullName)
            => await CRUD.IsExistAsync("SP_", "FullName", fullName);
        public static async Task<bool> IsExistByNationalNumAsync(string nationalNum)
            => await CRUD.IsExistAsync("SP_", "NationalNum", nationalNum);

        public static async Task<bool> UpdateAsync(int? personId, string fullName, string? nationalNum, string? phoneNum, string? address)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PersonID", personId),
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@NationalNum", nationalNum),
                new SqlParameter("@PhoneNumber", phoneNum),
                new SqlParameter("@Address", address)
            };
            return await CRUD.UpdateAsync("SP_", parameters);
        }

        // ممنوع الحذف في حال كان ايدي البيرسن مرتبط بجداول اخرى 
        public static async Task<bool> DeleteByIdAsync(int? personId)
        {
            if (personId == null) return false;
            return await CRUD.DeleteAsync("SP_", "PersonID", personId);
        }
        public static async Task<bool> DeleteByFullNameAsync(string fullName)
            => await CRUD.DeleteAsync("SP_", "FullName", fullName);
        public static async Task<bool> DeleteByNationalNumAsync(string nationalNum)
            => await CRUD.DeleteAsync("SP_", "NationalNum", nationalNum);
    }
}
