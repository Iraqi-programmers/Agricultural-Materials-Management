using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Net.NetworkInformation;
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
        

        public static async Task<int?> AddProductWithAllDetailsAsync(string typeName, string companyName, double size, double thickness, int warrinty)
        {

            //زيوني لازم تضيف  ستور بروسيجر تضيف برودكت من خلال البيانات
            SqlParameter[] parameters =
            {
                new SqlParameter("@TypeName", typeName),
                new SqlParameter("@CompanyName", companyName),
                new SqlParameter("@Thickness", thickness),
                new SqlParameter("@Size", size),
                new SqlParameter("@Warrinty", warrinty)

            };
            return await CRUD.AddAsync("SP_AddProductWithAllDetails", parameters);

        }
        public static async Task<int?> AddAsync(int? productTypeId, int? companyId, int? sizeId, int? thicknessId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@productTypeId", productTypeId),
                new SqlParameter("@companyId", companyId),
                new SqlParameter("@ThicknessID", thicknessId),
                new SqlParameter("@SizeID", sizeId)

            };
            return await CRUD.AddAsync("SP_AddProductWithAllDetails", parameters);

        }

        public static async Task<bool> UpdateAllProductDataAsync(int productID, string typeName, string companyName, double size, double thickness, int warrinty)
        {
            //لازم  ستور بروسيجر تعدل عليهن 
            SqlParameter[] parameters =
            {
                new SqlParameter("@TypeName", typeName),
                new SqlParameter("@CompanyName", companyName),
                new SqlParameter("@Size", size),
                new SqlParameter("@Thickness", thickness),
                new SqlParameter("@warrinty", warrinty)
            };
            return await CRUD.UpdateAsync("SP_UpdateProduct", parameters);
        }

        public static async Task<bool> UpdateAsync(int? productID, int? productTypeId, int? companyId, int? sizeId, int? thicknessId, int? warrintyId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProductID", productID),
                new SqlParameter("@TypeID", productTypeId),
                new SqlParameter("@CompanyID", companyId),
                new SqlParameter("@SizeID", sizeId),
                new SqlParameter("@ThicknessID", thicknessId),
                new SqlParameter("@WarrintyID", warrintyId)
            };
            return await CRUD.UpdateAsync("SP_UpdateProduct", parameters);

        }

    }
}
