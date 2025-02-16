using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class clsSupplier
    {
        public enum enMode { AddNew = 0, Update = 1};
        public enMode _Mode = enMode.AddNew;

        public int ID { get; set; }
        public string SupplierName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        private clsSupplier(int iD, string supplierName, string phone, string address)
        {
            this.ID = iD;
            this.SupplierName = supplierName;
            this.Phone = phone;
            this.Address = address;
            this._Mode = enMode.Update;
        }

        public clsSupplier()
        {
            this.ID = -1;
            this.SupplierName = "";
            this.Phone = "";
            this.Address = "";

            this._Mode = enMode.AddNew;
        }

        private async Task<bool> _AddNewSupplier()
        {
             this.ID = (int)await clsSupplierData.AddSupplier(this.SupplierName, this.Phone, this.Address);
            return this.ID != -1;
        }

        private async Task<bool> _UpdateSupplier()
        {
            return await clsSupplierData.UpdateSupplier(this.ID, this.SupplierName, this.Phone, this.Address);
        }

        public async Task<bool> Save()
        {
           
            return _Mode switch
            {
                enMode.AddNew => await _AddNewSupplier(),
                enMode.Update => await _UpdateSupplier(),
                _ => false
            };
        }

        //public async Task<bool> Save()
        //{
        //    switch(_Mode)
        //    {
        //            case enMode.AddNew:
        //            {
        //                if (await _AddNewSupplier())
        //                {
        //                    _Mode = enMode.Update;
        //                    return true;
        //                }
        //                else
        //                { return false; }

        //            }
        //            case enMode.Update:
        //            {
        //                return await _UpdateSupplier();
        //            }

        //    }
        //    return false;

        //}

        public static async Task<DataTable?> GetAllSuppliers()
        {
            return await clsSupplierData.GetAllSupplier();
        }

        public static async Task<bool> DeleteSupplier(int id)
        {
            return await clsSupplierData.DeleteSupplier(id);
        }

        public static async Task<object[]?> Find(int id)
        {
            return await clsSupplierData.GetSupplierByID(id);
        }

    }

}
