using DAL.Product;
using System.Data;

//Yousif 
namespace BLL.Product
{
    public class clsProductType :absClassesHelperBasc
    {
        public string typeName { set; get; }
        public clsProductType(string typeName)
        {
            Id = null;
            this.typeName = typeName;
        }
        private clsProductType(int typeId,  string typeName)
        {
            this.Id = typeId;
            this.typeName = typeName;
        }
        public static async Task<DataTable?> getAllAsDataTableAsync()
        {
            return await clsProductTypeData.GetAllAsync();
        }
        public static async Task<clsProductType?> findTypeAsync(int typeId)
        {
            var data = await clsProductTypeData.FindByIdAsync(typeId);
            if (data == null)
                return null;
            string typeName = (string)data["TypeName"];

            return new clsProductType(typeId, typeName);
        }
        private async Task<bool> __AddAsync()
        {
            Id = await clsProductTypeData.AddAsync(typeName);
            return Id.HasValue;
        }
        private async Task<bool> __UpdateAsync()
            =>await clsProductTypeData.UpdateAsync(Id, typeName);
        public async Task<bool> saveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
                
        }
    }
}
