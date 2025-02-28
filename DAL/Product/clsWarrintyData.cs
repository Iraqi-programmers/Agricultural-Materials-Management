using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System.Data;

namespace DAL
{
    // create PROCEDURE by (zaiun)
    //Create By Abu Sanad
    public class clsWarrintyData
    {
        public static async Task<int?> AddAsync(int period)
        {
            SqlParameter[] parameters =
            {
              new SqlParameter("@Period", period)
            };
            return await CRUD.AddAsync("SP_AddWarranty", parameters);
        }

        public static async Task<bool> UpdateAsync(int? warrantyId, int period)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@WarrantyID", warrantyId),
                new SqlParameter("@Period", period)
            };
            return await CRUD.UpdateAsync("SP_UpdateWarranty", parameters);
        }

        public static async Task<bool> DeleteAsync(int warrantyId)
            => await CRUD.DeleteAsync("SP_DeleteWarranty", "WarrantyID", warrantyId);
       

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int warrantyId)
            => await CRUD.GetByColumnValueAsync("SP_GetWarrantyByID", "WarrantyID", warrantyId);
        

        public static async Task<DataTable?> GetAllAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_GetAllWarranties");

       
    }
}
