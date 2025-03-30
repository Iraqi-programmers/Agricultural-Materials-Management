using System.Data;
using System.Diagnostics;
using BLL.Product;
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


        //internal static clsSales FetchSaleData(ref Dictionary<string, object> dict)
        //{



        //    string ID = "";


        //    List<clsSalesDetails> salesDetailsList = new();

        //    if (dict.ContainsKey("SalesDetailsData") && dict["SalesDetailsData"] is List<Dictionary<string, object>> detailsList)
        //    {
        //        foreach (var detail in detailsList)
        //        {
        //            ID = detail.Values.ToString()!;
        //            salesDetailsList.Add(new clsSalesDetails(
        //                (int)detail["DetailID"],
        //                (int) detail["SalesId"],
        //                clsStocks.FetchStockData(ref dict),
        //                (double)detail["Price"],
        //                (int)detail["Quantity"],
        //                (double)detail["TotalCost"],
        //                (double)detail["Profit"],
        //                detail.ContainsValue("WarrantyDate") ? (DateTime)detail["WarrantyDate"] : null
        //            ));
        //        }
        //    }
        //    int id = 0;
        //    // معالجة باقي القيم بشكل آمن
        //    return new clsSales(
        //        id,
        //        salesDetailsList,
        //        clsUsers.FetchUserData(ref dict),
        //        Convert.ToDateTime(dict["Date"]),
        //        Convert.ToDouble(dict["SaleTotalCost"]),
        //        Convert.ToBoolean(dict["IsDebt"]),
        //        Convert.ToDouble(dict["TotalProfit"]),
        //        dict.TryGetValue("PaidAmount", out var paidAmount) ?
        //            Convert.ToDouble(paidAmount) : (double?)null,
        //        clsPerson.FetchPersonData(ref dict)
        //    );
        //}

        internal static clsSales FetchSaleData(ref Dictionary<string, object> dict)
        {
            try
            {


                var salesData = (Dictionary<string, object>)dict["SalesData"];
                int id = Convert.ToInt32(salesData["SealeID"]);



                List<clsSalesDetails> salesDetailsList = new();
                if (dict.TryGetValue("SalesDetailsData", out var detailsData) && detailsData is List<Dictionary<string, object>> detailsList)
                {

                    foreach (var detail in detailsList)
                    {
                        salesDetailsList.Add(new clsSalesDetails(
                            Convert.ToInt32(detail["DetailID"]), id,
                            new clsStocks(Convert.ToInt32(detail["StockID"]), Convert.ToInt32(detail["Quantity"]), detail["Status"].ToString()!, Convert.ToDouble(detail["PurchasePrice"]), Convert.ToDouble(detail["SellingPrice"]),
                                new Product.clsProduct(Convert.ToInt32(detail["ProductID"]),
                                    new clsCompany(Convert.ToInt32(detail["CompanyID"]), detail["Name"].ToString()!),
                                    new clsProductType(Convert.ToInt32(detail["TypeID"]), detail["TypeName"].ToString()!),
                                    new clsSize(Convert.ToInt32(detail["SizeID"]), Convert.ToDouble(detail["Size"])),
                                    new clsThickness(Convert.ToInt32(detail["ThicknessID"]), Convert.ToDouble(detail["Thickness"])),
                                    new clsWarrinty(Convert.ToInt32(detail["WarrintyID"]), Convert.ToUInt16(detail["Period"])))),

                            Convert.ToDouble(detail["Price"]),
                            Convert.ToInt32(detail["Quantity"]),
                            Convert.ToDouble(detail["TotalCost"]),
                            Convert.ToDouble(detail["Profit"]),
                            (DateTime?)Convert.ToDateTime(detail["WarrantyDate"])
                        ));
                    }
                }

                Dictionary<string, object>? userData = null;
                if (dict.TryGetValue("UserData", out var userDataObj) && userDataObj is Dictionary<string, object> tempUserData)
                {
                    userData = tempUserData;
                }



                return new clsSales(
                    id,
                    salesDetailsList,
                    clsUsers.FetchUserData(ref userData!),
                    Convert.ToDateTime(salesData["Date"]),
                    Convert.ToDouble(salesData["TotalCost"]),
                    Convert.ToBoolean(salesData["IsDebt"]),
                    Convert.ToDouble(salesData["TotalProfit"]),
                    (Double?)Convert.ToDouble(salesData["PaidAmount"]),
                    new clsPerson(Convert.ToInt32(salesData["PersonID"]), salesData["PersonFullName"].ToString()!, salesData["PersonNationalNum"].ToString(), salesData["PersonPhoneNumber"].ToString(), salesData["PersonAddress"].ToString())
                );
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


    }
}
