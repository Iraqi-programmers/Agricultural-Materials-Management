using System.Data;
using DAL;

namespace BLL
{
    public class clsSalesDetails : absClassesHelper
    {
        public int StockID { get; set; }
        public DateTime WarrantyDate { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        //public clsSalesDetails()
        //{
        //    DetailID = null;
        //    StockID = 0;
        //    WarrantyDate = DateTime.MinValue;
        //    Price = 0;
        //    Quantity = 0;
        //    Mode = enMode.AddNew;
        //}

        private clsSalesDetails(int detailID, int stockID, DateTime warrantyDate, decimal price, int quantity)
        {
            _id = detailID;
            StockID = stockID;
            WarrantyDate = warrantyDate;
            Price = price;
            Quantity = quantity;
            //Mode = enMode.Update;
        }

        public static async Task<clsSalesDetails?> FindAsync(int detailID)
        {
            var data = await clsSalesDetailsData.GetSaleDetailInfoByIDAsync(detailID);
            return new clsSalesDetails(detailID, Convert.ToInt32(data[0]), Convert.ToDateTime(data[1]), Convert.ToDecimal(data[2]), Convert.ToInt32(data[3])) ?? null;
        }

        public static async Task<bool> IsSaleDetailExistAsync(int detailID)
            => await clsSalesDetailsData.IsSaleDetailExistAsync(detailID);

        //public async Task<bool> SaveAsync()
        //{
        //    if (Mode == enMode.AddNew)
        //    {
        //        var newId = await clsSalesDetailsData.AddNewSaleDetailAsync(StockID, WarrantyDate, Price, Quantity);
        //        if (newId.HasValue)
        //        {
        //            DetailID = newId.Value;
        //            Mode = enMode.Update;
        //            return true;
        //        }
        //    }
        //    else
        //    {
        //        return await clsSalesDetailsData.UpdateSaleDetailDataAsync(DetailID, StockID, WarrantyDate, Price, Quantity);
        //    }
        //    return false;
        //}

        //public async Task<bool> SaveAsync()
        //    => await clsSalesDetailsData.UpdateSaleDetailDataAsync(DetailID, StockID, WarrantyDate, Price, Quantity);

        //public static async Task<bool> DeleteAsync(int detailID)
        //    => await clsSalesDetailsData.DeleteSaleDetailByIDAsync(detailID);

        public static async Task<bool> DeleteMultipleAsync(List<int> detailIDs)
            => await clsSalesDetailsData.DeleteMultipleSaleDetailsAsync(detailIDs);

        public static async Task<DataTable> GetAllSalesDetailsAsDataTableAsync()
            => await clsSalesDetailsData.GetAllSaleDetailsAsDataTableAsync();

        public static async Task<List<object[]>> GetAllSalesDetailsAsListAsync()
            => await clsSalesDetailsData.GetAllSaleDetailsAsListAsync();
    }

}
