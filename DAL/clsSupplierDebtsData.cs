using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsSupplierDebtsData
    {
        public static async Task<int?> AddSupplierDebts(int SupplierID, decimal Amount, DateTime DeptPaymentDate)
        {
            SqlParameter[] Parameters =
            {
                new SqlParameter("@SupplierID", SupplierID),
                new SqlParameter("@Amount",Amount),
                new SqlParameter("@DebtPaymentDate",DeptPaymentDate)
            };

            return await CRUD.AddAsync("SP_AddSupplierDebt", Parameters, CommandType.StoredProcedure);
        }



        public static async Task<bool> UpdateSupplierDebt(int SupplierDebtID, decimal Amount, DateTime DeptPaymentDate)
        {
            SqlParameter[] Parameters =
            {
                new SqlParameter("@SupplierDebtID", SupplierDebtID),
                new SqlParameter("@Amount",Amount),
                new SqlParameter("@DebtPaymentDate",DeptPaymentDate)
            };

            return await CRUD.UpdateAsync("SP_UpdateSupplierDebt", Parameters, CommandType.StoredProcedure);

        }

        public static async Task<DataTable?> GetAllSupplierDept()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllSuppliersDebts");
        }

        public static async Task<bool> DeleteSupplierDebt(int ID)
        {
            return await CRUD.DeleteAsync("SP_DeleteSupplierDebt", "@SupplierDebtID", ID, CommandType.StoredProcedure);
        }

        public static async Task<object[]?> GetSupplierDebtByID(int ID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetSupplierDebtsByID", "@SupplierID", ID, CommandType.StoredProcedure);
        }




    }





} 


    
 
