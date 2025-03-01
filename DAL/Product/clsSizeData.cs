using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System.Data;
//Yousif
namespace DAL.Product
{
    public static class clsSizeData
    {
        public static async Task<DataTable?> GetAllAsDatatableAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllSizes");
        
        public static async Task<bool> DeleteAsync(int sizeID) => await CRUD.DeleteAsync("SP_DeleteType", "SizeID", sizeID);

        public static async Task<Dictionary<string, object>?> FindByIDAsync(int sizeID) => await CRUD.GetByColumnValueAsync("SP_GetSizeByID", "SizeID", sizeID);

        public static async Task<int?> AddAsync(double size)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@Size", size)
            };
            return await CRUD.AddAsync("SP_AddSize", sqlParameter);
        }

        public static async Task<bool> UpdateAsync(int? sizeId, double size)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SizeID", sizeId),
                new SqlParameter("@Size", size),
            };
            return await CRUD.UpdateAsync("SP_UpdateSiz", parameters);
        }
    }
}
