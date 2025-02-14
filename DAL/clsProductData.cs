using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace DAL
{
    public static class clsProductData
    {

        public static async Task<DataTable?> GetAll()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllProducts", type: CommandType.StoredProcedure);
            
        }

        public static async Task< bool> Delete(int productID)
        {

            return await CRUD.DeleteAsync("SP_DeleteProduct", "ProductID", productID, CommandType.StoredProcedure);

        }

        public static async Task <object?> FindByID(int productID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetProductByID", "ProductID", productID, CommandType.StoredProcedure);
        }

        public static async Task<object?> FindByCompanyID(int CompanyID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetProductByCompanyID", "CompanyID", CompanyID, CommandType.StoredProcedure);

        }

        public static async Task<object?> FindByTypeID(int TypeID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetProductByTypeID", "TypeID", TypeID, CommandType.StoredProcedure);
        }

        public static async Task<int?> AddProductWithDetails(int productID, float Size, float Thickness)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Size", Size),
                new SqlParameter("@Thickness", Thickness)
            };
            return await CRUD.AddAsync("SP_AddProductWithDetails", parameters, CommandType.StoredProcedure);
        }

        public static async Task<int?> AddProduct(int ProductID, int TypeID, int CompanyID, int DetailsID)
        {
            SqlParameter[] parameters =
{
                new SqlParameter("@Type", TypeID),
                new SqlParameter("@CompanyID", CompanyID)
            };
            return await CRUD.AddAsync("SP_AddProductWithDetails", parameters, CommandType.StoredProcedure);

        }

        public static async Task<int?> AddProductWithAllDetails(string TypeName, string CompanyName, float Size, float Thickness)
        {
            SqlParameter[] parameters =
{
                new SqlParameter("@@TypeName", TypeName),
                new SqlParameter("@CompanyName", CompanyName),
                new SqlParameter("@Thickness", Thickness),
                new SqlParameter("@Size", Size)

            };
            return await CRUD.AddAsync("SP_AddProductWithAllDetails", parameters, CommandType.StoredProcedure);

        }

        public static async Task<bool?> Update(int productID, int TypeID, int CompanyID, int DetailsID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TypeID", TypeID),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@DetailsID", DetailsID)
            };
            return await CRUD.UpdateAsync("SP_UpdateProduct", parameters, CommandType.StoredProcedure);
        }

    }
}
