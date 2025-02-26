using DAL.Product;
using System.Data;

//Yousif 
namespace BLL.Product
{
    public class clsProductType :absClassesHelperBasc
    {
        string typeName { set; get; }
        public clsProductType(string TypeName)
        {
            Id = null;
            this.typeName = TypeName;
        }
        private clsProductType(int TypeID,  string TypeName)
        {
            this.Id = TypeID;
            this.typeName = TypeName;
        }
        public static async Task<DataTable?> getAllAsDataTableAsync()
        {
            return await clsProductTypeData.getAllAsDatatableAsync();
        }
        public static async Task<clsProductType?> findTypeAsync(int TypeID)
        {
            var data = await clsProductTypeData.findByIDAsync(TypeID);

            return new clsProductType(TypeID, (string)data[1]) ?? null;
        }
        private  async Task<int?> __addAsync()
        {
            return await clsProductTypeData.addAsync(typeName);
        }
        private async Task<bool> __update()
        {
            return await clsProductTypeData.updateAsync(Id, typeName);
        }

        public async Task<bool> saveAsync()
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
