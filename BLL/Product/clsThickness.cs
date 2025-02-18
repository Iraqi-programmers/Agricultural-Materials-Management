using DAL.Product;
using System.Data;
//Yousif
namespace BLL.Product
{
    public class clsThickness :absClassesHelper
    {
        double Thickness { set; get; }

        public clsThickness(double Thickness)
        {
            _mode = enMode.AddNew;
            Id = null;
            this.Thickness = Thickness;
        }

        private clsThickness(int ThicknessID, double Thickness)
        {
            Id = ThicknessID;
            this.Thickness = Thickness;
            _mode = enMode.Update;
        }

        public static async Task<DataTable?> GetAllAsDataTableAsync()
        {
            return await clsSizeData.GetAllAsDatatableAsync();
        }

        public static async Task<clsThickness?> FindBySizeID(int SizeID)
        {
            var data = await clsThicknessData.FindByIDAsync(SizeID);

            return new clsThickness(SizeID, data?[1] as double? ?? 0.0) ?? null;
        }

        public async Task<bool> SaveAsync()
        {
            if (_mode == enMode.AddNew)
            {
                var newId = await clsThicknessData.AddAsync(Thickness);
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
                return await clsThicknessData.UpdateAsync(Id, this.Thickness);

        }


    }
}
