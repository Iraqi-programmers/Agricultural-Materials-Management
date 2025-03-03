using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsSupplierPymentsData
    {
        public static async Task<int?> AddAsync(double amount, int? supplierId, DateTime pymentDate, int purchaseId, int? userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Amount", amount),
                new SqlParameter("@SupplierID", supplierId),
                new SqlParameter("@PaymentDate", pymentDate),
                new SqlParameter("@PurchaseID", purchaseId),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.AddAsync("SP_AddSupplierPayment", parameters);
        }

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int supplierId)
            => await CRUD.GetByColumnValueAsync("SP_GetSupplierPaymentsBySupplier", "@SupplierID", supplierId);

        public static async Task<bool> UpdateAsync(int? supplierPaymentId, double amount, DateTime pymentDate, int purchaseId, int? userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SupplierPaymentID", supplierPaymentId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@PaymentDate", pymentDate),
                new SqlParameter("@PurchaseID",purchaseId),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.UpdateAsync("SP_UpdateSupplierPayment", parameters);
        }

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllSupplierPayments");

        public static async Task<bool> DeleteAsync(int supplierPaymentId) => await CRUD.DeleteAsync("SP_DeleteSupplierPayment", "@SupplierPaymentID", supplierPaymentId);
    }
}
/*
 
-- إضافة الدفع
CREATE PROCEDURE sp_AddSupplierPayment
    @Amount DECIMAL(18, 2),
    @SupplierID INT,
    @PymentDate DATETIME,
    @PurchaseID INT,
    @UserID INT
AS
BEGIN
    DECLARE @SupplierPymentID INT;

    INSERT INTO SupplierPyments (Amount, SupplierID, PymentDate, PurchaseID, UserID)
    VALUES (@Amount, @SupplierID, @PymentDate, @PurchaseID, @UserID);

    SET @SupplierPymentID = SCOPE_IDENTITY();

    UPDATE Purchases
    SET TotalPaid = ISNULL(TotalPaid, 0) + @Amount
    WHERE PurchaseID = @PurchaseID;

    SELECT @SupplierPymentID AS SupplierPymentID;
END;

-- تحديث الدفع
CREATE PROCEDURE sp_UpdateSupplierPayment
    @SupplierPymentID INT,
    @Amount DECIMAL(18, 2),
    @PymentDate DATETIME,
    @PurchaseID INT,
    @UserID INT
AS
BEGIN
    DECLARE @OldAmount DECIMAL(18, 2);

    SELECT @OldAmount = Amount FROM SupplierPyments WHERE SupplierPymentID = @SupplierPymentID;

    UPDATE SupplierPyments
    SET Amount = @Amount,
        PymentDate = @PymentDate,
        PurchaseID = @PurchaseID,
        UserID = @UserID
    WHERE SupplierPymentID = @SupplierPymentID;

    UPDATE Purchases
    SET TotalPaid = TotalPaid - @OldAmount + @Amount
    WHERE PurchaseID = @PurchaseID;

    SELECT 'Success' AS Result;
END;

-- حذف الدفع
CREATE PROCEDURE sp_DeleteSupplierPayment
    @SupplierPymentID INT
AS
BEGIN
    DECLARE @Amount DECIMAL(18, 2);
    DECLARE @PurchaseID INT;

    SELECT @Amount = Amount, @PurchaseID = PurchaseID
    FROM SupplierPyments
    WHERE SupplierPymentID = @SupplierPymentID;

    DELETE FROM SupplierPyments WHERE SupplierPymentID = @SupplierPymentID;

    UPDATE Purchases
    SET TotalPaid = ISNULL(TotalPaid, 0) - @Amount
    WHERE PurchaseID = @PurchaseID;

    SELECT 'Deleted' AS Result;
END;

-- جلب الدفع عن طريق المعرف
CREATE PROCEDURE sp_GetSupplierPaymentById
    @SupplierPymentID INT
AS
BEGIN
    SELECT sp.SupplierPymentID, sp.Amount, sp.PymentDate, sp.PurchaseID,
           s.SupplierID, s.SupplierName, 
           u.UserID, u.FullName AS UserName
    FROM SupplierPyments sp
    JOIN Suppliers s ON sp.SupplierID = s.SupplierID
    JOIN Users u ON sp.UserID = u.UserID
    WHERE sp.SupplierPymentID = @SupplierPymentID;
END;

-- جلب جميع الدفعات
CREATE PROCEDURE sp_GetAllSupplierPayments
AS
BEGIN
    SELECT sp.SupplierPymentID, sp.Amount, sp.PymentDate, sp.PurchaseID,
           s.SupplierID, s.SupplierName, 
           u.UserID, u.FullName AS UserName
    FROM SupplierPyments sp
    JOIN Suppliers s ON sp.SupplierID = s.SupplierID
    JOIN Users u ON sp.UserID = u.UserID
    ORDER BY sp.PymentDate DESC;
END;

 */