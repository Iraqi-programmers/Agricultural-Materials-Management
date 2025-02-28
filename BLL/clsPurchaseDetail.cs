using System.Data;
using BLL.Product;
using DAL;

namespace BLL
{
    public class clsPurchaseDetail : absClassesHelperBasc
    {
        public int PurchaseId { get; set; }
        public clsProduct Product { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public DateTime WarrantyDate { get; set; }

        public clsPurchaseDetail(clsProduct product, double price, string status, int quantity, DateTime warrantyDate)
        {
            Product = product;
            Price = price;
            Status = status;
            Quantity = quantity;
            WarrantyDate = warrantyDate;
        }

        private clsPurchaseDetail(int purchaseId, clsProduct product, double price, string status, int quantity, DateTime warrantyDate)
        {
            PurchaseId = purchaseId;
            Product = product;
            Price = price;
            Status = status;
            Quantity = quantity;
            WarrantyDate = warrantyDate;
        }

        public static async Task<clsPurchaseDetail?> GetByIdAsync(int purchaseDetailId)
        {
            Id = await clsPurchaseDetailData.GetByIdAsync(purchaseDetailId);
        }

        //internal clsPurchaseDetail(int? purchaseDetailId, int purchaseId, clsProduct product, double price, string status, int quantity, DateTime warrantyDate)
        //{
        //    Id = purchaseDetailId;
        //    PurchaseId = purchaseId;
        //    Product = product;
        //    Price = price;
        //    Status = status;
        //    Quantity = quantity;
        //    WarrantyDate = warrantyDate;
        //}

        //public bool UpdatePurchaseDetail(int purchaseDetailID, double? price = null, string? status = null, int? quantity = null, DateTime? warrantyDate = null)
        //{
        //    var detail = PurchaseDetailsList.FirstOrDefault(d => d.Id == purchaseDetailID);
        //    if (detail != null)
        //    {
        //        detail.Price = price ?? detail.Price;
        //        detail.Status = status ?? detail.Status;
        //        detail.Quantity = quantity ?? detail.Quantity;
        //        detail.WarrantyDate = warrantyDate ?? detail.WarrantyDate;
        //        return true;
        //    }
        //    return false;
        //}

        //public bool RemovePurchaseDetailFromList(int purchaseDetailID)
        //{
        //    var detail = PurchaseDetailsList.FirstOrDefault(d => d.Id == purchaseDetailID);
        //    if (detail != null)
        //    {
        //        PurchaseDetailsList.Remove(detail);
        //        return true;
        //    }
        //    return false;
        //}

        //public static bool ClearPurchaseDetailsList()
        //{
        //    PurchaseDetailsList.Clear();
        //    return PurchaseDetailsList.Count == 0;
        //}

        public static async Task<DataTable?> GetAllAsync() => await clsPurchasesDetailsData.GetAllAsync();

        internal static clsPurchaseDetail FetchPurchaseDetailData(ref Dictionary<string, object> dict)
        {
            clsProduct product = new();

            if (dict.ContainsKey("ProductData") && dict["ProductData"] is Dictionary<string, object> productData)
                product = clsProduct.FetchProductData(ref productData);

            return new clsPurchaseDetail(
                (int)dict["PurchaseID"],
                product,
                (double)dict["Price"],
                (string)dict["Status"],
                (int)dict["Quantity"],
                (DateTime)dict["WarrantyDate"]
            );
        }
    }
}
