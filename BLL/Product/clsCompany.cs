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
            => await clsCompanyData.GetAllAsync();
        

        public static async Task<clsCompany?> FindByIDAsync(int companyID)
        {
            var data = await clsCompanyData.FindByIDAsync(companyID);
            if (data == null)
                return null;

            string? companyName = (string)data["CompanyName"];
            return new clsCompany(companyID, companyName) ?? null;
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsCompanyData.AddAsync(CompanyName);
            return Id.HasValue;
        }
        private async Task<bool> __UpdateAsync()
            =>await clsCompanyData.UpdateAsync(Id, CompanyName);
       
        public async Task<bool> SaveAsync()
        {
            if(!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }


    }
}
