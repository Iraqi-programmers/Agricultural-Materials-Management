using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class clsPurchases
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode;

        public int? PurchaseID;
        public int? SupllierID;
        public int? UserID;
        public DateTime PurchaseDate;
        public decimal TotalPrice { get; set; }
        public decimal TotalPaid { get; set; }

        clsSupplier SupplierInfo { get; set; }
        clsUsers UserInfo { get; set; }
        private clsPurchases( int? purchaseID, int? supllierID, int? userID, DateTime purchaseDate, decimal totalPrice, decimal totalPaid)
        {
            this.PurchaseID = purchaseID;
            this.SupllierID = supllierID;
            this.UserID = userID;
            this.PurchaseDate = purchaseDate;
            this.TotalPrice = totalPrice;
            this.TotalPaid = totalPaid;
            
            this._Mode = enMode.Update;
        }

        public clsPurchases()
        {
            this.PurchaseID = null;
            this.SupllierID = null;
            this.UserID = null;
            this.PurchaseDate = DateTime.Now;
            this.TotalPrice = 0;
            this.TotalPaid = 0;
            UserInfo = new clsUsers();
            SupplierInfo = new clsSupplier();
            this._Mode = enMode.AddNew;
        }

        public static async Task<DataTable?> GetAllPurchase()
        {
            return await clsPurchasesData.GetAllPurchase();
        }

        public static async Task<bool> Delete(int ID)
        {
            return await clsPurchasesData.DeletePurchase(ID);
        }

        private async Task<bool> _AddNew()
        {
            this.PurchaseID = await clsPurchasesData.AddPurchase(this.PurchaseDate, (int)SupllierID, this.TotalPrice, this.TotalPaid, (int)UserID);

            return this.PurchaseID.HasValue;
        }

        private async Task<bool> _Update()
        {
            return await clsPurchasesData.UpdatePurchase((int)PurchaseID, PurchaseDate, (int)SupllierID, TotalPrice, this.TotalPaid, (int)UserID);
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
