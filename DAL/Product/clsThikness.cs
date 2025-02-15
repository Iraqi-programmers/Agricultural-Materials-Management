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
    public static class clsThikness
    {
        public static async Task<DataTable?> GetAll()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllThickness", type: CommandType.StoredProcedure);

        }
        public static async Task<bool> Delete(int ThicknessID)
        {

            return await CRUD.DeleteAsync("SP_DeleteThickness", "ThicknessID", ThicknessID, CommandType.StoredProcedure);

        }

        public static async Task<object?> FindByID(int ThicknessID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetThicknessByID", "ThicknessID", ThicknessID, CommandType.StoredProcedure);
        }

        public static async Task<int?> Add(float Thickness)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@Thickness", Thickness)
            };
            return await CRUD.AddAsync("SP_AddThickness", sqlParameter, CommandType.StoredProcedure);
        }
        public static async Task<bool?> Update(int ThicknessID, float Thickness)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ThicknessID", ThicknessID),
                new SqlParameter("@Thickness", Thickness),
            };
            return await CRUD.UpdateAsync("SP_UpdateThickness", parameters, CommandType.StoredProcedure);
        }

    }
}
