using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Product
{
    public static class clsThiknessData
    {
        public static async Task<DataTable?> GetAllAsDataTableAsync()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllThickness");

        }
        public static async Task<bool> DeleteAsync(int ThicknessID)
        {

            return await CRUD.DeleteAsync("SP_DeleteThickness", "ThicknessID", ThicknessID);

        }

        public static async Task<object?> FindByIDAsync(int ThicknessID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetThicknessByID", "ThicknessID", ThicknessID);
        }

        public static async Task<int?> AddAsync(float Thickness)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@Thickness", Thickness)
            };
            return await CRUD.AddAsync("SP_AddThickness", sqlParameter);
        }
        public static async Task<bool?> UpdateAsync(int ThicknessID, float Thickness)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ThicknessID", ThicknessID),
                new SqlParameter("@Thickness", Thickness),
            };
            return await CRUD.UpdateAsync("SP_UpdateThickness", parameters);
        }

    }
}
