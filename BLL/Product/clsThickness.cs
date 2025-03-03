using DAL.Product;
using Microsoft.Identity.Client;
using System.Data;
//Yousif
namespace BLL.Product
{
    public class clsThickness : absBaseEntity
    {
        public double Thickness { set; get; }

        public clsThickness(double thickness)
        {
            Id = null;
            this.Thickness = thickness;
        }

        internal clsThickness(int thicknessId, double thickness)
        {
            Id = thicknessId;
            Thickness = thickness;
        }

        public static async Task<DataTable?> GetAllAsync()
         => await clsThicknessData.GetAllAsDataTableAsync();
        
        
        private  async Task<bool> __AddAsync()
        {
            Id = await clsThicknessData.AddAsync(Thickness);
            return Id.HasValue;
        }

        private  async Task<bool> __UpdateAsync()
            => await clsThicknessData.UpdateAsync(this.Id, Thickness);
        

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        

        public static async Task<clsThickness?> FindByIdAsync(int thicknessId)
        {
            var data = await clsThicknessData.FindByIDAsync(thicknessId);
            if (data == null)
                return null;
            return new clsThickness(thicknessId, (double)data["Thickness"]);
        }



    }
}
