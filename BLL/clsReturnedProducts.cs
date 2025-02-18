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
    public class clsReturnedProducts : absClassesHelper
    {

        public int? ReturnedProductID { get; private set; }
        public int ProductID { get; set; }
        public DateTime DateOfReturned { get; set; }
        public int Quantity { get; set; }
        public int DetailID { get; set; }
        public int SupplierID { get; set; }
        public int UserID { get; set; }


        public clsReturnedProducts(int? returnedProductID, int productId, DateTime dateOfReturned, int quantity, int detailId, int supplierId, int userId)
        {
            this.ReturnedProductID = returnedProductID;
            this.ProductID = productId;
            this.DateOfReturned = dateOfReturned;
            this.Quantity = quantity;
            this.DetailID = detailId;
            this.SupplierID = supplierId;
            this.UserID = userId;

            _mode = enMode.Update;
        }

        public async Task<bool> Save()
        {
            return _mode switch
            {
                enMode.AddNew => await AddNew(),
                enMode.Update => await Update(),
                _ => false
            };
        }

        private async Task<bool> AddNew()
        {
            try
            {
                this.ReturnedProductID = await clsReturnedProductsData.AddNewReturnedProduct(ProductID, DateOfReturned, Quantity, DetailID, SupplierID, UserID);
                if (ReturnedProductID.HasValue)
                {
                    _mode = enMode.Update;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> Update()
        {
            try
            {
                return await clsReturnedProductsData.UpdateReturnedProduct(ReturnedProductID.Value, ProductID, DateOfReturned, Quantity, DetailID, SupplierID, UserID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> Delete(int returnedProductId)
        {
            return await clsReturnedProductsData.DeleteReturnedProductByIDAsync(returnedProductId);
        }

        public static async Task<clsReturnedProducts?> GetReturnedProductByID(int returnedProductId)
        {
            var data = await clsReturnedProductsData.GetReturnedProductByIDAsync(returnedProductId);
            if (data == null) return null;

            return new clsReturnedProducts(
                (int)data[0], (int)data[1], (DateTime)data[2], (int)data[3], (int)data[4], (int)data[5], (int)data[6]);
        }

        public static async Task<DataTable?> GetAllReturnedProducts()
        {
            return await clsReturnedProductsData.GetAllReturnedProductsAsync();
        }

        public static async Task<List<object[]>?> GetReturnedProductsByProductID(int productId)
        {
            return await clsReturnedProductsData.GetReturnedProductsByProductIDAsync(productId);
        }

        public static async Task<List<object[]>?> GetReturnedProductsByDetailID(int DetailID)
        {
            return await clsReturnedProductsData.GetReturnedProductsByDetailIDAsync(DetailID);
        }

        public static async Task<List<object[]>?> GetReturnedProductsBySupplierID(int SupplierID)
        {
            return await clsReturnedProductsData.GetReturnedProductsBySupplierIDAsync(SupplierID);
        }

    }
}
