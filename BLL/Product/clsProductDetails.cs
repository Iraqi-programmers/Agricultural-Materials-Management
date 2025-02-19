using DAL.Product;
using System.Data;
//Yousif
namespace BLL.Product
{
    public class clsProductDetails : absClassesHelper
    {
        public int SizeID { set; get; }
        private clsSize? _Size;
        public  int ThicknessID { set; get; }
        private clsThickness? _Thickness;
        public int CompanyID { set; get; }
        private clsCompany? _Company;
        public clsCompany? Company
        {
            get
            {
                return _Company;
            }
        }
        public int TypeID { set; get; }
        private clsProductType? _ProductType;
        public clsProductType? ProductType
        {
            get
            {
                return _ProductType;
            }
        }
        public clsSize? Size
        {
            get
            {
                return _Size;
            }

        }
        public clsThickness? Thickness
        {
            get
            {
                return _Thickness;
            }
        }

        private async Task PutSizeValue(int SizeID)
        {
            _Size = await clsSize.FindBySizeID(SizeID);
        }
        private async Task PutThickness(int ThicknessID)
        {
            _Thickness =  await clsThickness.FindBySizeID(ThicknessID);
        }
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
            PutSizeValue(SizeID).Wait();
            PutThickness(ThicknessID).Wait(); 
            
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
