using DAL.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Product
{
    public class clsCompany : absClassesHelperBasc
    {
        string CompanyName { set; get; }

        public clsCompany(string CompanyName)
        {
            Id = null;
            this.CompanyName =CompanyName;
        }

        private clsCompany(int companyID, string CompanyName)
        {
            Id = companyID;
            this.CompanyName = CompanyName;
        }

        public static async Task<DataTable?> getAllAsDataTableAsync()
        {
            return await clsCompanyData.getAllAsDatatableAsync();
        }

        public static async Task<clsCompany?> findCompanyByIDAsync(int CompanyID)
        {
            var data = await clsCompanyData.findByIDAsync(CompanyID);

            return new clsCompany(CompanyID, data?[1] as string) ?? null;
        }
        private async Task<int?> __addAsync()
        {
            return await clsCompanyData.addAsync(CompanyName);
        }
        private async Task<bool> __update()
        {
            return await clsCompanyData.updateAsync(Id, CompanyName);
        }
        public async Task<bool> SaveAsync()
        {
            if(Id == null)
            {
                Id = await __addAsync();
                return Id != null;
            }
            return await __update();
        }


    }
}
