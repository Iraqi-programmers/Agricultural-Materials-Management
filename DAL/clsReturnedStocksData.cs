using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsReturnedStocksData
    {
        public static async Task<int?> AddNew(int? SaleDetailID, int quantity, int? supplierId, int? userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SaleDetailID", SaleDetailID),
                new SqlParameter("@supplierId", supplierId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@SupplierID", supplierId),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.AddAsync("SP_AddReturnedProduct", parameters);
        }

        public static async Task<bool> Update(int? SaleDetailID, int quantity, int? supplierId, int? userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SaleDetailID", SaleDetailID),
                new SqlParameter("@supplierId", supplierId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@SupplierID", supplierId),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.UpdateAsync("SP_UpdateReturnedProduct", parameters);
        }

        public static async Task<bool> Delete(int returnedProductId) => await CRUD.DeleteAsync("SP_DeleteReturnedProduct", "ReturnedProductID", returnedProductId);

        public static async Task<Dictionary<string,object>?> GetByIdAsync(int returnedProductId) 
            => await CRUD.GetByColumnValueAsync("SP_GetReturnedProductByID", "ReturnedProductID", returnedProductId);
        
        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllReturnedProducts");
    }
}

/*

CREATE PROCEDURE sp_AddReturnedStock
    @SalesDetailsId INT,
    @Quantity INT,
    @SupplierId INT,
    @UserId INT
AS
BEGIN
    INSERT INTO ReturnedStocks (SalesDetailsId, Quantity, SupplierId, UserId)
    VALUES (@SalesDetailsId, @Quantity, @SupplierId, @UserId);
    SELECT SCOPE_IDENTITY() AS ReturnedStockID;
END;

CREATE PROCEDURE sp_UpdateReturnedStock
    @ReturnedStockId INT,
    @SalesDetailsId INT,
    @Quantity INT,
    @SupplierId INT,
    @UserId INT
AS
BEGIN
    UPDATE ReturnedStocks
    SET SalesDetailsId = @SalesDetailsId,
        Quantity = @Quantity,
        SupplierId = @SupplierId,
        UserId = @UserId
    WHERE ReturnedStockID = @ReturnedStockId;
END;

CREATE PROCEDURE sp_DeleteReturnedStock
    @ReturnedStockId INT
AS
BEGIN
    DELETE FROM ReturnedStocks
    WHERE ReturnedStockID = @ReturnedStockId;
END;

CREATE PROCEDURE sp_GetReturnedStockById
    @ReturnedStockId INT
AS
BEGIN
    SELECT rs.ReturnedStockID, rs.Quantity, rs.SalesDetailsId, rs.SupplierId, rs.UserId,
           sd.DetailID, sd.Price, sd.Quantity AS SaleQuantity, 
           s.SupplierID, s.SupplierName, u.UserID, u.UserName
    FROM ReturnedStocks rs
    JOIN SalesDetails sd ON rs.SalesDetailsId = sd.DetailID
    JOIN Suppliers s ON rs.SupplierId = s.SupplierID
    JOIN Users u ON rs.UserId = u.UserID
    WHERE rs.ReturnedStockID = @ReturnedStockId;
END;

CREATE PROCEDURE sp_GetAllReturnedStocks
AS
BEGIN
    SELECT rs.ReturnedStockID, rs.Quantity, rs.SalesDetailsId, rs.SupplierId, rs.UserId,
           sd.DetailID, sd.Price, sd.Quantity AS SaleQuantity, 
           s.SupplierID, s.SupplierName, u.UserID, u.UserName
    FROM ReturnedStocks rs
    JOIN SalesDetails sd ON rs.SalesDetailsId = sd.DetailID
    JOIN Suppliers s ON rs.SupplierId = s.SupplierID
    JOIN Users u ON rs.UserId = u.UserID;
END;
 
 */