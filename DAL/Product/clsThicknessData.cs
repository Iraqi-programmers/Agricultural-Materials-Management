using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System.Data;
//yousif
namespace DAL.Product
{
    public static class clsThicknessData
    {
        public static async Task<DataTable?> GetAllAsDataTableAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllThickness");

        public static async Task<bool> DeleteAsync(int thicknessId) => await CRUD.DeleteAsync("SP_DeleteThickness", "ThicknessID", thicknessId);
                
        public static async Task<Dictionary<string, object>?>FindByIDAsync(double thicknessId) => await CRUD.GetByColumnValueAsync("SP_GetThicknessByID", "ThicknessID", thicknessId);
       
        public static async Task<int?> AddAsync(double Thickness)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@Thickness", Thickness)
            };
            return await CRUD.AddAsync("SP_AddThickness", sqlParameter);
        }

        public static async Task<bool> UpdateAsync(int? thicknessId, double thickness)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ThicknessID", thicknessId),
                new SqlParameter("@Thickness", thickness),
            };
            return await CRUD.UpdateAsync("SP_UpdateThickness", parameters);
        }
    }
}
/*
 
-- إضافة إجراء مخزن لإضافة سماكة
CREATE PROCEDURE AddThickness
    @Thickness FLOAT
AS
BEGIN
    INSERT INTO Thicknesses (Thickness)
    VALUES (@Thickness);
    SELECT SCOPE_IDENTITY();
END;

-- إجراء مخزن لاسترجاع جميع السماكات
CREATE PROCEDURE GetAllThicknesses
AS
BEGIN
    SELECT * FROM Thicknesses;
END;

-- إجراء مخزن لاسترجاع سماكة حسب المعرف
CREATE PROCEDURE FindThicknessByID
    @ThicknessID INT
AS
BEGIN
    SELECT * FROM Thicknesses WHERE ThicknessID = @ThicknessID;
END;

-- إجراء مخزن لتحديث بيانات السماكة
CREATE PROCEDURE UpdateThickness
    @ThicknessID INT,
    @Thickness FLOAT
AS
BEGIN
    UPDATE Thicknesses
    SET Thickness = @Thickness
    WHERE ThicknessID = @ThicknessID;
END;

 */
