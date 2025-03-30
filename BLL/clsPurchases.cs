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
        public List<clsPurchaseDetails> PurchaseDetailsList { get; private set; }

        public clsPurchases(List<clsPurchaseDetails> purchaseDetailsList, clsSupplier supplier, DateTime date, double totalPrice, double? totalPaid, clsUsers user)
        {
            SupplierInfo = supplier;
            Date = date;
            PurchaseDetailsList = purchaseDetailsList;
            TotalPrice = totalPrice;
            TotalPaid = totalPaid;
            IsDebt = totalPrice == totalPaid;
            UserInfo = user;
        }

        internal clsPurchases(int purchasesId, List<clsPurchaseDetails> purchaseDetailsList,  clsSupplier supplier, DateTime date, double totalPrice, bool isDebt, double? totalPaid,  clsUsers user)
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


        internal static clsPurchases? FetchPurchaseData(ref Dictionary<string, object> dict)
        {
            try
            {
                var PurchaseData = (Dictionary<string, object>)dict["PurchaseData"];
                int id = Convert.ToInt32(PurchaseData["PurchaseID"]);

                List<clsPurchaseDetails> PurchaseDetilsList = new();

                if (dict.TryGetValue("PurchaseDetailsData", out var DetilsData) && DetilsData is List<Dictionary<string, object>> detilsList) 
                {

                    foreach (var detail in detilsList)
                    {
                        PurchaseDetilsList.Add(new clsPurchaseDetails(
                           Convert.ToInt32(detail["DetailID"]),
                          new Product.clsProduct(Convert.ToInt32(detail["ProductID"]),
                                    new clsCompany(Convert.ToInt32(detail["CompanyID"]), detail["Name"].ToString()!),
                                    new clsProductType(Convert.ToInt32(detail["TypeID"]), detail["TypeName"].ToString()!),
                                    new clsSize(Convert.ToInt32(detail["SizeID"]), Convert.ToDouble(detail["Size"])),
                                    new clsThickness(Convert.ToInt32(detail["ThicknessID"]), Convert.ToDouble(detail["Thickness"])),
                                    new clsWarrinty(Convert.ToInt32(detail["WarrintyID"]), Convert.ToUInt16(detail["Period"]))),
                          (double)detail["Price"],
                          (string)detail["Status"],
                          Convert.ToInt32(detail["Quantity"]),
                          (DateTime)Convert.ToDateTime(detail["WarrantyDate"])
                       ));

                    }
                }

              
                return new clsPurchases(
                    id,
                    PurchaseDetilsList,
                     new  clsSupplier(Convert.ToInt32(PurchaseData["SupllierID"]), PurchaseData["SupplierName"].ToString()!, PurchaseData["SupplierPhone"].ToString()!,
                    (bool)PurchaseData["IsPerson"], PurchaseData["SupplierAddress"].ToString()!),
                    (DateTime)Convert.ToDateTime(PurchaseData["PurchaseDate"]),
                    (double)PurchaseData["TotalPrice"],
                    (bool)PurchaseData["IsDebt"],
                    (double?)PurchaseData["TotalPaid"],
                    clsUsers.FetchUserData(ref PurchaseData)
                );


            }
            catch (Exception)
            {
                return null;
            }

            
        }
    }
}