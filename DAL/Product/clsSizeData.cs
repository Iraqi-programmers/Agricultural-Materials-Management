using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System.Data;
//Yousif
namespace DAL.Product
{
    public static class clsSizeData
    {
        public static async Task<DataTable?> getAllAsDatatableAsync()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllSizes");

        }
        public static async Task<bool> deleteAsync(int SizeID)
        {

            return await CRUD.DeleteAsync("SP_DeleteType", "SizeID", SizeID);

        }

        public static async Task<object[]?> findByIDAsync(int SizeID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetSizeByID", "SizeID", SizeID);
        }

        public static async Task<int?> addAsync(double Size)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@Size", Size)
               
            };
            return await CRUD.AddAsync("SP_AddSize", sqlParameter);

        }
        public static async Task<bool> updateAsync(int? SizeID, double Size)
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
