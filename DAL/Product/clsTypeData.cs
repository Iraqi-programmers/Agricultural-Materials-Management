using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System.Data;

namespace DAL.Product
{
    public static class clsTypeData
    {
        public static async Task<DataTable?> GetAllAsync()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllProductTypes");

        }
        public static async Task<bool> DeleteAsync(int TypeID)
        {

            return await CRUD.DeleteAsync("SP_DeleteType", "TypeID", TypeID);

        }

        public static async Task<object?> FindByIDAsync(int TypeID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetTypeByID", "TypeID", TypeID);
        }
        public static async Task<object?> FindTypeByNameAsync(string TypeName)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetTypeByName", "TypeName", TypeName);
        }

        public static async Task<int?> AddAsync(string TypeName)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@TypeName", TypeName)
            };
            return await CRUD.AddAsync("SP_AddProductType", sqlParameter);
        }
        public static async Task<bool?> UpdateAsync(int TypeID, string TypeName)
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
