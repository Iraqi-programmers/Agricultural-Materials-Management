using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsPurchasesDetailsData
    {
        public static async Task<int?> AddPurchaseDetail(int PurchaseID,int ProductID,decimal Price,string Status,int Quantity,DateTime Warrnty)
        {
            SqlParameter[] Parameters =
           {
                  new SqlParameter("@",PurchaseID),
                new SqlParameter("@",ProductID),
                new SqlParameter("@",Price),
                new SqlParameter("@", Status),
                new SqlParameter("@", Quantity),
                new SqlParameter("@", Warrnty)
            };

            return await CRUD.AddAsync("", Parameters);

        }

        public static async Task<bool> UpdatePurchaseDetail(int PurchesDetailID, decimal Price, string Status, int Quantity, DateTime Warrnty)
        {
            SqlParameter[] Parameters =
           {
                new SqlParameter("@",PurchesDetailID),
                new SqlParameter("@",Price),
                new SqlParameter("@", Status),
                new SqlParameter("@", Quantity),
                new SqlParameter("@", Warrnty)
            };

            return await CRUD.UpdateAsync("", Parameters);

        }

        public static async Task<DataTable?> GetAllPurchaseDetail()
        {
            return await CRUD.GetAllAsDataTableAsync("", null);
        }

        public static async Task<bool> DeletePurchaseDetils(int PurchaseID)
        {
            return await CRUD.DeleteAsync("", "", PurchaseID);
        }

        public static async Task<object[]?> GetPurchaseDetialByPurchaseID(int ID)
        {
            return await CRUD.GetByColumnValueAsync("", "@", ID);
        }

        //اضافه عملية جلب مشتريات بواسطة اسم منتج مطلوب عمله
        //البحث عن المشتريات التي تحتوي على ضمان غير منتهي والعكس
        //البحث عن عمليات الشراء حسب الحالة 

    }
}
