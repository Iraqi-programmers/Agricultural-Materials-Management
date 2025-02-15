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
    public static class clsSizeData
    {
        public static async Task<DataTable?> GetAll()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllSizes", type: CommandType.StoredProcedure);

        }
        public static async Task<bool> Delete(int SizeID)
        {

            return await CRUD.DeleteAsync("SP_DeleteType", "SizeID", SizeID, CommandType.StoredProcedure);

        }

        public static async Task<object?> FindByID(int SizeID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetSizeByID", "SizeID", SizeID, CommandType.StoredProcedure);
        }

        public static async Task<int?> Add(float Size)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@Size", Size)
            };
            return await CRUD.AddAsync("SP_AddSize", sqlParameter, CommandType.StoredProcedure);
        }
        public static async Task<bool?> Update(int SizeID, float Size)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SizeID", SizeID),
                new SqlParameter("@Size", Size),
            };
            return await CRUD.UpdateAsync("SP_UpdateSiz", parameters, CommandType.StoredProcedure);
        }


    }
}
