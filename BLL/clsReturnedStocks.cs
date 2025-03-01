using BLL.Product;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //Create By Abu Sanad
    public class clsReturnedStocks : absClassesHelperBasc
    {
        public int Quantity { get; set; }
        public clsSalesDetails SalesInfo { get; set; }
        public clsSupplier SupplierInfo { get; set; }
        public clsUsers UserInfo { get; set; }
        
        public clsReturnedStocks(int quantity, clsSalesDetails salesDetal, clsSupplier supplier, clsUsers users)
        {
            this.Quantity = quantity;
            this.SalesInfo = salesDetal;
            this.SupplierInfo = supplier;
            this.UserInfo = users;
        }

        internal clsReturnedStocks(int returnedStocks,int quantity, clsSalesDetails salesDetal, clsSupplier supplier, clsUsers users)
        {
            Id = returnedStocks;
            this.Quantity = quantity;
            this.SalesInfo = salesDetal;
            this.SupplierInfo = supplier;
            this.UserInfo = users;
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsReturnedStocksData.AddNew(SalesInfo.Id, Quantity, SupplierInfo.Id, UserInfo.Id);
            return Id != null;
        }

        public async Task<bool> __UpdateAsync()  => await clsReturnedStocksData.Update(SalesInfo.Id, Quantity, SupplierInfo.Id, UserInfo.Id);
           
        public static async Task<bool> DeleteAsync(int returnedStocksId) => await clsReturnedStocksData.Delete(returnedStocksId);

        public static async Task<clsReturnedStocks?> GetByIdAsync(int ReturnedStocksID)
        {
            var data = await clsReturnedStocksData.GetByIDAsync(ReturnedStocksID);
            if (data == null) return null;
            return FetchReturnedStocksData(ref data);
        }

        public static async Task<DataTable?> GetAll() => await clsReturnedStocksData.GetAllAsync();
        
        //public static async Task<List<object[]>?> GetReturnedProductsByProductID(int productId)
        //{
        //    return await clsReturnedStocksData.GetReturnedProductsByProductIDAsync(productId);
        //}

        //public static async Task<List<object[]>?> GetReturnedProductsByDetailID(int DetailID)
        //{
        //    return await clsReturnedStocksData.GetReturnedProductsByDetailIDAsync(DetailID);
        //}

        //public static async Task<List<object[]>?> GetReturnedProductsBySupplierID(int SupplierID)
        //{
        //    return await clsReturnedStocksData.GetReturnedProductsBySupplierIDAsync(SupplierID);
        //}

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
