using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System.Data;

namespace DAL
{
    //Create By Abu Sanad
    public class clsWarrintyData
    {
        public static async Task<int?> AddNewWarrantyAsync(int period)
        {
            SqlParameter[] parameters =
            {
              new SqlParameter("@Period", period)
            };
            return await CRUD.AddAsync("SP_AddWarranty", parameters);
        }

        public static async Task<bool> UpdateWarrantyAsync(int warrantyId, int period)
        {
            SqlParameter[] parameters =
            {
        new SqlParameter("@WarrantyID", warrantyId),
        new SqlParameter("@Period", period)
    };
            return await CRUD.UpdateAsync("SP_UpdateWarranty", parameters);
        }

        public static async Task<bool> DeleteWarrantyAsync(int warrantyId)
        {
            return await CRUD.DeleteAsync("SP_DeleteWarranty", "WarrantyID", warrantyId);
        }

        public static async Task<object[]?> GetWarrantyByIDAsync(int warrantyId)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetWarrantyByID", "WarrantyID", warrantyId);
        }

        public static async Task<DataTable?> GetAllWarrantiesAsync()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllWarranties");
        }
    }
}
