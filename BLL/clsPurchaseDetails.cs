using System.Data;
using BLL.Product;
using DAL;

namespace BLL
{
    public class clsPurchaseDetails : absBaseEntity
    {
        public int PurchaseId { get; set; }
        public clsProduct Product { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public DateTime WarrantyDate { get; set; }

        public clsPurchaseDetails(clsProduct product, double price, string status, int quantity, DateTime warrantyDate)
        {
            Product = product;
            Price = price;
            Status = status;
            Quantity = quantity;
            WarrantyDate = warrantyDate;
        }

        internal clsPurchaseDetails(int purchaseId, clsProduct product, double price, string status, int quantity, DateTime warrantyDate)
        {
            PurchaseId = purchaseId;
            Product = product;
            Price = price;
            Status = status;
            Quantity = quantity;
            WarrantyDate = warrantyDate;
        }

        public static async Task<clsPurchaseDetails?> GetByIdAsync(int purchaseId)
        {
            var dict = await clsPurchasesDetailsData.GetByIdAsync(purchaseId);
            if (dict == null) return null;
            return FetchPurchaseDetailData(ref dict);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsPurchasesDetailsData.GetAllAsync();

        internal static clsPurchaseDetails FetchPurchaseDetailData(ref Dictionary<string, object> dict)
        {
            clsProduct product = new();

            if (dict.ContainsKey("ProductData") && dict["ProductData"] is Dictionary<string, object> productData)
                product = clsProduct.FetchProductData(productData);

            return new clsPurchaseDetails(
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
