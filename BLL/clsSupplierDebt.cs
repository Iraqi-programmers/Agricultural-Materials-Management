using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class clsSupplierDebt
    {
        public enum enMode {AddNew=0,Update=1};
        public enMode _Mode=enMode.AddNew; 

        public int SupplierDebtID {  get; set; }    
        public int SupplierID { get; set; }   
        public decimal Amount { get; set; }    
        public DateTime DabtPaymentDate { get; set; }

        private clsSupplierDebt( int supplierDebtID, int supplierID, decimal amount, DateTime dabtPaymentDate)
        {
          
            SupplierDebtID = supplierDebtID;
            SupplierID = supplierID;
            Amount = amount;
            DabtPaymentDate = dabtPaymentDate;
            this._Mode = enMode.Update;
        }

        public clsSupplierDebt()
        {

            SupplierDebtID =-1;
            SupplierID =-1;
            Amount = 0;
            DabtPaymentDate = DateTime.Now;
            this._Mode = enMode.AddNew;
        }


        private async Task<bool> _AddNewSupplierDebt()
        {
            this.SupplierDebtID=(int)await clsSupplierDebtsData.AddSupplierDebts(this.SupplierID,this.Amount,this.DabtPaymentDate);
            return this.SupplierDebtID != -1;
        }

        private async Task<bool> _UpdateSupplierDebt()
        {
            return await clsSupplierDebtsData.UpdateSupplierDebt(this.SupplierID, this.Amount, this.DabtPaymentDate);
        }


        public async Task<bool> Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (await _AddNewSupplierDebt())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                        { return false; }

                    }
                case enMode.Update:
                    {
                        return await _UpdateSupplierDebt();
                    }

            }
            return false;

        }

        public static async Task<DataTable?> GetAllSuppliersDebt()
        {
            return await clsSupplierDebtsData.GetAllSupplierDept();
        }

        public static async Task<bool> DeleteSupplierDebt(int id)
        {
            return await clsSupplierDebtsData.DeleteSupplierDebt(id);   
        }

        public async Task<object[]?> Find(int id)
        {
            return await clsSupplierDebtsData.GetSupplierDebtByID(id);  
        }
    }
}
