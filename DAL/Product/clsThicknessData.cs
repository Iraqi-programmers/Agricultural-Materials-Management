using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System.Data;
//yousif
namespace DAL.Product
{
    public static class clsThicknessData
    {
        public static async Task<DataTable?> getAllAsDataTableAsync()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetAllThickness");

        }
        public static async Task<bool> deleteAsync(int ThicknessID)
        {

            return await CRUD.DeleteAsync("SP_DeleteThickness", "ThicknessID", ThicknessID);

        }

        public static async Task<object[]?> findByIDAsync(int ThicknessID)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetThicknessByID", "ThicknessID", ThicknessID);
        }

        public static async Task<int?> addAsync(double Thickness)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@Thickness", Thickness)
            };
            return await CRUD.AddAsync("SP_AddThickness", sqlParameter);
        }
        public static async Task<bool> updateAsync(int? ThicknessID, double Thickness)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ThicknessID", ThicknessID),
                new SqlParameter("@Thickness", Thickness),
            };
            return await CRUD.UpdateAsync("SP_UpdateThickness", parameters);
        }

    }
}
