using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class clsPurchaseDetail
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode;

        public int PurchaseDetailID {  get; set; }   
        public int PurchesID {  get; set; }   
        public int ProductID {  get; set; }   
        public decimal Price {  get; set; }
        public string Status { get; set; } 
        public int Quantity { get; set; }
        public DateTime WarrintyDate { get; set; }

        private clsPurchaseDetail(int purchaseDetailID, int purchesID, int productID, decimal price, string status, int quantity, DateTime warrintyDate)
        {
            PurchaseDetailID = purchaseDetailID;
            PurchesID = purchesID;
            ProductID = productID;
            Price = price;
            Status = status;
            Quantity = quantity;
            WarrintyDate = warrintyDate;
            _Mode = enMode.Update;
        }

        private clsPurchaseDetail()
        {
            PurchaseDetailID = -1;
            PurchesID =-1;
            ProductID = -1;
            Price = 0;
            Status = "";
            Quantity = 0;
            WarrintyDate = DateTime.Now;
            _Mode = enMode.AddNew;
        }

        public static async Task<DataTable?> GetAllPurchaseDetails()
        {
            return await clsPurchasesDetailsData.GetAllPurchaseDetail();
        }

        public static async Task<bool> Delete(int ID)
        {
            return await clsPurchasesDetailsData.DeletePurchaseDetils(ID);
        }

        private async Task<bool> _AddNew()
        {
            this.PurchaseDetailID =(int) await clsPurchasesDetailsData.AddPurchaseDetail( this.PurchesID,  this.ProductID,  this.Price, this.Status,  this.Quantity, this.WarrintyDate);

            return this.PurchaseDetailID!=-1;
        }

        private async Task<bool> _Update()
        {
            return await clsPurchasesDetailsData.UpdatePurchaseDetail(this.PurchaseDetailID ,this.Price, this.Status, this.Quantity, this.WarrintyDate);
        }

        public async Task<bool> Save()
        {
            return _Mode switch
            {
                enMode.AddNew => await _AddNew(),
                enMode.Update => await _Update(),
                _ => false
            };
        }





    }
}
