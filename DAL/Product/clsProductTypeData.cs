
using MyLib_DotNet.DatabaseExecutor;
using System.Data;
using Microsoft.Data.SqlClient;
//Yousif
namespace DAL.Product
{
    public static class clsProductTypeData
    {
        public static async Task<DataTable?> getAllAsDatatableAsync()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllProductTypes");

        }
        public static async Task<bool> deleteAsync(int TypeID)
        {

            return await CRUD.DeleteAsync("SP_DeleteType", "TypeID", TypeID);

        }
        
        public static async Task<object[]?> findByIDAsync(int TypeID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetTypeByID", "TypeID", TypeID);
        }
        public static async Task<object?> findTypeByNameAsync(string TypeName)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetTypeByName", "TypeName", TypeName);
        }

        public static async Task<int?> addAsync(string TypeName)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@TypeName", TypeName)
            };
            return await CRUD.AddAsync("SP_AddProductType", sqlParameter);
        }
        public static async Task<bool> updateAsync(int? TypeID, string TypeName)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TypeID", TypeID),
                new SqlParameter("@TypeName", TypeName),
            };
            return await CRUD.UpdateAsync("SP_UpdateType", parameters);
        }


    }
}
