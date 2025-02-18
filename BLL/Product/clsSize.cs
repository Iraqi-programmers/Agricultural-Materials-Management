using DAL.Product;
using System.Data;
//yousif
namespace BLL.Product
{
    public class clsSize:absClassesHelper
    {
        double Size { set; get; }

        public clsSize(double size)
        {
            _mode = enMode.AddNew;
            Id = null;
            Size = size;
        }

        private clsSize(int SizeID, double size)
        {
            Id = SizeID;
            Size = size;
            _mode = enMode.Update;
        }

        public static async Task<DataTable?> GetAllAsDataTableAsync()
        {
            return await clsSizeData.GetAllAsDatatableAsync();
        }

        public static async Task<clsSize?> FindBySizeID(int SizeID)
        {
            var data = await clsSizeData.FindByIDAsync(SizeID);

            return new clsSize(SizeID, data?[1] as double? ?? 0.0) ?? null;
        }

        public async Task<bool> SaveAsync()
        {
            if (_mode == enMode.AddNew)
            {
                var newId = await clsSizeData.AddAsync(Size);
                if (newId.HasValue)
                {
                    Id = newId.Value;
                    _mode = enMode.Update;
                    return true;
                }
                else
                    return false;
            }
            else
                return await clsSizeData.UpdateAsync(Id, this.Size);

        }

    }
}
