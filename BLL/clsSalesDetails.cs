using System.Data;
using DAL;

namespace BLL
{
    public class clsSalesDetails : absClassesHelper
    {
        public int SaleID { get; set; }
        public int StockID { get; set; }
        public DateTime WarrantyDate { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalCost { get; set; }

        public clsSalesDetails(int stockId, DateTime warrantyDate, double price, int quantity, double totalCost)
        {
            StockID = stockId;
            WarrantyDate = warrantyDate;
            Price = price;
            Quantity = quantity;
            TotalCost = totalCost;
        }

        private clsSalesDetails(int detailId, int saleId, int stockId, DateTime warrantyDate, double price, int quantity, double totalCost)
        {
            Id = detailId;
            SaleID = saleId;
            StockID = stockId;
            WarrantyDate = warrantyDate;
            Price = price;
            Quantity = quantity;
            TotalCost = totalCost;
            _mode = enMode.Update;
        }

        public static async Task<clsSalesDetails?> FindAsync(int detailId)
        {
            var data = await clsSalesDetailsData.GetSaleDetailInfoByIDAsync(detailId);
            return new clsSalesDetails(detailId, data?[1] as int? ?? 0, data?[2] as int? ?? 0, data?[3] as DateTime? ?? DateTime.MinValue, data?[4] as int? ?? 0, data?[5] as int? ?? 0, data?[6] as int? ?? 0) ?? null;
        }

        public static async Task<bool> IsSaleDetailExistAsync(int detailID)
            => await clsSalesDetailsData.IsSaleDetailExistAsync(detailID);

        
        public static async Task<DataTable?> GetAllSalesDetailsAsDataTableAsync()
            => await clsSalesDetailsData.GetAllSaleDetailsAsDataTableAsync();

        public static async Task<List<object[]>?> GetAllSalesDetailsAsListAsync()
            => await clsSalesDetailsData.GetAllSaleDetailsAsListAsync();

        public static async Task<bool> DeleteMultipleAsync(List<int> detailIDs, int userId)
            => await clsSalesDetailsData.DeleteMultipleSaleDetailsAsync(detailIDs, userId);
    }
}
