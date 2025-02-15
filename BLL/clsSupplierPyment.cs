using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class clsSupplierPyment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;

        public int SupplierPymentID { get; set; }
        public decimal Amount { get; set; }
        public int SupplierID { get; set; }
        public DateTime PymentDate { get; set; }
        public int PurchaseID { get; set; }
        public int UserID { get; set; }

        private clsSupplierPyment(int supplierPymentID, decimal amount, int supplierID, DateTime pymentDate, int purchaseID, int userID)
        {
            SupplierPymentID = supplierPymentID;
            Amount = amount;
            SupplierID = supplierID;
            PymentDate = pymentDate;
            PurchaseID = purchaseID;
            UserID = userID;
            _Mode = enMode.Update;
        }

        public clsSupplierPyment()
        {
            SupplierPymentID = -1;
            Amount =0;
            SupplierID =-1;
            PymentDate =DateTime.Now;
            PurchaseID = -1;
            UserID = -1;
            _Mode = enMode.AddNew;  
        }

        private async Task<bool> _AddNewSupplierPyment()
        {
            this.SupplierPymentID = (int)await clsSupplierPymentsData.AddSupplierPyment(this.Amount, this.SupplierID, this.PymentDate,this.PurchaseID,this.UserID);
            return this.SupplierPymentID != -1;
        }

        private async Task<bool> _UpdateSupplierPyments()
        {
            return await clsSupplierPymentsData.UpdateSupplierPyment(this.SupplierPymentID,this.Amount,this.PymentDate,this.PurchaseID,this.UserID) ;
        }

        public async Task<bool> Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (await _AddNewSupplierPyment())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                        { return false; }

                    }
                case enMode.Update:
                    {
                        return await _UpdateSupplierPyments();
                    }

            }
            return false;

        }

        public static async Task<DataTable?> GetAllSupplierPyments()
        {
            return await clsSupplierPymentsData.GetAllSupplierPayments();
        }

        public static async Task<bool> DeleteSupplierPymentByID(int id)
        {
            return await clsSupplierPymentsData.DeleteSupplierPyment(id);
        }

        public async Task<object[]?> Find(int id)
        {
            return await clsSupplierPymentsData.GetSupplierPymentBySupplierID(id);
        }
    }
}
