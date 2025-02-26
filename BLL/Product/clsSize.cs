using DAL.Product;
using System.Data;
using System.Diagnostics.Contracts;
//yousif
namespace BLL.Product
{
    public class clsSize: absClassesHelperBasc
    {
        double size { set; get; }

        public clsSize(double size)
        {
            Id = null;
            this.size = size;
        }

        private clsSize(int sizeID, double size)
        {
            Id = sizeID;
            this.size = size;
        }

        public static async Task<DataTable?> getAllAsDataTableAsync()
        {
            return await clsSizeData.getAllAsDatatableAsync();
        }

        public static async Task<clsSize?> findBySizeID(int SizeID)
        {
            var data = await clsSizeData.findByIDAsync(SizeID);

            return new clsSize(SizeID, data?[1] as double? ?? 0.0) ?? null;
        }

        private async Task<int?> __addAsync()
        {
            return await clsSizeData.addAsync(size);
        }

        private async Task<bool> __updateAsync()
        {
            return await clsSizeData.updateAsync(this.Id, size);
        }
        public async Task<bool> saveAsync()
        {
            if(Id == null)
            {
                Id = await __addAsync();
                return true;
            }
            return await __updateAsync();
        }

    }
}
