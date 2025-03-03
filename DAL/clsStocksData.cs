using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsStocksData
    {
        public static async Task<int?> AddAsync(int? productId, int quantity, string status, double price)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProductID", productId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@Status", status),
                new SqlParameter("@Price", price)
            };
            return await CRUD.AddAsync("SP_AddStock", parameters);
        }

        public static async Task<bool> UpdateAsync(int? stockId, int? productId, int quantity, string status, double price)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@StockID", stockId),
                new SqlParameter("@ProductID", productId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@Status", status),
                new SqlParameter("@Price", price),
            };
            return await CRUD.UpdateAsync("SP_UpdateStock", parameters);
        }

        public static async Task<bool> UpdateAsync(int stockId, int quantity)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@StockID", stockId),
                new SqlParameter("@Quantity", quantity)
            };
            return await CRUD.UpdateAsync("SP_UpdateStockQuantity", parameters);
        }

        public static async Task<bool> DeleteAsync(int stockId) => await CRUD.DeleteAsync("SP_DeleteStock", "StockID", stockId);
        
        public static async Task<Dictionary<string, object>?> GetByStockIdAsync(int stockId) => await CRUD.GetByColumnValueAsync("SP_GetStockByID", "StockID", stockId);
        
        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllStocks");

        public static async Task<Dictionary<string, object>?> GetByProductIdAsync(int productId) => await CRUD.GetByColumnValueAsync("SP_GetStockByProductID", "ProductID", productId);

        public static async Task<DataTable?> GetAllStockByProductIdAsync(int productId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProductID", productId)
            };
            return await CRUD.GetAllAsDataTableAsync("SP_GetStockByProductID", parameters);
        }

        public static async Task<DataTable?> GetAllProductsByStatusAsync(string status)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Status", status)
            };
            return await CRUD.GetAllAsDataTableAsync("SP_GetStockByStatus", parameters);
        }
    }
}

/*
CREATE PROCEDURE AddStock
    @ProductId INT,
    @Quantity INT,
    @Status NVARCHAR(50),
    @Price FLOAT
AS
BEGIN
    INSERT INTO Stocks (ProductId, Quantity, Status, Price)
    VALUES (@ProductId, @Quantity, @Status, @Price);
    SELECT SCOPE_IDENTITY();
END;

CREATE PROCEDURE GetStockById
    @StockId INT
AS
BEGIN
    SELECT s.StockID, s.Quantity, s.Status, s.Price, 
           p.ProductID, p.CompanyID, p.TypeID, p.SizeID, p.ThicknessID, p.WarrintyID,
           c.CompanyName, t.TypeName, sz.Size, th.Thickness, w.Period
    FROM Stocks s
    JOIN Products p ON s.ProductId = p.ProductID
    JOIN Companies c ON p.CompanyID = c.CompanyID
    JOIN ProductTypes t ON p.TypeID = t.TypeID
    LEFT JOIN Sizes sz ON p.SizeID = sz.SizeID
    LEFT JOIN Thicknesses th ON p.ThicknessID = th.ThicknessID
    LEFT JOIN Warranties w ON p.WarrintyID = w.WarrintyID
    WHERE s.StockID = @StockId;
END;

CREATE PROCEDURE GetAllStocks
AS
BEGIN
    SELECT s.StockID, s.Quantity, s.Status, s.Price, 
           p.ProductID, p.CompanyID, p.TypeID, p.SizeID, p.ThicknessID, p.WarrintyID,
           c.CompanyName, t.TypeName, sz.Size, th.Thickness, w.Period
    FROM Stocks s
    JOIN Products p ON s.ProductId = p.ProductID
    JOIN Companies c ON p.CompanyID = c.CompanyID
    JOIN ProductTypes t ON p.TypeID = t.TypeID
    LEFT JOIN Sizes sz ON p.SizeID = sz.SizeID
    LEFT JOIN Thicknesses th ON p.ThicknessID = th.ThicknessID
    LEFT JOIN Warranties w ON p.WarrintyID = w.WarrintyID;
END;

CREATE PROCEDURE UpdateStock
    @StockId INT,
    @ProductId INT,
    @Quantity INT,
    @Status NVARCHAR(50),
    @Price FLOAT
AS
BEGIN
    UPDATE Stocks
    SET ProductId = @ProductId,
        Quantity = @Quantity,
        Status = @Status,
        Price = @Price
    WHERE StockID = @StockId;
END;

CREATE PROCEDURE DeleteStock
    @StockId INT
AS
BEGIN
    DELETE FROM Stocks WHERE StockID = @StockId;
END;

 */
