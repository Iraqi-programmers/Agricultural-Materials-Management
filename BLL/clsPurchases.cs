using System.Data;
using DAL;
using Newtonsoft.Json;

namespace BLL
{
    public class clsPurchases : absClassesHelperBasc
    {
        public DateTime Date { get; private set; }
        public int SupplierId { get; set; }
        public double TotalPrice { get; set; }
        public double? TotalPaid { get; set; }
        public bool IsDebt { get; set; }
        public int UserId { get; private set; }

        public clsPurchaseDetail? PurchaseDetails { get; set; }

        public clsPurchases(clsPurchaseDetail purchaseDetails, DateTime date, int supplierId, double totalPrice, double? totalPaid, int userId)
        {
            SupplierId = supplierId;
            Date = date;
            PurchaseDetails = purchaseDetails;
            TotalPrice = totalPrice;
            TotalPaid = totalPaid;
            IsDebt = totalPrice == totalPaid;
            UserId = userId;
        }

        private clsPurchases(int purchasesId, int supplierId, DateTime date, double totalPrice, bool isDebt, double? totalPaid, int userId)
        {
            Id = purchasesId;
            SupplierId = supplierId;
            Date = date;
            TotalPrice = totalPrice;
            TotalPaid = totalPaid;
            IsDebt = isDebt;
            UserId = userId;
        }

        public async Task<int?> AddAsync()
        {
            Id = await clsPurchasesData.AddAsync(JsonConvert.SerializeObject(clsPurchaseDetail.PurchaseDetailsList), Date, SupplierId, TotalPrice, TotalPaid, IsDebt, UserId);
            return Id;
        }

        public static async Task<clsPurchases?> GetByIdAsync(int purchaseId)
        {
            var dict = await clsPurchasesData.GetByIdAsync(purchaseId);
            if (dict == null) return null;
            return __FetchPurchaseData(ref dict);
        }

        public static async Task<DataTable?> GetAllPurchasesAsDataTableAsync() => await clsPurchasesData.GetAllAsync();

        public async Task<bool> UpdateAsync() 
            => await clsPurchasesData.UpdateAsync(JsonConvert.SerializeObject(clsPurchaseDetail.PurchaseDetailsList), Date, Id, SupplierId, TotalPrice, TotalPaid, IsDebt, UserId);

        public static async Task<bool> DeleteAsync(int? purchaseId) => await clsPurchasesData.DeleteAsync(purchaseId);

        public async Task<bool> DeleteAsync() => await DeleteAsync(Id);

        private static clsPurchases __FetchPurchaseData(ref Dictionary<string, object> dict)
        {
            int id = (int)dict["PurchaseID"];

            List<clsPurchaseDetail> purchaseDetailsList = new();
            if (dict.ContainsKey("PurchaseDetails") && dict["PurchaseDetails"] is List<Dictionary<string, object>> detailsList)
            {
                foreach (var detail in detailsList)
                {
                    purchaseDetailsList.Add(new clsPurchaseDetail(
                        id,
                        (int)detail["ProductID"],
                        (double)detail["Price"],
                        (string)detail["Status"],
                        (int)detail["Quantity"],
                        (DateTime)detail["WarrantyDate"]
                    ));
                }
            }
            return new clsPurchases(
                id,
                (int)dict["UserID"],
                (DateTime)dict["Date"],
                (double)dict["TotalPrice"],
                (bool)dict["IsDebt"],
                dict["TotalPaid"] != DBNull.Value ? Convert.ToDouble(dict["TotalPaid"]) : (double?)null,
                dict["SupplierID"] != DBNull.Value ? Convert.ToInt32(dict["SupplierID"]) : (int?)null
            )
            {
                PurchaseDetails = purchaseDetailsList.Count > 0 ? new clsPurchaseDetail(purchaseDetailsList) : null
            };
        }
    }

    //public class clsPurchases : absClassesHelperBasc
    //{
    //    public DateTime Date { get; set; }
    //    public int SupplierId { get; set; }
    //    public decimal TotalPrice { get; set; }
    //    public decimal TotalPaid { get; set; }
    //    public bool IsDebt { get; set; }
    //    public int UserId { get; set; }

    //    public clsPurchases(DateTime purchaseDate, int supplierId, decimal totalPrice, decimal totalPaid, bool isDebt, int userId)
    //    {
    //        Date = purchaseDate;
    //        SupplierId = supplierId;
    //        TotalPrice = totalPrice;
    //        TotalPaid = totalPaid;
    //        IsDebt = isDebt;
    //        UserId = userId;
    //    }

    //    public clsPurchases(int purchaseId, DateTime purchaseDate, int supplierId, decimal totalPrice, decimal totalPaid, bool isDebt, int userId)
    //    {
    //        Id = purchaseId;
    //        Date = purchaseDate;
    //        SupplierId = supplierId;
    //        TotalPrice = totalPrice;
    //        TotalPaid = totalPaid;
    //        IsDebt = isDebt;
    //        UserId = userId;
    //    }

    //    public async Task<int?> AddAsync()
    //    {
    //        Id = await clsPurchasesData.AddAsync(Date, SupplierId, TotalPrice, TotalPaid, IsDebt, UserId);
    //        return Id;
    //    }

    //    public static async Task<DataTable?> GetAllAsync() => await clsPurchasesData.GetAllAsync();

    //    public async Task<bool> UpdateAsync() => await clsPurchasesData.UpdateAsync(Id, Date, SupplierId, TotalPrice, TotalPaid, IsDebt, UserId);

    //    public static async Task<bool> DeleteByIdAsync(int? id) => await clsPurchasesData.DeleteAsync(id);

    //    public async Task<bool> DeleteByIdAsync() => await DeleteByIdAsync(Id);

    //    private static clsPurchases __FetchPurchaseData(ref Dictionary<string, object> dict)
    //    {
    //        return new clsPurchases(
    //            (int)dict["PurchaseID"],
    //            (DateTime)dict["PurchaseDate"],
    //            (int)dict["SupplierID"],
    //            (decimal)dict["TotalPrice"],
    //            (decimal)dict["TotalPaid"],
    //            (bool)dict["IsDebt"],
    //            (int)dict["UserID"]
    //        );
    //    }
    //}
}
