using System.Data;
using DAL.Product;

namespace BLL.Product
{
    public class clsThickness : absBaseEntity
    {
        public double Thickness { set; get; }

        public clsThickness(double thickness)
        {
            Thickness = thickness;
        }

        internal clsThickness(int thicknessId, double thickness)
        {
            Id = thicknessId;
            Thickness = thickness;
        }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        private  async Task<bool> __AddAsync()
        {
            Id = await clsThicknessData.AddAsync(Thickness);
            return Id.HasValue;
        }

        public static async Task<clsThickness?> GetByIdAsync(int thicknessId)
        {
            var dict = await clsThicknessData.FindByIDAsync(thicknessId);
            if (dict == null) return null;
            return FetchThicknessData(dict);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsThicknessData.GetAllAsDataTableAsync();

        private async Task<bool> __UpdateAsync() => await clsThicknessData.UpdateAsync(Id, Thickness);

        internal static clsThickness FetchThicknessData(Dictionary<string, object> dict)
        {
            return new clsThickness(
                (int)dict["ThicknessID"],
                (double)dict["Thickness"]
            );
        }
    }
}
