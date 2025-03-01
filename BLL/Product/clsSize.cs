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
            => await clsSizeData.GetAllAsDatatableAsync();
        

        public static async Task<clsSize?> FindByIdAsync(int sizeId)
        {
            var data = await clsSizeData.FindByIDAsync(sizeId);
            if (data == null)
                return null;

            return new clsSize(sizeId, (double)data["Size"]);
        }

        private async Task<bool> __AddAsync()
        {
            Id =  await clsSizeData.AddAsync(Size);
            return Id.HasValue;
        }

        private async Task<bool> __UpdateAsync()
             =>await clsSizeData.UpdateAsync(this.Id, Size);
        
        public async Task<bool> SaveAsync()
        {
            if(!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

    }
}
