using MyLib_DotNet.DatabaseExecutor;
using System.Data;

namespace DAL
{
    public static class clsProductData
    {

        public static async Task<DataTable?> GetAll()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllProducts", type: CommandType.StoredProcedure);
            
        }

        public static async Task< bool> Delete(int productID)
        {

            return await CRUD.DeleteAsync("SP_DeleteProduct", "ProductID", productID, CommandType.StoredProcedure);

        }

        public static bool Update(int productID)
        {
            return true;
        }

        public static DataTable Find(int productID)
        {
            return new DataTable();
        }

        public static int Add(int productID)
        {
            return 0;
        }



    }
}
