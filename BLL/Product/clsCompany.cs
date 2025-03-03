using DAL.Product;
using System.Data;

namespace BLL.Product
{
    public class clsCompany : absBaseEntity
    {
        public string CompanyName { set; get; } = "";

        public clsCompany(string companyName)
        {
            CompanyName =companyName;
        }

        internal clsCompany(int companyId, string companyName)
        {
            Id = companyId;
            CompanyName = companyName;
        }

        internal clsCompany() { }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __Update();
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsCompanyData.AddAsync(CompanyName);
            return Id.HasValue;
        } 

        public static async Task<DataTable?> GetAllAsync() => await clsCompanyData.GetAllAsync();

        public static async Task<clsCompany?> GetByIdAsync(int companyId)
        {
            var dict = await clsCompanyData.GetByIdAsync(companyId);
            if (dict == null) return null;
            return FetchCompanyData(dict);
        }
        
        private async Task<bool> __Update() => await clsCompanyData.UpdateAsync(Id, CompanyName);

        internal static clsCompany FetchCompanyData(Dictionary<string, object> dict)
        {
            return new clsCompany(
                (int)dict["CompanyID"],
                (string)dict["CompanyName"]
            );
        }
    }
}
