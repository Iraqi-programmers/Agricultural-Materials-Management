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
   // CREATE  the class by Mohammed
    public class clsSupplierPymentsData
    {
        
        public static async Task<int?> AddSupplierPyment( decimal Amount,int SupplierID,DateTime PymentDate,int PurchaseID,int UserID)
        {
            SqlParameter[] Parameters =
            {
                new SqlParameter("@Amount", Amount),
                new SqlParameter("@SupplierID", SupplierID),
                new SqlParameter("@PaymentDate", PymentDate),
                new SqlParameter("@PurchaseID",PurchaseID),
                new SqlParameter("@UserID", UserID)
            };

            return await CRUD.AddAsync("SP_AddSupplierPayment", Parameters);
        }

        public static async Task<bool> UpdateSupplierPyment(int SupplierPaymentID, decimal Amount, DateTime PymentDate, int PurchaseID, int UserID)
        {
            SqlParameter[] Parameters =
            {
                new SqlParameter("@SupplierPaymentID", SupplierPaymentID),
                new SqlParameter("@Amount", Amount),
                new SqlParameter("@PaymentDate", PymentDate),
                new SqlParameter("@PurchaseID",PurchaseID),
                new SqlParameter("@UserID", UserID)
            };

            return await CRUD.UpdateAsync("SP_UpdateSupplierPayment", Parameters);

        }

        public static async Task<DataTable?> GetAllSupplierPayments()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllSupplierPayments");
        }

        public static async Task<bool> DeleteSupplierPyment(int ID)
        {
            return await CRUD.DeleteAsync("SP_DeleteSupplierPayment","@SupplierPaymentID",ID);
        }

        public static async Task<object[]?> GetSupplierPymentBySupplierID(int SupplierID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetSupplierPaymentsBySupplier","@SupplierID", SupplierID);
        }

    }
}
