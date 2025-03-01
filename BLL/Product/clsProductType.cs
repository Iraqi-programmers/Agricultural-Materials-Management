using DAL.Product;
using System.Data;

//Yousif 
namespace BLL.Product
{
    public class clsProductType :absClassesHelperBasc
    {
        public string TypeName { set; get; }
        public clsProductType(string typeName)
        {
            Id = null;
            this.TypeName = typeName;
        }
        internal clsProductType(int typeId,  string typeName)
        {
            this.Id = typeId;
            this.TypeName = typeName;
        }
        public static async Task<DataTable?> GetAllAsync()
            => await clsProductTypeData.GetAllAsync();
        
        public static async Task<clsProductType?> FindTypeAsync(int typeId)
        {
            var data = await clsProductTypeData.FindByIdAsync(typeId);
            if (data == null)
                return null;
            return new clsProductType(typeId, (string)data["TypeName"]);
        }
        private async Task<bool> __AddAsync()
        {
            Id = await clsProductTypeData.AddAsync(TypeName);
            return Id.HasValue;
        }
        private async Task<bool> __UpdateAsync()
            =>await clsProductTypeData.UpdateAsync(Id, TypeName);
        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
                
        }
    }
}
