using DAL.Product;
using System.Data;

namespace BLL.Product
{
    public class clsProductType : absBaseEntity
    {
        public string TypeName { set; get; } = "";

        public clsProductType(string typeName)
        {
            TypeName = typeName;
        }

        internal clsProductType(int typeId,  string typeName)
        {
            Id = typeId;
            TypeName = typeName;
        }

        internal clsProductType() { }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsProductTypeData.AddAsync(TypeName);
            return Id.HasValue;
        }

        public static async Task<clsProductType?> GetByIdAsync(int typeId)
        {
            var dict = await clsProductTypeData.GetByIdAsync(typeId);
            if (dict == null) return null;
            return FetchProductTypeData(dict);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsProductTypeData.GetAllAsync();

        private async Task<bool> __UpdateAsync() => await clsProductTypeData.UpdateAsync(Id, TypeName);
        
        internal static clsProductType FetchProductTypeData(Dictionary<string, object> dict)
        {
            return new clsProductType(
                (int)dict["TypeID"],
                (string)dict["TypeName"]
            );
        }
    }
}
