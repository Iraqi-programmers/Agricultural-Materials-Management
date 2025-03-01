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
        public static async Task<DataTable?> GetAllAsync()
         => await CRUD.GetAllAsDataTableAsync("SP_GetAllCompanies");

        
        public static async Task<bool> DeleteAsync(int CompanyID)
        {

            return await CRUD.DeleteAsync("SP_DeleteCompany", "CompanyID", CompanyID);

        }

        public static async Task<Dictionary<string, object>?> FindByIDAsync(int companyId)
            => await CRUD.GetByColumnValueAsync("SP_", "PersonID", companyId);

        public static async Task<object?> FindByNameAsync(string CompanyName)
            => await CRUD.GetByColumnValueAsync("SP_GetCompanyByName", "CompanyName", CompanyName);
       


        public static async Task<bool> IsCompanyExist(string companyName)
            => await CRUD.IsExistAsync("Sp_", "CompanyName", companyName);
       
        //if company exists Will Return The ID of company
        public static async Task<int?> AddAsync(string CompanyName)
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
