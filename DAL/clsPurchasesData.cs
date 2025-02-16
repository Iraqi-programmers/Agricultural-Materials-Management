using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class clsPurchasesData
    {
        public static async Task<int?> AddPurchase(DateTime PurchaseDate,int SupplierID, decimal TotalPrice, decimal TotalPaid, int UserID)
        {
            SqlParameter[] Parameters =
            {
                new SqlParameter("@PurchaseDate", PurchaseDate),
                new SqlParameter("@SupplierID", SupplierID),
                new SqlParameter("@TotalPrice", TotalPrice),
                new SqlParameter("@TotalPaid", TotalPaid),
                new SqlParameter("@UserID", UserID)
            };

            return await CRUD.AddAsync("SP_AddPurchase", Parameters, CommandType.StoredProcedure);
        }

        public static async Task<bool> UpdatePurchase(int PurchaseID, DateTime PurchaseDate, int SupplierID, decimal TotalPrice, decimal TotalPaid, int UserID)
        {
            SqlParameter[] Parameters =
            {
                new SqlParameter("@PurchaseID", PurchaseID),
                new SqlParameter("@PurchaseDate", PurchaseDate),
                new SqlParameter("@SupplierID", SupplierID),
                new SqlParameter("@TotalPrice", TotalPrice),
                new SqlParameter("@TotalPaid", TotalPaid),
                new SqlParameter("@UserID", UserID)
            };

            return await CRUD.UpdateAsync("SP_UpdatePurchase", Parameters, CommandType.StoredProcedure);
        }

        public static async Task<DataTable?> GetAllPurchase()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllPurchases",null, CommandType.StoredProcedure);
        }

        public static async Task<bool> DeletePurchase(int PurchaseID)
        {
            return await CRUD.DeleteAsync("SP_DeletePurchase", "@PurchaseID", PurchaseID, CommandType.StoredProcedure);
        }


    }
}
