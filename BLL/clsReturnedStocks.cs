using System.Data;
using DAL;

namespace BLL
{
    public class clsReturnedStocks : absBaseEntity
    {
        public int Quantity { get; set; }
        public clsSalesDetails SalesInfo { get; set; }
        public clsSupplier SupplierInfo { get; set; }
        public clsUsers UserInfo { get; set; }
        
        public clsReturnedStocks(int quantity, clsSalesDetails salesDetal, clsSupplier supplier, clsUsers users)
        {
            Quantity = quantity;
            SalesInfo = salesDetal;
            SupplierInfo = supplier;
            UserInfo = users;
        }

        internal clsReturnedStocks(int returnedStocks,int quantity, clsSalesDetails salesDetal, clsSupplier supplier, clsUsers users)
        {
            Id = returnedStocks;
            Quantity = quantity;
            SalesInfo = salesDetal;
            SupplierInfo = supplier;
            UserInfo = users;
        }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsReturnedStocksData.AddNew(SalesInfo.Id, Quantity, SupplierInfo.Id, UserInfo.Id);
            return Id.HasValue;
        }

        public async Task<bool> __UpdateAsync()  => await clsReturnedStocksData.Update(Id,SalesInfo.Id, Quantity, SupplierInfo.Id, UserInfo.Id);
           
        public static async Task<bool> DeleteAsync(int returnedStocksId) => await clsReturnedStocksData.Delete(returnedStocksId);

        public static async Task<clsReturnedStocks?> GetByIdAsync(int ReturnedStocksID)
        {
            var data = await clsReturnedStocksData.GetByIdAsync(ReturnedStocksID);
            if (data == null) return null;
            return FetchReturnedStocksData(ref data);
        }

        public static async Task<DataTable?> GetAll() => await clsReturnedStocksData.GetAllAsync();
        
        internal static clsReturnedStocks FetchReturnedStocksData(ref Dictionary<string, object> dict)
        {
            return new clsReturnedStocks(
                (int)dict["ReturndStockID"],
                (int)dict["quantity"],
                clsSalesDetails.FetchSalesDetailsData(ref dict),
                clsSupplier.FetchSupplierData(ref dict),
                clsUsers.FetchUserData(ref dict)
            );
        }
    }
}
