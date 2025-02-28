using DAL.Product;
using System.Data;
using System.Diagnostics.Contracts;
//yousif
namespace BLL.Product
{
    public class clsSize: absClassesHelperBasc
    {
        public double Size { set; get; }

        public clsSize(double size)
        {
            Id = null;
            Size = size;
        }

        private clsSize(int sizeID, double size)
        {
            Id = sizeID;
            Size = size;
        }

        public static async Task<DataTable?> GetAllAsync()
        {
            return await clsSizeData.getAllAsDatatableAsync();
        }

        public static async Task<clsSize?> FindByIDAsync(int SizeID)
        {
            var data = await clsSizeData.findByIDAsync(SizeID);

            return new clsSize(SizeID, data?[1] as double? ?? 0.0) ?? null;
        }

        private async Task<int?> __AddAsync()
        {
            return await clsSizeData.addAsync(Size);
        }

        private async Task<bool> __UpdateAsync()
        {
            return await clsSizeData.updateAsync(this.Id, Size);
        }
        public async Task<bool> aveAsync()
        {
            if(Id == null)
            {
                Id = await __AddAsync();
                return true;
            }
            return await __UpdateAsync();
        }

    }
}
