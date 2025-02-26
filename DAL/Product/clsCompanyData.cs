using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//yousif
namespace DAL.Product
{
    public static class clsCompanyData
    {
        public static async Task<DataTable?> getAllAsDatatableAsync()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllCompanies");

        }
        public static async Task<bool> deleteAsync(int CompanyID)
        {

            return await CRUD.DeleteAsync("SP_DeleteCompany", "CompanyID", CompanyID);

        }

        public static async Task<object[]?> findByIDAsync(int CompanyID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetCompanyByID", "TypeID", CompanyID);
        }
        public static async Task<object?> findCompanyByNameAsync(string CompanyName)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetCompanyByName", "CompanyName", CompanyName);
        }


        public static async Task<bool> isCompanyExist(int companyID)
        {
            return await CRUD.IsExistAsync("Sp_", "CompanyID", companyID);
        }
        //if company exists Will Return The ID of company
        public static async Task<int?> addAsync(string CompanyName)
        {
            
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@CompanyName", CompanyName)
            };
            return await CRUD.AddAsync("SP_AddCompany", sqlParameter);
        }

        public static async Task<bool> updateAsync(int? CompanyID, string CompanyName)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@CompanyName", CompanyName),
            };
            return await CRUD.UpdateAsync("SP_UpdateCompany", parameters);
        }

    }


}
