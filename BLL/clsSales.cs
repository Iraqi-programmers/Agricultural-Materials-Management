using System.Data;
using DAL;
using Newtonsoft.Json;

namespace BLL
{
    public class clsSales : absClassesHelperBasc
    {
        public DateTime Date { get; private set; }
        public clsPerson? Person { get; set; }
        public clsUsers User { get; private set; }
        public double SaleTotalCost { get; set; }
        public double? PaidAmount {  get; set; }
        public bool IsDebt { get; set; }
        
        public List<clsSalesDetails> SalesDetailsList { get; set; }

        public clsSales(List<clsSalesDetails> salesDetailsList, clsUsers user, double saleTotalCost, bool isDebt, double? paidAmount = null, clsPerson? person = null)
        {
            Person = person;
            User = user;
            SaleTotalCost = saleTotalCost;
            SalesDetailsList = salesDetailsList;
            PaidAmount = paidAmount;
            IsDebt = isDebt;
        }

        private clsSales(int selesId, List<clsSalesDetails> salesDetailsList, clsUsers user, DateTime date, double saleTotalCost, bool isDebt, double? paidAmount = null, clsPerson? person = null)
        {
            Id = selesId;
            Date = date;
            Person = person;
            User = user;
            SaleTotalCost = saleTotalCost;
            PaidAmount = paidAmount;
            IsDebt = isDebt;
            SalesDetailsList = salesDetailsList;
        }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsSalesData.AddAsync(JsonConvert.SerializeObject(SalesDetailsList), User.Id, SaleTotalCost, PaidAmount, Person?.Id);
            return Id.HasValue;
        }

        public static async Task<clsSales?> GetByIdAsync(int saleId)
        {
            var dict = await clsSalesData.GetByIdAsync(saleId);
            if (dict == null) return null;
            return FetchSaleData(ref dict);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsSalesData.GetAllAsync();       

        private async Task<bool> __UpdateAsync() => await clsSalesData.UpdateAsync(User.Id, JsonConvert.SerializeObject(SalesDetailsList), SaleTotalCost);

        public static async Task<bool> DeleteAsync(int? saleId, int? userId, bool returnToStock = false) => await clsSalesData.DeleteAsync(saleId, userId, returnToStock);

        public async Task<bool> DeleteAsync(bool returnToStock = false) => await DeleteAsync(Id, User.Id, returnToStock);

        internal static clsSales FetchSaleData(ref Dictionary<string, object> dict)
        { 
            int id = (int)dict["SaleID"];

            List<clsSalesDetails> salesDetailsList = new();

            if (dict.ContainsKey("SalesDetailsData") && dict["SalesDetailsData"] is List<Dictionary<string, object>> detailsList)
            {
                foreach (var detail in detailsList)
                {
                    salesDetailsList.Add(new clsSalesDetails(
                        (int)detail["DetailID"],
                        id,
                        (int)detail["StockID"],
                        (double)detail["Price"],
                        (int)detail["Quantity"],
                        (double)detail["TotalCost"],
                        detail["WarrantyDate"] != DBNull.Value ? Convert.ToDateTime(detail["WarrantyDate"]) : null
                    ));
                }
            }
            return new clsSales(
                id,
                clsUsers.FetchUserData(ref dict),
                (DateTime)dict["Date"],
                (double)dict["SaleTotalCost"],
                (bool)dict["IsDebt"],
                dict["PaidAmount"] != DBNull.Value ? Convert.ToDouble(dict["PaidAmount"]) : (double?)null,
                clsPerson.FetchPersonData(ref dict)
            )
            {
                SalesDetails = salesDetailsList.Count > 0 ? new clsSalesDetails(salesDetailsList) : null
            };
        }
    }
}
