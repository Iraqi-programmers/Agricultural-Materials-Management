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
    public class clsReturnedProductsData
    {
        public static async Task<int?> AddNewReturnedProduct(int productId, DateTime dateOfReturned, int quantity, int detailId, int supplierId, int userId)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@ProductID", productId),
            new SqlParameter("@DateOfReturned", dateOfReturned),
            new SqlParameter("@Quantity", quantity),
            new SqlParameter("@DetailID", detailId),
            new SqlParameter("@SupplierID", supplierId),
            new SqlParameter("@UserID", userId)
        };

            return await CRUD.AddAsync("SP_AddReturnedProduct", parameters);
        }

        public static async Task<bool> UpdateReturnedProduct(int returnedProductId, int productId, DateTime dateOfReturned, int quantity, int detailId, int supplierId, int userId)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@ReturnedProductID", returnedProductId),
            new SqlParameter("@ProductID", productId),
            new SqlParameter("@DateOfReturned", dateOfReturned),
            new SqlParameter("@Quantity", quantity),
            new SqlParameter("@DetailID", detailId),
            new SqlParameter("@SupplierID", supplierId),
            new SqlParameter("@UserID", userId)
        };
            return await CRUD.UpdateAsync("SP_UpdateReturnedProduct", parameters);
        }

        public static async Task<bool> DeleteReturnedProductByIDAsync(int returnedProductId)
        {
            return await CRUD.DeleteAsync("SP_DeleteReturnedProduct", "ReturnedProductID", returnedProductId);
        }

        public static async Task<object[]?> GetReturnedProductByIDAsync(int returnedProductId)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetReturnedProductByID", "ReturnedProductID", returnedProductId);
        }

        public static async Task<DataTable?> GetAllReturnedProductsAsync()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllReturnedProducts");
        }

        public static  async Task<List<object[]>?> GetReturnedProductsByProductIDAsync(int productId)
        {
            SqlParameter[] Prameter =
            {
                new SqlParameter("@ProductedID",productId)
            };

            return await CRUD.GetAllAsListAsync("SP_GetReturnedProductsByProductID", Prameter);
        }
       
        public static async Task<List<object[]>?> GetReturnedProductsByDetailIDAsync(int DetailID)
        {
            SqlParameter[] Prameter =
            {
                new SqlParameter("@DetailID",DetailID)
            };

            return await CRUD.GetAllAsListAsync("SP_GetReturnedProductsByProductID", Prameter);
        }

        public static async Task<List<object[]>?> GetReturnedProductsBySupplierIDAsync(int SupplierID)
        {
            SqlParameter[] Prameter =
            {
                new SqlParameter("@ SupplierID", SupplierID)
            };

            return await CRUD.GetAllAsListAsync("SP_GetReturnedProductsBySupplierID", Prameter);
        }


    }
}
