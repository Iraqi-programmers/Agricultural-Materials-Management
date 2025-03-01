
using MyLib_DotNet.DatabaseExecutor;
using System.Data;
using Microsoft.Data.SqlClient;
//Yousif
namespace DAL.Product
{
    public static class clsProductTypeData
    {
        public static async Task<DataTable?> GetAllAsync()
             => await CRUD.GetAllAsDataTableAsync("SP_GetAllProductTypes");

       
        public static async Task<bool> DeleteAsync(int typeId)
            => await CRUD.DeleteAsync("SP_DeleteType", "TypeID", typeId);

        
        
        public static async Task<Dictionary<string, object>?> FindByIdAsync(int typeId)
            => await CRUD.GetByColumnValueAsync("SP_GetTypeByID", "TypeID", typeId);
        
        public static async Task<object?> FindByNameAsync(string typeName)
            => await CRUD.GetByColumnValueAsync("SP_GetTypeByName", "TypeName", typeName);
        

        public static async Task<int?> AddAsync(string TypeName)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@TypeName", TypeName)
            };
            return await CRUD.AddAsync("SP_AddProductType", sqlParameter);
        }
        public static async Task<bool> UpdateAsync(int? TypeID, string TypeName)
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
