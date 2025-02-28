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
    public class clsReturnedStocksData
    {
        public static async Task<int?> AddNew(int? SaleDetailID, int quantity, int? supplierId, int? userId)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@SaleDetailID", SaleDetailID),
            new SqlParameter("@supplierId", supplierId),
            new SqlParameter("@Quantity", quantity),
            new SqlParameter("@SupplierID", supplierId),
            new SqlParameter("@UserID", userId)
        };

            return await CRUD.AddAsync("SP_AddReturnedProduct", parameters);
        }

        public static async Task<bool> Update(int? SaleDetailID, int quantity, int? supplierId, int? userId)
        {
            SqlParameter[] parameters =
             {
            new SqlParameter("@SaleDetailID", SaleDetailID),
            new SqlParameter("@supplierId", supplierId),
            new SqlParameter("@Quantity", quantity),
            new SqlParameter("@SupplierID", supplierId),
            new SqlParameter("@UserID", userId)
        };
            return await CRUD.UpdateAsync("SP_UpdateReturnedProduct", parameters);
        }

        public static async Task<bool> Delete(int returnedProductId)
        {
            return await CRUD.DeleteAsync("SP_DeleteReturnedProduct", "ReturnedProductID", returnedProductId);
        }

        public static async Task<Dictionary<string,object>?> GetByIDAsync(int returnedProductId)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetReturnedProductByID", "ReturnedProductID", returnedProductId);
        }

        public static async Task<DataTable?> GetAllAsync()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllReturnedProducts");
        }


        //public static  async Task<List<object[]>?> GetReturnedProductsByProductIDAsync(int productId)
        //{
        //    SqlParameter[] Prameter =
        //    {
        //        new SqlParameter("@ProductedID",productId)
        //    };

        //    return await CRUD.GetAllAsListAsync("SP_GetReturnedProductsByProductID", Prameter);
        //}
       
        //public static async Task<List<object[]>?> GetReturnedProductsByDetailIDAsync(int DetailID)
        //{
        //    SqlParameter[] Prameter =
        //    {
        //        new SqlParameter("@DetailID",DetailID)
        //    };

        //    return await CRUD.GetAllAsListAsync("SP_GetReturnedProductsByProductID", Prameter);
        //}

        //public static async Task<List<object[]>?> GetReturnedProductsBySupplierIDAsync(int SupplierID)
        //{
        //    SqlParameter[] Prameter =
        //    {
        //        new SqlParameter("@ SupplierID", SupplierID)
        //    };

        //    return await CRUD.GetAllAsListAsync("SP_GetReturnedProductsBySupplierID", Prameter);
        //}


    }
}
