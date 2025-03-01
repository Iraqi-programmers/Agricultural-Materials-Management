using System.Data;
using DAL;

namespace BLL
{
    public class clsSupplierPyment : absClassesHelperBasc
    {
        public decimal Amount { get; set; }
        public DateTime PymentDate { get; set; }
        public int PurchaseId { get; set; }

        public clsSupplier SupplierInfo { get; private set; }
        public clsUsers UserInfo { get; private set; }

        public clsSupplierPyment(clsSupplier supplier, decimal amount, DateTime pymentDate, int purchaseId, clsUsers userInfo)
        {
            SupplierInfo = supplier;
            Amount = amount;
            PymentDate = pymentDate;
            PurchaseId = purchaseId;
            UserInfo = userInfo;
        }

        private clsSupplierPyment(int supplierPymentId, clsSupplier supplier, decimal amount, DateTime pymentDate, int purchaseId, clsUsers userInfo)
        {
            Id = supplierPymentId;
            SupplierInfo = supplier;
            Amount = amount;
            PymentDate = pymentDate;
            PurchaseId = purchaseId;
            UserInfo = userInfo;
        }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsSupplierPymentsData.AddAsync(Amount, SupplierInfo.Id, PymentDate, PurchaseId, UserInfo.Id);
            if (!Id.HasValue) return false;
            return await __UpdateAsync();
        }

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int supplierPaymentId) => await clsSupplierPymentsData.GetByIdAsync(supplierPaymentId);

        public static async Task<DataTable?> GetAllAsync() => await clsSupplierPymentsData.GetAllAsync();

        private async Task<bool> __UpdateAsync() => await clsSupplierPymentsData.UpdateAsync(Id, Amount, PymentDate, PurchaseId, UserInfo.Id) ;

        public static async Task<bool> DeleteAsync(int supplierPymentsId) => await clsSupplierPymentsData.DeleteAsync(supplierPymentsId);

        internal static clsSupplierPyment FetchSupplierPymentData(ref Dictionary<string, object> dict)
        {
            return new clsSupplierPyment(
                (int)dict["SupplierPymentID"],
                clsSupplier.FetchSupplierData(ref dict),
                (decimal)dict["Amount"],
                (DateTime)dict["PymentDate"],
                (int)dict["PurchaseID"],
                clsUsers.FetchUserData(ref dict)
            );
        }
    }
}
