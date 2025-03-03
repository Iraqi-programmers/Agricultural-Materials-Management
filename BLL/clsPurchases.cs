using System.Data;
using BLL.Product;
using DAL;
using Newtonsoft.Json;

namespace BLL
{
    public class clsPurchases : absTransaction
    {
        public double TotalPrice { get; set; }
        public double? TotalPaid { get; set; }
        public bool IsDebt { get; set; }
        public clsSupplier SupplierInfo { get; private set; }
        public List<clsPurchaseDetail> PurchaseDetailsList { get; private set; }

        public clsPurchases(List<clsPurchaseDetail> purchaseDetailsList, clsSupplier supplier, DateTime date, double totalPrice, double? totalPaid, clsUsers user)
        {
            SupplierInfo = supplier;
            Date = date;
            PurchaseDetailsList = purchaseDetailsList;
            TotalPrice = totalPrice;
            TotalPaid = totalPaid;
            IsDebt = totalPrice == totalPaid;
            UserInfo = user;
        }

        private clsPurchases(int purchasesId, List<clsPurchaseDetail> purchaseDetailsList, ref clsSupplier supplier, DateTime date, double totalPrice, bool isDebt, double? totalPaid, ref clsUsers user)
        {
            Id = purchasesId;
            SupplierInfo = supplier;
            Date = date;
            TotalPrice = totalPrice;
            TotalPaid = totalPaid;
            IsDebt = isDebt;
            UserInfo = user;
            PurchaseDetailsList = purchaseDetailsList;
        }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsPurchasesData.AddAsync(JsonConvert.SerializeObject(PurchaseDetailsList), Date, SupplierInfo.Id, TotalPrice, TotalPaid, IsDebt, UserInfo?.Id);
            return Id.HasValue;
        }

        public static async Task<clsPurchases?> GetByIdAsync(int purchaseId)
        {
            var dict = await clsPurchasesData.GetByIdAsync(purchaseId);
            if (dict == null) return null;
            return FetchPurchaseData(ref dict);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsPurchasesData.GetAllAsync();

        private async Task<bool> __UpdateAsync()
            => await clsPurchasesData.UpdateAsync(JsonConvert.SerializeObject(PurchaseDetailsList), Date, Id, SupplierInfo.Id, TotalPrice, TotalPaid, IsDebt, UserInfo?.Id);

        public static async Task<bool> DeleteAsync(int purchaseId) => await clsPurchasesData.DeleteAsync(purchaseId);

        //public async Task<bool> DeleteAsync() => await DeleteAsync(Id);

        internal static clsPurchases? FetchPurchaseData(ref Dictionary<string, object> dict)
        {
            if (dict.ContainsKey("PurchaseData") && dict["PurchaseData"] is Dictionary<string, object> purchaseData)
            {
                int id = (int)purchaseData["PurchaseID"]; 

                clsSupplier supplier = new();
                if (dict.ContainsKey("SupplierPaymentsData") && dict["SupplierPaymentsData"] is Dictionary<string, object> supplierData)
                    supplier = clsSupplier.FetchSupplierData(ref supplierData);

                clsUsers user = new();
                if (dict.ContainsKey("UserData") && dict["UserData"] is Dictionary<string, object> userData)
                    user = clsUsers.FetchUserData(ref userData);

                List<clsPurchaseDetail> purchaseDetailsList = new();
                if (dict.ContainsKey("PurchaseDetailsData") && dict["PurchaseDetailsData"] is List<Dictionary<string, object>> detailsList)
                {
                    foreach (var detail in detailsList)
                    {
                        purchaseDetailsList.Add(new clsPurchaseDetail(
                            id,
                            clsProduct.FetchProductData(detail),
                            (double)detail["Price"],
                            (string)detail["Status"],
                            (int)detail["Quantity"],
                            (DateTime)detail["WarrantyDate"]
                        ));
                    }
                }
                return new clsPurchases(
                    id,
                    purchaseDetailsList,
                    ref supplier,
                    (DateTime)purchaseData["Date"],
                    (double)purchaseData["TotalPrice"],
                    (bool)purchaseData["IsDebt"],
                    purchaseData.ContainsValue("TotalPaid") ? (double)purchaseData["TotalPaid"] : null,
                    ref user
                );
            }
            return null;
        }
    }
}