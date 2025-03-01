using System.Data;
using DAL;

namespace BLL
{
    public class clsSupplierPyment : absClassesHelperBasc
    {
        public decimal Amount { get; set; }
        public DateTime PymentDate { get; set; }
        public int PurchaseId { get; set; }

        public clsSupplier SupplierInfo { get; set; }

        public clsUsers UserInfo { get; private set; }

        public clsSupplierPyment(clsSupplier supplier, decimal amount, DateTime pymentDate, int purchaseId, clsUsers userInfo)
        {
            SupplierInfo = supplier;
            Amount = amount;
            PymentDate = pymentDate;
            PurchaseId = purchaseId;
            UserInfo = userInfo;
        }

        private clsSupplierPyment(int? supplierPymentId, clsSupplier supplier, decimal amount, DateTime pymentDate, int purchaseId, clsUsers userInfo)
        {
            Id = supplierPymentId;
            SupplierInfo = supplier;
            Amount = amount;
            PymentDate = pymentDate;
            PurchaseId = purchaseId;
            UserInfo = userInfo;
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsSupplierPymentsData.AddAsync(Amount, SupplierInfo.Id, PymentDate, PurchaseId, UserInfo.Id);
            if (!Id.HasValue) return false;
            return await __UpdateAsync();
        }

        private async Task<bool> __UpdateAsync()
            => await clsSupplierPymentsData.UpdateAsync(Id, Amount, PymentDate, PurchaseId, UserInfo.Id) ;

        public async Task<bool> Save()
        {
            if (!Id.HasValue) 
                return await __AddAsync();
            return await __UpdateAsync();
        }

        public static async Task<DataTable?> GetAllSupplierPyments()
            => await clsSupplierPymentsData.GetAllAsync();

        public static async Task<bool> DeleteSupplierPymentByID(int id)
            => await clsSupplierPymentsData.DeleteAsync(id);

        public async Task<Dictionary<string, object>?> Find(int id)
            => await clsSupplierPymentsData.GetBySupplierIdAsync(id);
    }
}
