using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class clsSale : absClassesHelper
    {
        public DateTime Date { get; set; }
        public int? PersonID { get; set; }

        public List<Dictionary<string, object>> Details { get; private set; }

        public clsSale()
        {
            _id = null;
            Date = DateTime.Now;
            PersonID = 0;
            UserID = 0;
            Details = new List<Dictionary<string, object>>();
            _mode = enMode.AddNew;
        }

        private clsSale(int saleID, DateTime date, int personID, int userID)
        {
            _id = saleID;
            Date = date;
            PersonID = personID;
            Details = new List<Dictionary<string, object>>();
            _mode = enMode.Update;
        }

        //private clsSale(DateTime date, List<Dictionary<string, object>> Details)
        //{
        //    Date = date;
        //    Details = new List<Dictionary<string, object>>();
        //    Mode = enMode.Update;
        //}

        public static async Task<clsSale?> FindAsync(int saleID)
        {
            var data = await clsSalesData.GetSaleInfoByIDAsync(saleID);
            return new clsSale(saleID, Convert.ToDateTime(data[0]), Convert.ToInt32(data[1]), Convert.ToInt32(data[2])) ?? null;
        }

        public static async Task<bool> IsSaleExistAsync(int saleID) => await clsSalesData.IsSaleExistAsync(saleID);

        public async Task<bool> SaveAsync()
        {
            if (_mode == enMode.AddNew)
            {
                var saleID = await clsSalesData.AddNewSaleWithDetailsAsync(Date, UserID, Details, PersonID);
                if (saleID.HasValue)
                {
                    _id = saleID;
                    _mode = enMode.Update;
                    return true;
                }
            }
            else
                return await clsSalesData.UpdateSaleDataAsync(_id, Date, Details);
            return false;
        }

        public static async Task<bool> DeleteAsync(int saleID) => await clsSalesData.DeleteSaleByIDAsync(saleID);
        public static async Task<bool> DeleteMultipleAsync(List<int> saleIDs) => await clsSalesData.DeleteMultipleSalesAsync(saleIDs);

        public static async Task<DataTable> GetAllSalesAsDataTableAsync() => await clsSalesData.GetAllSalesAsDataTableAsync();
        public static async Task<List<object[]>> GetAllSalesAsListAsync() => await clsSalesData.GetAllSalesAsListAsync();
    }

}
