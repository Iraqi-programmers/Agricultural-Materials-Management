using DAL.Product;
using Microsoft.Identity.Client;
using System.Data;
//Yousif
namespace BLL.Product
{
    public class clsThickness : absClassesHelperBasc
    {
        double thickness { set; get; }

        public clsThickness(double Thickness)
        {
            Id = null;
            this.thickness = Thickness;
        }

        private clsThickness(int ThicknessID, double Thickness)
        {
            Id = ThicknessID;
            this.thickness = Thickness;
        }

        public static async Task<DataTable?> getAllAsDataTableAsync()
        {
            return await clsThicknessData.getAllAsDataTableAsync();
        }
        
        private  async Task<int?> __addAsync()
        {
            return await clsThicknessData.addAsync(thickness);
        }

        private  async Task<bool> __update()
        {
            return await clsThicknessData.updateAsync(this.Id, thickness);
        }

        public async Task<bool> save()
        {
            if(this.Id == null)
            {
                Id = await __addAsync();
                return true;
            }
            return await __update();
        }

        

        public static async Task<clsThickness?> findBySizeID(int SizeID)
        {
            var data = await clsThicknessData.findByIDAsync(SizeID);

            return new clsThickness(SizeID, data?[1] as double? ?? 0.0) ?? null;
        }



    }
}
