using DAL.Product;
using Microsoft.Identity.Client;
using System.Data;
//Yousif
namespace BLL.Product
{
    public class clsThickness : absClassesHelperBasc
    {
        double Thickness { set; get; }

        public clsThickness(double thickness)
        {
            Id = null;
            this.Thickness = thickness;
        }

        private clsThickness(int thicknessId, double thickness)
        {
            Id = thicknessId;
            Thickness = thickness;
        }

        public static async Task<DataTable?> GetAllAsync()
        {
            return await clsThicknessData.GetAllAsDataTableAsync();
        }
        
        private  async Task<bool> __AddAsync()
        {
            Id = await clsThicknessData.AddAsync(Thickness);
            return Id.HasValue;
        }

        private  async Task<bool> __UpdateAsync()
        {
            return await clsThicknessData.UpdateAsync(this.Id, Thickness);
        }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        

        public static async Task<clsThickness?> FindByIdAsync(int SizeID)
        {
            var data = await clsThicknessData.FindByIDAsync(SizeID);

            return new clsThickness(SizeID, data?[1] as double? ?? 0.0) ?? null;
        }



    }
}
