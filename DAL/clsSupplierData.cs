using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsSupplierData
    {
        public static async Task<int?> AddAsync(string supplierName, string phone, bool isPerson, string address)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SupplierName", supplierName),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@IsPerson", isPerson),
                new SqlParameter("@Address", address)
            };
            return await CRUD.AddAsync("SP_AddSupplier", parameters);
        }

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int supplierId) => await CRUD.GetByColumnValueAsync("SP_GetSupplierByID", "SupplierID", supplierId);
        public static async Task<Dictionary<string, object>?> GetByNameAsync(string supplierName) => await CRUD.GetByColumnValueAsync("SP_GetSupplierBySupplierName", "@SupplierName", supplierName);
        public static async Task<Dictionary<string, object>?> GetByPhoneAsync(string phone) => await CRUD.GetByColumnValueAsync("SP_GetSupplierByPhone", "@Phone", phone);

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllSuppliers");

        public static async Task<bool> UpdateAsync(int? supplierId, string supplierName, string phone, bool isPerson,  string address)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SupplierID", supplierId),
                new SqlParameter("@SupplierName", supplierName),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@IsPerson", isPerson),
                new SqlParameter("@Address", address)
            };
            return await CRUD.UpdateAsync("SP_UpdateSupplier", parameters);
        }

        public static async Task<bool> DeleteAsync(int? supplierId)
        {
            if (supplierId == null) return false;
            return await CRUD.DeleteAsync("SP_DeleteSupplier", "@SupplierID", supplierId);
        }
    }
}
/*

-- إنشاء جدول الموردين إذا لم يكن موجودًا
CREATE TABLE Suppliers (
    SupplierID INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NOT NULL UNIQUE,
    IsPerson BIT NOT NULL,
    Address NVARCHAR(255) NULL
);

-- إضافة مورد جديد
CREATE PROCEDURE sp_AddSupplier
    @SupplierName NVARCHAR(100),
    @Phone NVARCHAR(20),
    @IsPerson BIT,
    @Address NVARCHAR(255)
AS
BEGIN
    INSERT INTO Suppliers (SupplierName, Phone, IsPerson, Address)
    VALUES (@SupplierName, @Phone, @IsPerson, @Address);
    SELECT SCOPE_IDENTITY() AS SupplierID;
END;

-- تحديث بيانات مورد
CREATE PROCEDURE sp_UpdateSupplier
    @SupplierId INT,
    @SupplierName NVARCHAR(100),
    @Phone NVARCHAR(20),
    @IsPerson BIT,
    @Address NVARCHAR(255)
AS
BEGIN
    UPDATE Suppliers
    SET SupplierName = @SupplierName, 
        Phone = @Phone, 
        IsPerson = @IsPerson, 
        Address = @Address
    WHERE SupplierID = @SupplierId;
END;

-- حذف مورد
CREATE PROCEDURE sp_DeleteSupplier
    @SupplierId INT
AS
BEGIN
    DELETE FROM Suppliers WHERE SupplierID = @SupplierId;
END;

-- جلب بيانات مورد حسب المعرف
CREATE PROCEDURE sp_GetSupplierById
    @SupplierId INT
AS
BEGIN
    SELECT * FROM Suppliers WHERE SupplierID = @SupplierId;
END;

-- جلب بيانات مورد حسب الاسم
CREATE PROCEDURE sp_GetSupplierByName
    @SupplierName NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Suppliers WHERE SupplierName = @SupplierName;
END;

-- جلب بيانات مورد حسب رقم الهاتف
CREATE PROCEDURE sp_GetSupplierByPhone
    @Phone NVARCHAR(20)
AS
BEGIN
    SELECT * FROM Suppliers WHERE Phone = @Phone;
END;

-- جلب جميع الموردين
CREATE PROCEDURE sp_GetAllSuppliers
AS
BEGIN
    SELECT * FROM Suppliers;
END;

 */