using System.Data;
using DAL;

namespace BLL
{
    public class clsSupplierPayment : absTransaction
    {
        public double PaidAmount { get; set; }
        public int PurchaseId { get; set; }
        public clsSupplier SupplierInfo { get; private set; }

        public clsSupplierPayment(clsSupplier supplier, double amount, DateTime pymentDate, int purchaseId, clsUsers userInfo)
        {
            SupplierInfo = supplier;
            PaidAmount = amount;
            Date = pymentDate;
            PurchaseId = purchaseId;
            UserInfo = userInfo;
        }

        private clsSupplierPayment(int supplierPymentId, clsSupplier supplier, double amount, DateTime pymentDate, int purchaseId, clsUsers userInfo)
        {
            Id = supplierPymentId;
            SupplierInfo = supplier;
            PaidAmount = amount;
            Date = pymentDate;
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
            Id = await clsSupplierPymentsData.AddAsync(PaidAmount, SupplierInfo.Id, Date, PurchaseId, UserInfo?.Id);
            if (!Id.HasValue) return false;
            return await __UpdateAsync();
        }

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int supplierPaymentId) => await clsSupplierPymentsData.GetByIdAsync(supplierPaymentId);

        public static async Task<DataTable?> GetAllAsync() => await clsSupplierPymentsData.GetAllAsync();

        private async Task<bool> __UpdateAsync() => await clsSupplierPymentsData.UpdateAsync(Id, PaidAmount, Date, PurchaseId, UserInfo?.Id) ;

        public static async Task<bool> DeleteAsync(int supplierPymentsId) => await clsSupplierPymentsData.DeleteAsync(supplierPymentsId);

        internal static clsSupplierPayment FetchSupplierPymentData(ref Dictionary<string, object> dict)
        {
            return new clsSupplierPayment(
                (int)dict["SupplierPymentID"],
                clsSupplier.FetchSupplierData(ref dict),
                (double)dict["Amount"],
                (DateTime)dict["PymentDate"],
                (int)dict["PurchaseID"],
                clsUsers.FetchUserData(ref dict)
            );
        }
    }
}
