using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Product
{
    public static class clsProductDetailsData
    {
        public static async Task<DataTable?> GetAllAsyncAsDataTable()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllProductDetails");
        }

        public static async Task<int?> AddAsync(int SizeID, int ThicknessID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SizeID", SizeID),
                new SqlParameter("@ThickenssID", ThicknessID)
            };
            return  await CRUD.AddAsync("SP_AddDetails", parameters);
        }

        public static async Task<bool?> DeleteAsync(int DetailID)
        {
            return await CRUD.DeleteAsync("SP_DeleteDetails", "ProductDetailID", DetailID);
        }

        
    }
}
