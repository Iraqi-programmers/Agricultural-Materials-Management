using DAL.Product;
using System.Data;

namespace BLL.Product
{
    public class clsSize: absBaseEntity
    {
        public double Size { set; get; }

        public clsSize(double size)
        {
            Size = size;
        }

        internal clsSize(int sizeId, double size)
        {
            Id = sizeId;
            Size = size;
        }

        public async Task<bool> SaveAsync()
        {
            if(!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        private async Task<bool> __AddAsync()
        {
            Id =  await clsSizeData.AddAsync(Size);
            return Id.HasValue;
        }

        public static async Task<clsSize?> GetByIdAsync(int sizeId)
        {
            var data = await clsSizeData.FindByIDAsync(sizeId);
            if (data == null) return null;
            return new clsSize(sizeId, (double)data["Size"]);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsSizeData.GetAllAsDatatableAsync();

        private async Task<bool> __UpdateAsync() => await clsSizeData.UpdateAsync(Id, Size);

        internal static clsSize FetchSizeData(Dictionary<string, object> dict)
        {
            return new clsSize(
                (int)dict["SizeID"],
                (double)dict["Size"]
            );
        }
    }
}
