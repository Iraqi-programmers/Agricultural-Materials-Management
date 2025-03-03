using System.Data;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsSalesDetailsData
    {
        public static async Task<Dictionary<string, object>?> GetByIdAsync(int detailId) => await CRUD.GetByColumnValueAsync("SP_", "SalesDetailID", detailId);

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_");
    }
}
/*

-- إنشاء جدول تفاصيل المبيعات إذا لم يكن موجودًا
CREATE TABLE SalesDetails (
    SalesDetailID INT IDENTITY(1,1) PRIMARY KEY,
    SaleID INT NOT NULL,
    StockID INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Quantity INT NOT NULL,
    TotalCost DECIMAL(18,2) NOT NULL,
    WarrantyDate DATETIME NULL,
    FOREIGN KEY (SaleID) REFERENCES Sales(SaleID),
    FOREIGN KEY (StockID) REFERENCES Stocks(StockID)
);

-- إضافة تفاصيل بيع جديدة
CREATE PROCEDURE sp_AddSalesDetail
    @SaleID INT,
    @StockID INT,
    @Price DECIMAL(18,2),
    @Quantity INT,
    @TotalCost DECIMAL(18,2),
    @WarrantyDate DATETIME NULL
AS
BEGIN
    INSERT INTO SalesDetails (SaleID, StockID, Price, Quantity, TotalCost, WarrantyDate)
    VALUES (@SaleID, @StockID, @Price, @Quantity, @TotalCost, @WarrantyDate);
    SELECT SCOPE_IDENTITY() AS SalesDetailID;
END;

-- تحديث تفاصيل بيع
CREATE PROCEDURE sp_UpdateSalesDetail
    @SalesDetailID INT,
    @SaleID INT,
    @StockID INT,
    @Price DECIMAL(18,2),
    @Quantity INT,
    @TotalCost DECIMAL(18,2),
    @WarrantyDate DATETIME NULL
AS
BEGIN
    UPDATE SalesDetails
    SET SaleID = @SaleID,
        StockID = @StockID,
        Price = @Price,
        Quantity = @Quantity,
        TotalCost = @TotalCost,
        WarrantyDate = @WarrantyDate
    WHERE SalesDetailID = @SalesDetailID;
END;

-- حذف تفاصيل بيع
CREATE PROCEDURE sp_DeleteSalesDetail
    @SalesDetailID INT
AS
BEGIN
    DELETE FROM SalesDetails WHERE SalesDetailID = @SalesDetailID;
END;

-- جلب تفاصيل بيع حسب المعرف
CREATE PROCEDURE sp_GetSalesDetailById
    @SalesDetailID INT
AS
BEGIN
    SELECT * FROM SalesDetails WHERE SalesDetailID = @SalesDetailID;
END;

-- جلب جميع تفاصيل المبيعات
CREATE PROCEDURE sp_GetAllSalesDetails
AS
BEGIN
    SELECT * FROM SalesDetails;
END;

 */