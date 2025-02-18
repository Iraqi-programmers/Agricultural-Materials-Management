using DAL.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Product
{
    public class clsCompany : absClassesHelper
    {
        string CompanyName { set; get; }

        public clsCompany(string CompanyName)
        {
            _mode = enMode.AddNew;
            Id = null;
            this.CompanyName =CompanyName;
        }

        private clsCompany(int companyID, string CompanyName)
        {
            Id = companyID;
            this.CompanyName = CompanyName;
            _mode = enMode.Update;
        }

        public static async Task<DataTable?> GetAllAsDataTableAsync()
        {
            return await clsCompanyData.GetAllAsDatatableAsync();
        }

        public static async Task<clsCompany?> FindCompanyByIDAsync(int CompanyID)
        {
            var data = await clsCompanyData.FindByIDAsync(CompanyID);

            return new clsCompany(CompanyID, data?[1] as string) ?? null;
        }

        public async Task<bool> SaveAsync()
        {
            if (_mode == enMode.AddNew)
            {
                var newId = await clsCompanyData.AddAsync(CompanyName);
                if (newId.HasValue)
                {
                    Id = newId.Value;
                    _mode = enMode.Update;
                    return true;
                }
                else
                    return false;
            }
            else
                return await clsCompanyData.UpdateAsync(this.Id, this.CompanyName);
        }


    }
}
