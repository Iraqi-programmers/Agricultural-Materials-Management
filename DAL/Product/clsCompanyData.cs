using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL.Product
{
    public static class clsCompanyData
    {
        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllCompanies");

        public static async Task<bool> DeleteAsync(int companyId) => await CRUD.DeleteAsync("SP_DeleteCompany", "CompanyID", companyId);

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int companyId) => await CRUD.GetByColumnValueAsync("SP_", "PersonID", companyId);

        public static async Task<object?> GetByNameAsync(string companyName) => await CRUD.GetByColumnValueAsync("SP_GetCompanyByName", "CompanyName", companyName);
       
        public static async Task<bool> IsCompanyExistAsync(string companyName) => await CRUD.IsExistAsync("Sp_", "CompanyName", companyName);
       
        public static async Task<int?> AddAsync(string companyName)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@CompanyName", companyName)
            };
            return await CRUD.AddAsync("SP_AddCompany", parameter);
        }

        public static async Task<bool> UpdateAsync(int? companyId, string companyName)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CompanyID", companyId),
                new SqlParameter("@CompanyName", companyName),
            };
            return await CRUD.UpdateAsync("SP_UpdateCompany", parameters);
        }
    }
}
/*

-- إضافة إجراء مخزن لإضافة شركة
CREATE PROCEDURE AddCompany
    @CompanyName NVARCHAR(255)
AS
BEGIN
    INSERT INTO Companies (CompanyName)
    VALUES (@CompanyName);
    SELECT SCOPE_IDENTITY();
END;

-- إجراء مخزن لاسترجاع جميع الشركات
CREATE PROCEDURE GetAllCompanies
AS
BEGIN
    SELECT * FROM Companies;
END;

-- إجراء مخزن لاسترجاع شركة حسب المعرف
CREATE PROCEDURE FindCompanyByID
    @CompanyID INT
AS
BEGIN
    SELECT * FROM Companies WHERE CompanyID = @CompanyID;
END;

-- إجراء مخزن لتحديث بيانات شركة
CREATE PROCEDURE UpdateCompany
    @CompanyID INT,
    @CompanyName NVARCHAR(255)
AS
BEGIN
    UPDATE Companies
    SET CompanyName = @CompanyName
    WHERE CompanyID = @CompanyID;
END;

 */