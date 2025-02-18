using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Product
{
    public static class clsCompanyData
    {
        public static async Task<DataTable?> GetAllAsDatatableAsync()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllCompanies");

        }
        public static async Task<bool> DeleteAsync(int CompanyID)
        {

            return await CRUD.DeleteAsync("SP_DeleteCompany", "CompanyID", CompanyID);

        }

        public static async Task<object[]?> FindByIDAsync(int CompanyID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetCompanyByID", "TypeID", CompanyID);
        }
        public static async Task<object?> FindCompanyByNameAsync(string CompanyName)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetCompanyByName", "CompanyName", CompanyName);
        }


        //if company exists Will Return The ID of company
        public static async Task<int?> AddAsync(string CompanyName)
        {
            
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@CompanyName", CompanyName)
            };
            return await CRUD.AddAsync("SP_AddCompany", sqlParameter);
        }

        public static async Task<bool> UpdateAsync(int? CompanyID, string CompanyName)
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
