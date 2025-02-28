using DAL.Product;
using System.Data;


//yousif
namespace BLL.Product
{

    public class clsCompany : absClassesHelperBasc
    {
        public string CompanyName { set; get; }

        public clsCompany(string companyName)
        {
            Id = null;
            this.CompanyName =companyName;
        }

        private clsCompany(int companyID, string companyName)
        {
            Id = companyID;
            this.CompanyName = companyName;
        }

        public static async Task<DataTable?> GetAllAsync()
        {
            return await clsCompanyData.getAllAsDatatableAsync();
        }

        public static async Task<clsCompany?> FindByIDAsync(int companyID)
        {
            var data = await clsCompanyData.FindByIDAsync(companyID);
            

            return new clsCompany(companyID, data?[1] as string) ?? null;
        }

        private async Task<int?> __AddAsync()
        {
            return await clsCompanyData.addAsync(CompanyName);
        }
        private async Task<bool> __Update()
        {
            return await clsCompanyData.updateAsync(Id, CompanyName);
        }
        public async Task<bool> SaveAsync()
        {
            if(Id == null)
            {
                Id = await __AddAsync();
                return Id != null;
            }
            return await __Update();
        }


    }
}
