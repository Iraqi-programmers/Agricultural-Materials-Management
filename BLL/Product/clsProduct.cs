using DAL.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Product
{
    public class clsProduct :absClassesHelperBasc
    {
        public clsCompany Company { get; set; }
        public clsProductType ProductType { get; set; }
        public clsSize? Size { get; set; }
        public clsThickness? Thickness { get; set; }
        public clsWarrinty? Warrinty { get; set; }
        public clsProduct(clsCompany company, clsProductType productType, clsSize? size, clsThickness? thickness, clsWarrinty? warrinty)
        {
            Id = null;
            Company = company;
            ProductType = productType;
            Size = size;
            Thickness = thickness;
            Warrinty = warrinty;

        }
        private clsProduct(int id, clsCompany company, clsProductType productType, clsSize? size, clsThickness? thickness, clsWarrinty? warrinty)
        {
            Id = id;
            Company = company;
            ProductType = productType;
            Size = size;
            Thickness = thickness;
            Warrinty = warrinty;
        }
        public static async Task<DataTable?>GetAll()
            => await clsProductData.GetAllAsync();
        public async static Task< clsProduct?> GetByIdAsync(int id)
        {
           var dict = await clsProductData.FindByIDAsync(id);
            if (dict == null) 
                return null;
            return await FetchProductDataAsync(dict);
        }

        public static async Task<int?> AddWithAllProductData(string productName, string companyName, double size, double thickness, int warrinty)
            => await clsProductData.AddProductWithAllDetailsAsync(productName, companyName, size, thickness, warrinty);

        private async Task<bool> __AddAsync()
        {
            Id = await clsProductData.AddAsync(ProductType.Id, Company.Id, Size.Id, Thickness.Id);
            return Id.HasValue;
        }

        private async Task<bool> __Update()
            => await clsProductData.UpdateAsync(Id, ProductType.Id, Company.Id, Size.Id, Thickness.Id, Warrinty.Id);
        public static async Task<bool> UpdateAllProductData(int productId, string companyName, string typeName, double size, double thickness, int warrinty)
           => await clsProductData.UpdateAllProductDataAsync(productId, typeName, companyName, size, thickness, warrinty);
        public async Task<bool> Save()
        {
            if(!Id.HasValue)
                return await __AddAsync();
            return await __Update();
        }
        public async Task<bool> Delete(int id)
            => await clsProductData.DeleteAsync(id);    
        internal static async  Task<clsProduct?> FetchProductDataAsync(Dictionary<string, object>? dict)
        {
            if (dict == null) 
                return null;
            
            clsCompany company = await clsCompany.FindByIDAsync((int)dict["CompanyID"]);
            clsProductType productType = await clsProductType.FindTypeAsync((int)dict["ProductTypeID"]);
            clsSize? size  = await clsSize.FindByIdAsync((int)dict["Size"]);
            clsThickness? thickness = await clsThickness.FindByIdAsync((int)dict["ThicknessID"]);
            clsWarrinty? warrinty = await clsWarrinty.GetByIDAsync((int)dict["WarrintyID"]);
            return  new clsProduct((int)dict["ProductID"], company, productType, size, thickness, warrinty);
            
        }
        
    }
}
