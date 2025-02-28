using System.Data;
using DAL;

namespace BLL
{
    public class clsSupplier : absClassesHelperBasc
    {
        public string SupplierName { get; set; } = "";
        public string Phone { get; set; } = "";
        public bool IsPerson { get; set; } = false;
        public string Address { get; set; } = "";

        public clsSupplier(string supplierName, string phone, bool isPerson, string address)
        {
            SupplierName = supplierName;
            Phone = phone;
            IsPerson = isPerson;
            Address = address;
        }

        private clsSupplier(int supplierId, string supplierName, string phone, bool isPerson, string address)
        {
            Id = supplierId;
            SupplierName = supplierName;
            Phone = phone;
            IsPerson = isPerson;
            Address = address;
        }

        internal clsSupplier() { }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsSupplierData.AddAsync(SupplierName, Phone, IsPerson, Address);
            return Id.HasValue;
        }

        public static async Task<clsSupplier?> GetByIdAsync(int supplierId)
        {
            var data = await clsSupplierData.GetByIdAsync(supplierId);
            if (data == null) return null;
            return FetchSupplierData(ref data);
        }
        public static async Task<clsSupplier?> GetByNameAsync(string supplierName)
        {
            var data = await clsSupplierData.GetByNameAsync(supplierName);
            if (data == null) return null;
            return FetchSupplierData(ref data);
        }
        public static async Task<clsSupplier?> GetByPhoneAsync(string phone)
        {
            var data = await clsSupplierData.GetByPhoneAsync(phone);
            if (data == null) return null;
            return FetchSupplierData(ref data);
        }

        public static async Task<DataTable?> GetAllSuppliersAsync() => await clsSupplierData.GetAllAsync();

        private async Task<bool> __UpdateAsync() => await clsSupplierData.UpdateAsync(Id, SupplierName, Phone, IsPerson, Address);

        public static async Task<bool> DeleteByIdAsync(int? supplierId) => await clsSupplierData.DeleteAsync(supplierId);

        public async Task<bool> DeleteByIdAsync() => await DeleteByIdAsync(Id);

        internal static clsSupplier FetchSupplierData(ref Dictionary<string, object> dict)
        {
            return new clsSupplier(
                (int)dict["SupplierID"],
                (string)dict["SupplierName"],
                (string)dict["Phone"],
                (bool)dict["IsPerson"],
                (string)dict["Address"]
            );
        }
    }
}
