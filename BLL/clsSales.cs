using System.Data;
using DAL;
using Newtonsoft.Json;

namespace BLL
{
    public class clsSales : absClassesHelperBasc
    {
        public DateTime Date { get; private set; }
        public int? PersonId { get; set; }
        public int UserId { get; private set; }
        public double SaleTotalCost { get; set; }
        public double? PaidAmount {  get; set; }
        public bool IsDebt { get; set; }
        
        public clsSalesDetails? SalesDetails { get; set; }

        public clsSales(clsSalesDetails objSalesDetails, int userId, double saleTotalCost, bool isDebt, double? paidAmount = null, int? personId = null)
        {
            PersonId = personId;
            UserId = userId;
            SaleTotalCost = saleTotalCost;
            SalesDetails = objSalesDetails;
            PaidAmount = paidAmount;
            IsDebt = isDebt;
        }

        private clsSales(int id, int userId, DateTime date, double saleTotalCost, bool isDebt, double? paidAmount = null, int? personId = null)
        {
            Id = id;
            Date = date;
            PersonId = personId;
            UserId = userId;
            SaleTotalCost = saleTotalCost;
            PaidAmount = paidAmount;
            IsDebt = isDebt;
        }

        public async Task<int?> AddSaleAsync(string? fullName = null, string? nationalNum = null, string? phoneNum = null, string? address = null)
        {
            Id = await clsSalesData.AddAsync(JsonConvert.SerializeObject(clsSalesDetails.SalesDetailsList), UserId, SaleTotalCost, PaidAmount, PersonId, fullName, nationalNum, phoneNum, address);
            return Id;
        }

        public static async Task<clsSales?> GetByIdAsync(int saleId)
        {
            var dict = await clsSalesData.GetByIdAsync(saleId);
            if (dict == null) return null;
            return __FetchSaleData(ref dict);
        }

        public static async Task<DataTable?> GetAllSalesAsDataTableAsync() => await clsSalesData.GetAllAsync();       

        public async Task<bool> UpdateAsync(int userId) => await clsSalesData.UpdateAsync(userId, JsonConvert.SerializeObject(clsSalesDetails.SalesDetailsList), SaleTotalCost);

        public static async Task<bool> DeleteAsync(int? saleId, int userId, bool returnToStock = false) => await clsSalesData.DeleteAsync(saleId, userId, returnToStock);

        public async Task<bool> DeleteAsync(bool returnToStock = false) => await DeleteAsync(Id, UserId, returnToStock);

        private static clsSales __FetchSaleData(ref Dictionary<string, object> dict)
        {
            int id = (int)dict["SaleID"];

            List<clsSalesDetails> salesDetailsList = new();
            if (dict.ContainsKey("SalesDetails") && dict["SalesDetails"] is List<Dictionary<string, object>> detailsList)
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
                (int)dict["UserID"],
                (DateTime)dict["Date"],
                (double)dict["SaleTotalCost"],
                (bool)dict["IsDebt"],
                dict["PaidAmount"] != DBNull.Value ? Convert.ToDouble(dict["PaidAmount"]) : (double?)null,
                dict["PersonID"] != DBNull.Value ? Convert.ToInt32(dict["PersonID"]) : (int?)null
            )
            {
                SalesDetails = salesDetailsList.Count > 0 ? new clsSalesDetails(salesDetailsList) : null
            };
        }
    }
}
