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
    //Create By Abu Sanad

    public class clsWarrintyReturnesToPeopleData
    {
        public static async Task<Dictionary<string, object>?> AddNew(int? warrantyReturnedID, int returndStocksID, int personID, int? userID)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@returndStocksID", returndStocksID),
            new SqlParameter("@Status", personID),
            new SqlParameter("@userID", userID),

        };

            return await CRUD.GetAsync("SP_Add", parameters);
        }

        public static async Task<bool> Update(int? returndStocksID, int? personID, int? userID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@returndStocksID", returndStocksID),
                new SqlParameter("@personID", personID),
                new SqlParameter("@userID", userID),
              

            };

            return await CRUD.UpdateAsync("SP_Update", parameters);
        }

        public static async Task<bool> Delete(int WarrantyReturnedID)
         => await CRUD.DeleteAsync("SP_Delete", "WarrantyReturnedID", WarrantyReturnedID);


        public static async Task<Dictionary<string, object>?> GetByID(int WarrantyReturnedID)
        => await CRUD.GetByColumnValueAsync("SP_GetByID", "WarrantyReturnedID", WarrantyReturnedID);

        public static async Task<DataTable?> GetAll()
        => await CRUD.GetAllAsDataTableAsync("SP_GetAll");

        public static async Task<Dictionary<string, object>?> GetByPersonID(int personid)
         => await CRUD.GetByColumnValueAsync("SP_GetByPersonID", "PersonID", personid);

        public static async Task<DataTable?> GetAllStockByProductID(int ProductID)
        {
            SqlParameter[] pr =
            {
                new SqlParameter("@ProductID",ProductID)
            };

            return await CRUD.GetAllAsDataTableAsync("SP_GetStockByProductID", pr);
        }

    }
}
