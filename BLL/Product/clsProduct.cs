using System.Data;
using DAL.Product;

namespace BLL.Product
{
    public class clsProduct : absClassesHelperBasc
    {
        public clsCompany Company { get; set; } 
        public clsProductType ProductType { get; set; }
        public clsSize? Size { get; set; }
        public clsThickness? Thickness { get; set; } 
        public clsWarrinty? Warrinty { get; set; } 

        public clsProduct(clsCompany company, clsProductType productType, clsSize? size, clsThickness? thickness, clsWarrinty? warrinty)
        {
            Company = company;
            ProductType = productType;
            Size = size;
            Thickness = thickness;
            Warrinty = warrinty;
        }

        internal clsProduct(int productId, clsCompany company, clsProductType productType, clsSize? size, clsThickness? thickness, clsWarrinty? warrinty)
        {
            Id = productId;
            Company = company;
            ProductType = productType;
            Size = size;
            Thickness = thickness;
            Warrinty = warrinty;
        }

        internal clsProduct() { }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        public static async Task<int?> AddWithAllProductDataAsync(string productName, string companyName, double size, double thickness, int warrinty)
            => await clsProductData.AddProductWithAllDetailsAsync(productName, companyName, size, thickness, warrinty);

        private async Task<bool> __AddAsync()
        {
            Id = await clsProductData.AddAsync(ProductType.Id, Company.Id, Size?.Id, Thickness?.Id);
            return Id.HasValue;
        }

        public async static Task<clsProduct?> GetByIdAsync(int productId)
        {
            var dict = await clsProductData.FindByIDAsync(productId);
            if (dict == null) return null;
            return FetchProductData(ref dict);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsProductData.GetAllAsync();

        private async Task<bool> __UpdateAsync() => await clsProductData.UpdateAsync(Id, ProductType.Id, Company.Id, Size?.Id, Thickness?.Id, Warrinty?.Id);

        public static async Task<bool> UpdateAllProductData(int productId, string companyName, string typeName, double size, double thickness, int warrinty)
            => await clsProductData.UpdateAllProductDataAsync(productId, typeName, companyName, size, thickness, warrinty);

        public async Task<bool> DeleteAsync(int id) => await clsProductData.DeleteAsync(id);   
        
        internal static clsProduct FetchProductData(ref Dictionary<string, object> dict)
        {
            return new clsProduct(
                (int)dict["ProductID"],
                new clsCompany((int)dict["CompanyID"], (string)dict["CompanyName"]),
                new clsProductType((int)dict["TypeID"], (string)dict["TypeName"]),
                dict.ContainsValue("SizeID") ? new clsSize((int)dict["SizeID"], (double)dict["Size"]) : null,
                dict.ContainsValue("ThicknessID") ? new clsThickness((int)dict["ThicknessID"], (double)dict["Thickness"]) : null,
                dict.ContainsValue("WarrintyID") ? new clsWarrinty((int)dict["WarrintyID"], (int)dict["Period"]) : null
            );
        }
    }
}
