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
             => await CRUD.GetAllAsDataTableAsync("SP_GetAllProducts");
       
        public static async Task<bool> DeleteAsync(int productId)
            => await CRUD.DeleteAsync("SP_DeleteProduct", "ProductID", productId);

       

        public static async Task<Dictionary<string, object>?> FindByIDAsync(int productId)
            => await CRUD.GetByColumnValueAsync("SP_GetProductByID", "ProductID", productId);
        

        public static async Task<object?> FindByCompanyIDAsync(int companyId)
            =>await CRUD.GetByColumnValueAsync("SP_GetProductByCompanyID", "CompanyID", companyId);

       

        public static async Task<object?> FindByTypeIdAsync(int typeId)
            => await CRUD.GetByColumnValueAsync("SP_GetProductByTypeID", "TypeID", typeId);
        

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
