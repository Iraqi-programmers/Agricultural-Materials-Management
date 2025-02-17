using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace DAL.Product
{
    public static class clsProductData
    {

        public static async Task<DataTable?> GetAllAsync()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllProducts");

        }

        public static async Task<bool> DeleteAsync(int productID)
        {

            return await CRUD.DeleteAsync("SP_DeleteProduct", "ProductID", productID);

        }

        public static async Task<object?> FindByIDAsync(int productID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetProductByID", "ProductID", productID);
        }

        public static async Task<object?> FindByCompanyIDAsync(int CompanyID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetProductByCompanyID", "CompanyID", CompanyID);

        }

        public static async Task<object?> FindByTypeIDAsync(int TypeID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetProductByTypeID", "TypeID", TypeID);
        }

        public static async Task<int?> AddProductWithAllDetailsAsync(string TypeName, string CompanyName, float Size, float Thickness)
        {
            SqlParameter[] parameters =
{
                new SqlParameter("@@TypeName", TypeName),
                new SqlParameter("@CompanyName", CompanyName),
                new SqlParameter("@Thickness", Thickness),
                new SqlParameter("@Size", Size)

            };
            return await CRUD.AddAsync("SP_AddProductWithAllDetails", parameters);

        }

        public static async Task<bool?> UpdateAsync(int productID, int TypeID, int CompanyID, int DetailsID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TypeID", TypeID),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@DetailsID", DetailsID)
            };
            return await CRUD.UpdateAsync("SP_UpdateProduct", parameters);
        }

    }
}
