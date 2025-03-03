using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsWarrintyReturnesToPeopleData
    {
        public static async Task<int?> AddAsync(int? returndStocksId, int? personId, int? userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@returndStocksID", returndStocksId),
                new SqlParameter("@Status", personId),
                new SqlParameter("@userID", userId),
            };
            return await CRUD.AddAsync("SP_Add", parameters);
        }

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int warrantyReturnedId) => await CRUD.GetByColumnValueAsync("SP_GetByID", "WarrantyReturnedID", warrantyReturnedId);

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAll");

        public static async Task<Dictionary<string, object>?> GetByPersonIdAsync(int personid) => await CRUD.GetByColumnValueAsync("SP_GetByPersonID", "PersonID", personid);

        public static async Task<DataTable?> GetAllStockByProductIdAsync(int ProductId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProductID", ProductId)
            };
            return await CRUD.GetAllAsDataTableAsync("SP_GetStockByProductID", parameters);
        }

        public static async Task<bool> UpdateAsync(int? returndStocksId, int? personId, int? userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@returndStocksID", returndStocksId),
                new SqlParameter("@personID", personId),
                new SqlParameter("@userID", userId),
            };
            return await CRUD.UpdateAsync("SP_Update", parameters);
        }

        public static async Task<bool> DeleteAsync(int warrantyReturnedId) => await CRUD.DeleteAsync("SP_Delete", "WarrantyReturnedID", warrantyReturnedId);
    }
}

/*

-- إضافة إرجاع ضمان لشخص
CREATE PROCEDURE sp_AddWarrintyReturnesToPeople
    @ReturnedStockId INT,
    @PersonId INT,
    @UserId INT
AS
BEGIN
    INSERT INTO WarrintyReturnesToPeople (ReturnedStockId, PersonId, UserId)
    VALUES (@ReturnedStockId, @PersonId, @UserId);

    SELECT SCOPE_IDENTITY() AS WarrantyReturnedID;
END;

-- تحديث إرجاع ضمان لشخص
CREATE PROCEDURE sp_UpdateWarrintyReturnesToPeople
    @WarrantyReturnedId INT,
    @PersonId INT,
    @UserId INT
AS
BEGIN
    UPDATE WarrintyReturnesToPeople
    SET PersonId = @PersonId,
        UserId = @UserId
    WHERE WarrantyReturnedID = @WarrantyReturnedId;
END;

-- حذف إرجاع ضمان لشخص
CREATE PROCEDURE sp_DeleteWarrintyReturnesToPeople
    @WarrantyReturnedId INT
AS
BEGIN
    DELETE FROM WarrintyReturnesToPeople
    WHERE WarrantyReturnedID = @WarrantyReturnedId;
END;

-- جلب إرجاع ضمان عن طريق المعرف
CREATE PROCEDURE sp_GetWarrintyReturnesToPeopleById
    @WarrantyReturnedId INT
AS
BEGIN
    SELECT wr.WarrantyReturnedID, wr.ReturnedStockId, wr.PersonId, wr.UserId,
           rs.StockID, rs.Quantity, rs.Reason,
           p.PersonID, p.PersonName,
           u.UserID, u.UserName
    FROM WarrintyReturnesToPeople wr
    JOIN ReturnedStocks rs ON wr.ReturnedStockId = rs.StockID
    JOIN Persons p ON wr.PersonId = p.PersonID
    JOIN Users u ON wr.UserId = u.UserID
    WHERE wr.WarrantyReturnedID = @WarrantyReturnedId;
END;

-- جلب جميع إرجاعات الضمان
CREATE PROCEDURE sp_GetAllWarrintyReturnesToPeople
AS
BEGIN
    SELECT wr.WarrantyReturnedID, wr.ReturnedStockId, wr.PersonId, wr.UserId,
           rs.StockID, rs.Quantity, rs.Reason,
           p.PersonID, p.PersonName,
           u.UserID, u.UserName
    FROM WarrintyReturnesToPeople wr
    JOIN ReturnedStocks rs ON wr.ReturnedStockId = rs.StockID
    JOIN Persons p ON wr.PersonId = p.PersonID
    JOIN Users u ON wr.UserId = u.UserID;
END;

 */