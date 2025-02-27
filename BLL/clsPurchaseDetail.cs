using System.Data;
using DAL;

namespace BLL
{
    public class clsPurchaseDetail : absClassesHelperBasc
    {
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public DateTime WarrantyDate { get; set; }

        public static List<clsPurchaseDetail> PurchaseDetailsList { get; private set; } = new List<clsPurchaseDetail>();

        public clsPurchaseDetail(int purchaseId, int productId, double price, string status, int quantity, DateTime warrantyDate)
        {
            PurchaseId = purchaseId;
            ProductId = productId;
            Price = price;
            Status = status;
            Quantity = quantity;
            WarrantyDate = warrantyDate;
            PurchaseDetailsList.Add(this);
        }

        public clsPurchaseDetail(int? purchaseDetailId, int purchaseId, int productId, double price, string status, int quantity, DateTime warrantyDate)
        {
            Id = purchaseDetailId;
            PurchaseId = purchaseId;
            ProductId = productId;
            Price = price;
            Status = status;
            Quantity = quantity;
            WarrantyDate = warrantyDate;
        }

        //public clsPurchaseDetail(List<clsPurchaseDetail> purchaseDetailsList)
        //{
        //    PurchaseDetailsList = purchaseDetailsList;
        //}

        public static bool UpdatePurchaseDetail(int purchaseDetailID, double? price = null, string? status = null, int? quantity = null, DateTime? warrantyDate = null)
        {
            var detail = PurchaseDetailsList.FirstOrDefault(d => d.Id == purchaseDetailID);
            if (detail != null)
            {
                detail.Price = price ?? detail.Price;
                detail.Status = status ?? detail.Status;
                detail.Quantity = quantity ?? detail.Quantity;
                detail.WarrantyDate = warrantyDate ?? detail.WarrantyDate;
                return true;
            }
            return false;
        }

        public static bool RemovePurchaseDetailFromList(int purchaseDetailID)
        {
            var detail = PurchaseDetailsList.FirstOrDefault(d => d.Id == purchaseDetailID);
            if (detail != null)
            {
                PurchaseDetailsList.Remove(detail);
                return true;
            }
            return false;
        }

        public static bool ClearPurchaseDetailsList()
        {
            PurchaseDetailsList.Clear();
            return PurchaseDetailsList.Count == 0;
        }

        public static async Task<DataTable?> GetAllAsync() => await clsPurchasesDetailsData.GetAllAsync();
    }
}
