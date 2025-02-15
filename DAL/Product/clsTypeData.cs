using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System.Data;

namespace DAL.Product
{
    public static class clsTypeData
    {
        public static async Task<DataTable?> GetAll()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllProductTypes", type: CommandType.StoredProcedure);

        }
        public static async Task<bool> Delete(int TypeID)
        {

            return await CRUD.DeleteAsync("SP_DeleteType", "TypeID", TypeID, CommandType.StoredProcedure);

        }

        public static async Task<object?> FindByID(int TypeID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetTypeByID", "TypeID", TypeID, CommandType.StoredProcedure);
        }
        public static async Task<object?> FindTypeByName(string TypeName)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetTypeByName", "TypeName", TypeName, CommandType.StoredProcedure);
        }

        public static async Task<int?> Add(string TypeName)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@TypeName", TypeName)
            };
            return await CRUD.AddAsync("SP_AddProductType", sqlParameter, CommandType.StoredProcedure);
        }
        public static async Task<bool?> Update(int TypeID, string TypeName)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TypeID", TypeID),
                new SqlParameter("@TypeName", TypeName),
            };
            return await CRUD.UpdateAsync("SP_UpdateType", parameters, CommandType.StoredProcedure);
        }


    }
}
