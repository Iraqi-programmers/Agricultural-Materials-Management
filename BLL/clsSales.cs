using System.Data;
using System.Diagnostics;
using DAL;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace BLL
{
    public class clsSales : absTransaction
    {
        public clsPerson? Person { get; set; }
        public double SaleTotalCost { get; set; }
        public double? PaidAmount { get; set; }
        public bool IsDebt { get; set; }
        public double TotalProfit { get; set; } //
        public List<clsSalesDetails> SalesDetailsList { get; set; }

        public clsSales(List<clsSalesDetails> salesDetailsList, clsUsers user, double saleTotalCost, bool isDebt, double totalProfit, double? paidAmount = null, clsPerson? person = null)
        {
            Person = person;
            UserInfo = user;
            SaleTotalCost = saleTotalCost;
            SalesDetailsList = salesDetailsList;
            PaidAmount = paidAmount;
            IsDebt = isDebt;
            TotalProfit = totalProfit;
        }

        private clsSales(int selesId, List<clsSalesDetails> salesDetailsList, clsUsers user, DateTime date, double saleTotalCost, bool isDebt, double totalProfit, double? paidAmount = null, clsPerson? person = null)
        {
            Id = selesId;
            Date = date;
            Person = person;
            UserInfo = user;
            SaleTotalCost = saleTotalCost;
            PaidAmount = paidAmount;
            IsDebt = isDebt;
            TotalProfit = totalProfit;
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
            Id = await clsSalesData.AddAsync(JsonConvert.SerializeObject(SalesDetailsList), UserInfo?.Id, SaleTotalCost, PaidAmount, Person?.Id);
            return Id.HasValue;
        }

        public static async Task<clsSales?> GetByIdAsync(int saleId)
        {
            var dict = await clsSalesData.GetByIdAsync(saleId);
            if (dict == null) return null;
            return FetchSaleData(ref dict);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsSalesData.GetAllAsync();       

        private async Task<bool> __UpdateAsync() => await clsSalesData.UpdateAsync(UserInfo?.Id, JsonConvert.SerializeObject(SalesDetailsList), SaleTotalCost);

        public static async Task<bool> DeleteAsync(int? saleId, int? userId, bool returnToStock = false) => await clsSalesData.DeleteAsync(saleId, userId, returnToStock);

        public async Task<bool> DeleteAsync(bool returnToStock = false) => await DeleteAsync(Id, UserInfo?.Id, returnToStock);

        //internal static clsSales FetchSaleData( ref Dictionary<string, object> dict)
        //{
        //    try
        //    {


        //        // عمل نسخة من القاموس إذا كنت تخشى التعديل على الأصل
        //        var localDict = new Dictionary<string, object>(dict);

        //        List<clsSalesDetails> salesDetailsList = new();

        //        if (localDict.ContainsKey("SalesDetailsData") &&
        //            localDict["SalesDetailsData"] is List<Dictionary<string, object>> detailsList)
        //        {
        //            foreach (var detail in detailsList)
        //            {
        //                // تمرير detail كقيمة عادية (بدون ref)
        //                salesDetailsList.Add(new clsSalesDetails(
        //                    (int)detail["DetailID"],
        //                    (int)localDict["SealeID"],
        //                    clsStocks.FetchStockData(ref localDict), // تعديل هذه الدالة لعدم استخدام ref
        //                    (double)detail["Price"],
        //                    (int)detail["Quantity"],
        //                    (double)detail["TotalCost"],
        //                    (double)detail["Profit"],
        //                    detail.ContainsKey("WarrantyDate") ? (DateTime)detail["WarrantyDate"] : null
        //                ));
        //            }
        //        }

        //        return new clsSales(
        //            (int)localDict["SealeID"],
        //            salesDetailsList,
        //            clsUsers.FetchUserData(ref localDict), // تعديل هذه الدالة لعدم استخدام ref
        //            (DateTime)localDict["Date"],
        //            (double)localDict["SaleTotalCost"],
        //            (bool)localDict["IsDebt"],
        //            (double)localDict["TotalProfit"],
        //            localDict.ContainsKey("PaidAmount") ? (double?)localDict["PaidAmount"] : null,
        //            clsPerson.FetchPersonData(ref localDict) // تعديل هذه الدالة لعدم استخدام ref
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"خطأ في تحويل بيانات البيع: {ex.Message}");
        //        throw;
        //    }
        //}


        internal static clsSales FetchSaleData(ref Dictionary<string, object> dict)
        {
            // معالجة SealeID بشكل آمن
            int id = -1;
            if (dict.TryGetValue("SalesData", out var salesDataObj) &&
                salesDataObj is Dictionary<string, object> salesData)
            {
                if (salesData.TryGetValue("SealeID", out var sealeIdObj))
                {
                    id = Convert.ToInt32(sealeIdObj); // الطريقة الآمنة
                }
            }

            List<clsSalesDetails> salesDetailsList = new();

            if (dict.TryGetValue("SalesDetailsData", out var detailsData) &&
                detailsData is List<Dictionary<string, object>> detailsList)
            {
                foreach (var detail in detailsList)
                {
                    salesDetailsList.Add(new clsSalesDetails(
                        Convert.ToInt32(detail["DetailID"]),
                        id,
                        clsStocks.FetchStockData(ref dict),
                        Convert.ToDouble(detail["Price"]),
                        Convert.ToInt32(detail["Quantity"]),
                        Convert.ToDouble(detail["TotalCost"]),
                        Convert.ToDouble(detail["Profit"]),
                        detail.TryGetValue("WarrantyDate", out var warrantyDate) ?
                            (DateTime?)Convert.ToDateTime(warrantyDate) : null
                    ));
                }
            }

            // معالجة باقي القيم بشكل آمن
            return new clsSales(
                id,
                salesDetailsList,
                clsUsers.FetchUserData(ref dict),
                Convert.ToDateTime(dict["Date"]),
                Convert.ToDouble(dict["SaleTotalCost"]),
                Convert.ToBoolean(dict["IsDebt"]),
                Convert.ToDouble(dict["TotalProfit"]),
                dict.TryGetValue("PaidAmount", out var paidAmount) ?
                    Convert.ToDouble(paidAmount) : (double?)null,
                clsPerson.FetchPersonData(ref dict)
            );
        }
    }
}
