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
        public static async Task<DataTable?> GetAllAsDatatableAsync()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllSizes");

        }
        public static async Task<bool> DeleteAsync(int SizeID)
        {

            return await CRUD.DeleteAsync("SP_DeleteType", "SizeID", SizeID);

        }

        public static async Task<object?> FindByIDAsync(int SizeID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetSizeByID", "SizeID", SizeID);
        }

        public static async Task<int?> AddAsync(float Size)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@Size", Size)
            };
            return await CRUD.AddAsync("SP_AddSize", sqlParameter);
        }
        public static async Task<bool?> UpdateAsync(int SizeID, float Size)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SizeID", SizeID),
                new SqlParameter("@Size", Size),
            };
            return await CRUD.UpdateAsync("SP_UpdateSiz", parameters);
        }


    }
}
