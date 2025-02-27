using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsSupplierData
    {
        public static async Task<int?> AddAsync(string supplierName, string phone, bool isPerson, string address)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SupplierName", supplierName),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@IsPerson", isPerson),
                new SqlParameter("@Address", address)
            };
            return await CRUD.AddAsync("SP_AddSupplier", parameters);
        }

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int supplierId) => await CRUD.GetByColumnValueAsync("SP_GetSupplierByID", "@SupplierID", supplierId);
        public static async Task<Dictionary<string, object>?> GetByNameAsync(string supplierName) => await CRUD.GetByColumnValueAsync("SP_", "@SupplierName", supplierName);
        public static async Task<Dictionary<string, object>?> GetByPhoneAsync(string phone) => await CRUD.GetByColumnValueAsync("SP_", "@Phone", phone);

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllSuppliers");

        public static async Task<bool> UpdateAsync(int? supplierId, string supplierName, string phone, bool isPerson,  string address)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SupplierID", supplierId),
                new SqlParameter("@SupplierName", supplierName),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@IsPerson", isPerson),
                new SqlParameter("@Address", address)
            };
            return await CRUD.UpdateAsync("SP_UpdateSupplier", parameters);
        }

        public static async Task<bool> DeleteAsync(int? supplierId)
        {
            if (supplierId == null) return false;
            return await CRUD.DeleteAsync("SP_DeleteSupplier", "@SupplierID", supplierId);
        }
    }
}
