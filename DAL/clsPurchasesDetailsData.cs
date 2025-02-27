using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsPurchasesDetailsData
    {
        public static async Task<int?> AddAsync(int purchaseId, int productId, decimal price, string status, int quantity, DateTime warrantyDate, int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PurchaseID", purchaseId),
                new SqlParameter("@ProductID", productId),
                new SqlParameter("@Price", price),
                new SqlParameter("@Status", status),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@WarrantyDate", warrantyDate),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.AddAsync("SP_", parameters);
        }

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllPurchaseDetails");
        
        public static async Task<List<Dictionary<string, object>>?> GetByPurchaseIdAsync(int purchaseId)
            => await CRUD.GetAllAsListAsync("SP_", new SqlParameter[] { new SqlParameter("@PurchaseID", purchaseId) });

        public static async Task<List<Dictionary<string, object>>?> GetByProductNameAsync(string productName)
            => await CRUD.GetAllAsListAsync("SP_", new SqlParameter[] { new SqlParameter("@ProductName", productName) });

        public static async Task<bool> UpdateAsync(int purchaseDetailId, decimal price, string status, int quantity, DateTime warrantyDate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PurchaseDetailID", purchaseDetailId),
                new SqlParameter("@Price", price),
                new SqlParameter("@Status", status),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@WarrantyDate", warrantyDate)
            };
            return await CRUD.UpdateAsync("SP_", parameters);
        }

        public static async Task<bool> DeleteAsync(int purchaseId) => await CRUD.DeleteAsync("SP_", "@PurchaseID", purchaseId);
}
    

    //public class clsPurchasesDetailsData
    //{
    //    public static async Task<int?> AddAsync(int PurchaseID,int ProductID,decimal Price,string Status,int Quantity,DateTime Warrnty)
    //    {
    //        SqlParameter[] Parameters =
    //        {
    //            new SqlParameter("@",PurchaseID),
    //            new SqlParameter("@",ProductID),
    //            new SqlParameter("@",Price),
    //            new SqlParameter("@", Status),
    //            new SqlParameter("@", Quantity),
    //            new SqlParameter("@", Warrnty)
    //        };

    //        return await CRUD.AddAsync("", Parameters);

    //    }

    //    public static async Task<bool> UpdateAsync(int PurchesDetailID, decimal Price, string Status, int Quantity, DateTime Warrnty)
    //    {
    //        SqlParameter[] Parameters =
    //       {
    //            new SqlParameter("@",PurchesDetailID),
    //            new SqlParameter("@",Price),
    //            new SqlParameter("@", Status),
    //            new SqlParameter("@", Quantity),
    //            new SqlParameter("@", Warrnty)
    //        };

    //        return await CRUD.UpdateAsync("", Parameters);

    //    }

    //    public static async Task<DataTable?> GetAllAsync()
    //    {
    //        return await CRUD.GetAllAsDataTableAsync("", null);
    //    }

    //    public static async Task<bool> DeletePurchaseDetils(int PurchaseID)
    //    {
    //        return await CRUD.DeleteAsync("", "", PurchaseID);
    //    }

    //    public static async Task<object[]?> GetPurchaseDetialByPurchaseID(int ID)
    //    {
    //        return await CRUD.GetByColumnValueAsync("", "@", ID);
    //    }

    //    //اضافه عملية جلب مشتريات بواسطة اسم منتج مطلوب عمله
    //    //البحث عن المشتريات التي تحتوي على ضمان غير منتهي والعكس
    //    //البحث عن عمليات الشراء حسب الحالة 

    //}
}
