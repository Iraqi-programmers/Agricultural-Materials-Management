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
        public static async Task<DataTable?> GetAll()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllCompanies", type: CommandType.StoredProcedure);

        }
        public static async Task<bool> Delete(int CompanyID)
        {

            return await CRUD.DeleteAsync("SP_DeleteCompany", "CompanyID", CompanyID, CommandType.StoredProcedure);

        }

        public static async Task<object?> FindByID(int CompanyID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetCompanyByID", "TypeID", CompanyID, CommandType.StoredProcedure);
        }
        public static async Task<object?> FindCompanyByName(string CompanyName)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetCompanyByName", "CompanyName", CompanyName, CommandType.StoredProcedure);
        }


        //if company exists Will Return The ID of company
        public static async Task<int?> Add(string CompanyName)
        {
            
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@CompanyName", CompanyName)
            };
            return await CRUD.AddAsync("SP_AddCompany", sqlParameter, CommandType.StoredProcedure);
        }

        public static async Task<bool?> Update(int CompanyID, string CompanyName)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@CompanyName", CompanyName),
            };
            return await CRUD.UpdateAsync("SP_UpdateCompany", parameters, CommandType.StoredProcedure);
        }

    }


}
