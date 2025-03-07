using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsPurchasesDetailsData
    {
        public static async Task<int?> AddAsync(int purchaseId, int productId, decimal price, string status, int quantity, DateTime warrantyDate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PurchaseID", purchaseId),
                new SqlParameter("@ProductID", productId),
                new SqlParameter("@Price", price),
                new SqlParameter("@Status", status),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@WarrantyDate", warrantyDate)
                
            };
            return await CRUD.AddAsync("SP_AddPurchaseDetail", parameters);
        }

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int purchaseId)
            => await CRUD.GetByColumnValueAsync("SP_GetPurchaseDetailByID", "PurchaseID", purchaseId);

        public static async Task<List<Dictionary<string, object>>?> GetByProductNameAsync(string productName)
            => await CRUD.GetAllAsListAsync("SP_", new SqlParameter[] { new SqlParameter("@ProductName", productName) });

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllPurchaseDetails");

        public static async Task<bool> UpdateAsync(int purchaseDetailId, decimal price, string status, int quantity, DateTime warrantyDate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PurchaseDetailID", purchaseDetailId),
                new SqlParameter("@Price", price),
                new SqlParameter("@Status", status),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@WarrantyDate", warrantyDate)
            };
            return await CRUD.UpdateAsync("SP_UpdatePurchaseDetail", parameters);
        }

        public static async Task<bool> DeleteAsync(int purchaseId) => await CRUD.DeleteAsync("SP_", "@PurchaseID", purchaseId);
    }
}

/*

-- إنشاء جدول تفاصيل المشتريات إذا لم يكن موجودًا
CREATE TABLE PurchaseDetails (
    PurchaseID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    Quantity INT NOT NULL,
    WarrantyDate DATETIME NOT NULL,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- إضافة تفاصيل شراء جديدة
CREATE PROCEDURE sp_AddPurchaseDetail
    @ProductID INT,
    @Price DECIMAL(18,2),
    @Status NVARCHAR(50),
    @Quantity INT,
    @WarrantyDate DATETIME
AS
BEGIN
    INSERT INTO PurchaseDetails (ProductID, Price, Status, Quantity, WarrantyDate)
    VALUES (@ProductID, @Price, @Status, @Quantity, @WarrantyDate);
    SELECT SCOPE_IDENTITY() AS PurchaseID;
END;

-- تحديث تفاصيل شراء
CREATE PROCEDURE sp_UpdatePurchaseDetail
    @PurchaseID INT,
    @ProductID INT,
    @Price DECIMAL(18,2),
    @Status NVARCHAR(50),
    @Quantity INT,
    @WarrantyDate DATETIME
AS
BEGIN
    UPDATE PurchaseDetails
    SET ProductID = @ProductID,
        Price = @Price,
        Status = @Status,
        Quantity = @Quantity,
        WarrantyDate = @WarrantyDate
    WHERE PurchaseID = @PurchaseID;
END;

-- حذف تفاصيل شراء
CREATE PROCEDURE sp_DeletePurchaseDetail
    @PurchaseID INT
AS
BEGIN
    DELETE FROM PurchaseDetails WHERE PurchaseID = @PurchaseID;
END;

-- جلب تفاصيل شراء حسب المعرف
CREATE PROCEDURE sp_GetPurchaseDetailById
    @PurchaseID INT
AS
BEGIN
    SELECT * FROM PurchaseDetails WHERE PurchaseID = @PurchaseID;
END;

-- جلب جميع تفاصيل المشتريات
CREATE PROCEDURE sp_GetAllPurchaseDetails
AS
BEGIN
    SELECT * FROM PurchaseDetails;
END;

 */