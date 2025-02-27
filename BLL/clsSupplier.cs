using System.Data;
using DAL;

namespace BLL
{
    public class clsSupplier : absClassesHelperBasc
    {
        public string SupplierName { get; set; }
        public string Phone { get; set; }
        public bool IsPerson { get; set; }
        public string Address { get; set; }

        public clsSupplier(string supplierName, string phone, bool isPerson, string address)
        {
            SupplierName = supplierName;
            Phone = phone;
            IsPerson = isPerson;
            Address = address;
        }

        public clsSupplier(int supplierId, string supplierName, string phone, bool isPerson, string address)
        {
            Id = supplierId;
            SupplierName = supplierName;
            Phone = phone;
            IsPerson = isPerson;
            Address = address;
        }

        public async Task<int?> AddAsync()
        {
            Id = await clsSupplierData.AddAsync(SupplierName, Phone, IsPerson, Address);
            return Id;
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

        public async Task<bool> UpdateAsync() => await clsSupplierData.UpdateAsync(Id, SupplierName, Phone, IsPerson, Address);

        public static async Task<bool> DeleteByIdAsync(int? supplierId) => await clsSupplierData.DeleteAsync(supplierId);

        public async Task<bool> DeleteByIdAsync() => await DeleteByIdAsync(Id);

        public static clsSupplier FetchSupplierData(ref Dictionary<string, object> dict)
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
