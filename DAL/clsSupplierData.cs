using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class clsSupplierData
    {
        public static async Task<int?> AddSupplier(string SupplierName, string Phone, string Address)
        {
            SqlParameter[] Parameters =
            {
                new SqlParameter("@SupplierName", SupplierName),
                new SqlParameter("@Phone", Phone),
                new SqlParameter("@Address", Address)
            };
           
           return await CRUD.AddAsync("SP_AddSupplier", Parameters, CommandType.StoredProcedure);
        }

        public static async Task<bool> UpdateSupplier(int ID, string SupplierName, string Phone, string Address)
        {
            SqlParameter[] Parameters =
            {
                new SqlParameter("@SupplierID", ID),
                new SqlParameter("@SupplierName", SupplierName),
                new SqlParameter("@Phone", Phone),
                new SqlParameter("@Address", Address)
            };
            
            return await CRUD.UpdateAsync("SP_UpdateSupplier", Parameters, CommandType.StoredProcedure);

        }

        public static async Task<DataTable?> GetAllSupplier()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllSuppliers");
        }

        public static async Task<bool> DeleteSupplier(int ID)
        {
            return await CRUD.DeleteAsync("SP_DeleteSupplier", "@SupplierID" ,ID, CommandType.StoredProcedure);
        }

        public static async Task<object[]?> GetSupplierByID(int ID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetSupplierByID", "@SupplierID", ID, CommandType.StoredProcedure);
        }

    }

}
