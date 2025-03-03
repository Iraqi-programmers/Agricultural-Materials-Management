using MyLib_DotNet.DatabaseExecutor;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DAL.Product
{
    public static class clsProductTypeData
    {
        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllProductTypes");

        public static async Task<bool> DeleteAsync(int typeId) => await CRUD.DeleteAsync("SP_DeleteType", "TypeID", typeId);

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int typeId) => await CRUD.GetByColumnValueAsync("SP_GetTypeByID", "TypeID", typeId);
        
        public static async Task<object?> FindByNameAsync(string typeName) => await CRUD.GetByColumnValueAsync("SP_GetTypeByName", "TypeName", typeName);
        
        public static async Task<int?> AddAsync(string TypeName)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@TypeName", TypeName)
            };
            return await CRUD.AddAsync("SP_AddProductType", sqlParameter);
        }

        public static async Task<bool> UpdateAsync(int? TypeID, string TypeName)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TypeID", TypeID),
                new SqlParameter("@TypeName", TypeName),
            };
            return await CRUD.UpdateAsync("SP_UpdateType", parameters);
        }
    }
}

/*

-- إضافة إجراء مخزن لإضافة نوع منتج
CREATE PROCEDURE AddProductType
    @TypeName NVARCHAR(255)
AS
BEGIN
    INSERT INTO ProductTypes (TypeName)
    VALUES (@TypeName);
    SELECT SCOPE_IDENTITY();
END;

-- إجراء مخزن لاسترجاع جميع أنواع المنتجات
CREATE PROCEDURE GetAllProductTypes
AS
BEGIN
    SELECT * FROM ProductTypes;
END;

-- إجراء مخزن لاسترجاع نوع المنتج حسب المعرف
CREATE PROCEDURE FindProductTypeByID
    @TypeID INT
AS
BEGIN
    SELECT * FROM ProductTypes WHERE TypeID = @TypeID;
END;

-- إجراء مخزن لتحديث بيانات نوع المنتج
CREATE PROCEDURE UpdateProductType
    @TypeID INT,
    @TypeName NVARCHAR(255)
AS
BEGIN
    UPDATE ProductTypes
    SET TypeName = @TypeName
    WHERE TypeID = @TypeID;
END;

 */
