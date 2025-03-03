using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System.Data;

namespace DAL.Product
{
    public static class clsSizeData
    {
        public static async Task<DataTable?> GetAllAsDatatableAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllSizes");
        
        public static async Task<bool> DeleteAsync(int sizeID) => await CRUD.DeleteAsync("SP_DeleteType", "SizeID", sizeID);

        public static async Task<Dictionary<string, object>?> FindByIDAsync(int sizeID) => await CRUD.GetByColumnValueAsync("SP_GetSizeByID", "SizeID", sizeID);

        public static async Task<int?> AddAsync(double size)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@Size", size)
            };
            return await CRUD.AddAsync("SP_AddSize", sqlParameter);
        }

        public static async Task<bool> UpdateAsync(int? sizeId, double size)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SizeID", sizeId),
                new SqlParameter("@Size", size),
            };
            return await CRUD.UpdateAsync("SP_UpdateSiz", parameters);
        }
    }
}
/*
 
-- إضافة إجراء مخزن لإضافة حجم
CREATE PROCEDURE AddSize
    @Size FLOAT
AS
BEGIN
    INSERT INTO Sizes (Size)
    VALUES (@Size);
    SELECT SCOPE_IDENTITY();
END;

-- إجراء مخزن لاسترجاع جميع الأحجام
CREATE PROCEDURE GetAllSizes
AS
BEGIN
    SELECT * FROM Sizes;
END;

-- إجراء مخزن لاسترجاع حجم حسب المعرف
CREATE PROCEDURE FindSizeByID
    @SizeID INT
AS
BEGIN
    SELECT * FROM Sizes WHERE SizeID = @SizeID;
END;

-- إجراء مخزن لتحديث بيانات الحجم
CREATE PROCEDURE UpdateSize
    @SizeID INT,
    @Size FLOAT
AS
BEGIN
    UPDATE Sizes
    SET Size = @Size
    WHERE SizeID = @SizeID;
END;

 */