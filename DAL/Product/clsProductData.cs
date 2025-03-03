using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL.Product
{
    public static class clsProductData
    {
        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllProducts");
       
        public static async Task<bool> DeleteAsync(int productId) => await CRUD.DeleteAsync("SP_DeleteProduct", "ProductID", productId);
       
        public static async Task<Dictionary<string, object>?> GetByIdAsync(int productId) => await CRUD.GetByColumnValueAsync("SP_GetProductByID", "ProductID", productId);
        
        public static async Task<object?> GetByCompanyIdAsync(int companyId) => await CRUD.GetByColumnValueAsync("SP_GetProductByCompanyID", "CompanyID", companyId);

        public static async Task<object?> FindByTypeIdAsync(int typeId) => await CRUD.GetByColumnValueAsync("SP_GetProductByTypeID", "TypeID", typeId);
        
        public static async Task<int?> AddAsync(string typeName, string companyName, double size, double thickness, int warrinty)
        {
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

        public static async Task<bool> UpdateAsync(int productId, string typeName, string companyName, double size, double thickness, int warrinty)
        {
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

        public static async Task<bool> UpdateAsync(int? productId, int? productTypeId, int? companyId, int? sizeId, int? thicknessId, int? warrintyId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProductID", productId),
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
/*

-- إضافة إجراء مخزن لإضافة منتج
CREATE PROCEDURE AddProduct
    @CompanyID INT,
    @TypeID INT,
    @SizeID INT = NULL,
    @ThicknessID INT = NULL,
    @WarrintyID INT = NULL
AS
BEGIN
    INSERT INTO Products (CompanyID, TypeID, SizeID, ThicknessID, WarrintyID)
    VALUES (@CompanyID, @TypeID, @SizeID, @ThicknessID, @WarrintyID);
    SELECT SCOPE_IDENTITY();
END;

-- إجراء مخزن لاسترجاع جميع المنتجات
CREATE PROCEDURE GetAllProducts
AS
BEGIN
    SELECT * FROM Products;
END;

-- إجراء مخزن لاسترجاع منتج حسب المعرف
CREATE PROCEDURE FindProductByID
    @ProductID INT
AS
BEGIN
    SELECT * FROM Products WHERE ProductID = @ProductID;
END;

-- إجراء مخزن لتحديث بيانات المنتج
CREATE PROCEDURE UpdateProduct
    @ProductID INT,
    @CompanyID INT,
    @TypeID INT,
    @SizeID INT = NULL,
    @ThicknessID INT = NULL,
    @WarrintyID INT = NULL
AS
BEGIN
    UPDATE Products
    SET CompanyID = @CompanyID, TypeID = @TypeID, SizeID = @SizeID, ThicknessID = @ThicknessID, WarrintyID = @WarrintyID
    WHERE ProductID = @ProductID;
END;

 */