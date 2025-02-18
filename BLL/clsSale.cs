using System.Data;
using DAL;

namespace BLL
{
    public class clsSale : absClassesHelper
    {
        public DateTime Date { get; set; }
        public int? PersonId { get; set; }
        public int UserId { get; set; }

        public List<object[]> Details { get; private set; }

        public clsSale(List<object[]> details)
        {
            Id = null;
            Details = details;
        }

        //private clsSale(int saleId, DateTime date, int? personId, int userId, List<object[]> details)
        //{
        //    Id = saleId;
        //    Date = date;
        //    PersonId = personId;
        //    UserId = userId;
        //    Details = details;
        //    _mode = enMode.Update;
        //}

        ////public static async Task<clsSale?> FindAsync(int saleId)
        ////{
        ////    var data = await clsSalesData.GetSaleInfoByIDAsync(saleId);
        ////    return new clsSale(saleId, Convert.ToDateTime(data[0]), Convert.ToInt32(data[1]), Convert.ToInt32(data[2])) ?? null;
        ////}

        public static async Task<DataTable?> GetAllSalesAsDataTableAsync() => await clsSalesData.GetAllSalesAsDataTableAsync();
        public static async Task<List<object[]>?> GetAllSalesAsListAsync() => await clsSalesData.GetAllSalesAsListAsync();

        public static async Task<bool> IsSaleExistAsync(int saleID) => await clsSalesData.IsSaleExistAsync(saleID);

        public async Task<int?> AddSaleWithDetailsAsync(int userId, int? personId = null, string? fullName = null, string? nationalNum = null, string? phoneNum = null, string? address = null)
            => await clsSalesData.AddSaleWithDetailsAsync(Details, userId, personId, fullName, nationalNum, phoneNum, address);
          

        public  async Task<bool> UpdateSaleDataAsync(int userId)
            => await clsSalesData.UpdateSaleDetailsAsync(userId, Details);

        //public async Task<bool> AddSaleWithDetailsAsync(int userId, string? fullName = null, string? nationalNum = null, string? phoneNum = null, string? address = null)
        //{
        //    if (_mode == enMode.AddNew)
        //    {
        //        var saleID = await clsSalesData.AddSaleWithDetailsAsync(Details, userId, fullName, nationalNum, phoneNum, address);
        //        if (saleID.HasValue)
        //        {
        //            Id = saleID;
        //            _mode = enMode.Update;
        //            return true;
        //        }
        //    }
        //    else
        //        return await clsSalesData.UpdateSaleDataAsync(Id, Date, Details);
        //    return false;
        //}

        public static async Task<bool> DeleteAsync(int saleId, int userId) => await clsSalesData.DeleteSaleByIDAsync(saleId, userId);
        public static async Task<bool> DeleteMultipleAsync(List<int> saleIDs, int userId) => await clsSalesData.DeleteMultipleSalesAsync(saleIDs, userId);
    }
}
