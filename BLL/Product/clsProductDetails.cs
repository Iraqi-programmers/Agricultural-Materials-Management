using DAL.Product;
using System.Data;
//Yousif
namespace BLL.Product
{
    public class clsProductDetails : absClassesHelper
    {
        int SizeID { set; get; }
        int ThicknessID { set; get; } 


        public clsProductDetails(int SizeID, int ThicknessID)
        {
            Id = null;
            this.SizeID = SizeID;
            this.ThicknessID = ThicknessID;
        }

        private clsProductDetails(int DetailID, int ThicknessID, int SizeID)
        {
            Id = DetailID;
            this.ThicknessID = ThicknessID;
            this.SizeID = SizeID;
        
        }

        public static async Task<DataTable?> GetAllAsDataTableAsync()
        {
            return await clsProductDetailsData.GetAllAsyncAsDataTable();
        }

        public static async Task<clsProductDetails?> FindBySizeID(int SizeID)
        {
            var data = await clsThicknessData.FindByIDAsync(SizeID);

            return new clsProductDetails(SizeID, data?[1] as int? ?? 0, data?[2] as int? ?? 0) ?? null;
        }

        public async Task<bool> SaveAsync()
        {

                var newId = await clsProductDetailsData.AddAsync(SizeID, ThicknessID);
                if (newId.HasValue)
                {
                    Id = newId.Value;
                    _mode = enMode.Update;
                    return true;
                }
                else
                    return false;
        }


    }
}
