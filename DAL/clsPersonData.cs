using System.Data;
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

        public static async Task<Dictionary<string, object>?> GetPersonInfoByIdAsync(int personId)
            => await CRUD.GetByColumnValueAsync("SP_", "PersonID", personId);
        public static async Task<Dictionary<string, object>?> GetPersonInfoByFullNameAsync(string fullName)
            => await CRUD.GetByColumnValueAsync("SP_", "FullName", fullName);
        public static async Task<Dictionary<string, object>?> GetPersonInfoByNationalNumAsync(string nationalNum)
            => await CRUD.GetByColumnValueAsync("SP_", "NationalNum", nationalNum);

        public static async Task<Dictionary<string, object>?> GetPersonFullDataByIdAsync(int personId)
            => await CRUD.GetByColumnValueAsync("SP_", "PersonID", personId);
        public static async Task<Dictionary<string, object>?> GetPersonFullDataByFullNameAsync(string fullName)
            => await CRUD.GetByColumnValueAsync("SP_", "FullName", fullName);
        public static async Task<Dictionary<string, object>?> GetPersonFullDataPhoneNumAsync(string phoneNum)
            => await CRUD.GetByColumnValueAsync("SP_", "PhoneNumber", phoneNum);

        public static async Task<DataTable?> GetAllAsDataTableAsync() => await CRUD.GetAllAsDataTableAsync("SP_");
        public static async Task<List<Dictionary<string, object>>?> GetAllAsListAsync() => await CRUD.GetAllAsListAsync("SP_");

        public static async Task<bool> IsPersonExistAsync(int personId)
            => await CRUD.IsExistAsync("SP_", "PersonID", personId);
        public static async Task<bool> IsPersonExistByFullNameAsync(string fullName)
            => await CRUD.IsExistAsync("SP_", "FullName", fullName);
        public static async Task<bool> IsPersonExistByNationalNumAsync(string nationalNum)
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
        public static async Task<bool> DeletePersonByIdAsync(int? personId)
        {
            if (personId == null) return false;
            return await CRUD.DeleteAsync("SP_", "PersonID", personId);
        }
        public static async Task<bool> DeletePersonByFullNameAsync(string fullName)
            => await CRUD.DeleteAsync("SP_", "FullName", fullName);
        public static async Task<bool> DeletePersonByNationalNumAsync(string nationalNum)
            => await CRUD.DeleteAsync("SP_", "NationalNum", nationalNum);
    }
}
