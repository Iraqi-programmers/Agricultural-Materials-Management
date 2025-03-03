using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System.Data;

namespace DAL
{
    public class clsWarrintyData
    {
        public static async Task<int?> AddAsync(int period)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Period", period)
            };
            return await CRUD.AddAsync("SP_AddWarranty", parameters);
        }

        public static async Task<bool> UpdateAsync(int? warrantyId, int period)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@WarrantyID", warrantyId),
                new SqlParameter("@Period", period)
            };
            return await CRUD.UpdateAsync("SP_UpdateWarranty", parameters);
        }

        public static async Task<bool> DeleteAsync(int warrantyId) => await CRUD.DeleteAsync("SP_DeleteWarranty", "WarrantyID", warrantyId);
       
        public static async Task<Dictionary<string, object>?> GetByIdAsync(int warrantyId) => await CRUD.GetByColumnValueAsync("SP_GetWarrantyByID", "WarrantyID", warrantyId);
        
        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllWarranties");
    }
}

/*
 
-- إضافة إجراء مخزن لإضافة ضمان
CREATE PROCEDURE AddWarrinty
    @Period INT
AS
BEGIN
    INSERT INTO Warranties (Period)
    VALUES (@Period);
    SELECT SCOPE_IDENTITY();
END;

-- إجراء مخزن لاسترجاع جميع الضمانات
CREATE PROCEDURE GetAllWarranties
AS
BEGIN
    SELECT * FROM Warranties;
END;

-- إجراء مخزن لاسترجاع ضمان حسب المعرف
CREATE PROCEDURE FindWarrintyByID
    @WarrintyID INT
AS
BEGIN
    SELECT * FROM Warranties WHERE WarrintyID = @WarrintyID;
END;

-- إجراء مخزن لتحديث بيانات الضمان
CREATE PROCEDURE UpdateWarrinty
    @WarrintyID INT,
    @Period INT
AS
BEGIN
    UPDATE Warranties
    SET Period = @Period
    WHERE WarrintyID = @WarrintyID;
END;

 */
