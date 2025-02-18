using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsSalesDetailsData
    {
        public static async Task<int?> AddNewSaleDetailAsync(int stockId, uint quantity, DateTime warrantyDate, float price, float totaCost, int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@StockID", stockId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@WarrntyDate", warrantyDate),
                new SqlParameter("@Price", price),
                new SqlParameter("@TotaCost", totaCost),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.AddAsync("SP_", parameters);
        }

        public static async Task<object[]?> GetSaleDetailInfoByIDAsync(int detailId)
            => await CRUD.GetByColumnValueAsync("SP_", "DetailID", detailId);

        public static async Task<List<object[]>?> GetAllSaleDetailsAsListAsync()
            => await CRUD.GetAllAsListAsync("SP_");

        public static async Task<DataTable?> GetAllSaleDetailsAsDataTableAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_");

        public static async Task<bool> IsSaleDetailExistAsync(int detailId)
            => await CRUD.IsExistAsync("SP_", "DetailID", detailId);

        //public static async Task<bool> UpdateSaleDetailDataAsync(int? detailId, int stockId, int quantity, DateTime warrantyDate, float price, float totaCost, int userId)
        //{
        //    SqlParameter[] parameters =
        //    {
        //        new SqlParameter("@DetailID", detailId),
        //        new SqlParameter("@StockID", stockId),
        //        new SqlParameter("@Quantity", quantity),
        //        new SqlParameter("@WarrntyDate", warrantyDate),
        //        new SqlParameter("@Price", price),
        //        new SqlParameter("@TotaCost", totaCost),
        //        new SqlParameter("@UserID", userId)
        //    };
        //    return await CRUD.UpdateAsync("SP_", parameters);
        //}

        //public static async Task<bool> DeleteSaleDetailByIDAsync(int detailId, int userId)
        //    => await CRUD.DeleteAsync("SP_", "DetailID", detailId, "UserID", userId);

        /*
        CREATE PROCEDURE SP_DeleteSaleDetail
            @SaleDetailID INT,
            @ReturnToStock BIT
        AS
        BEGIN
            DECLARE @StockID INT;
            DECLARE @Quantity INT;
            DECLARE @Result BIT = 0;
        
            BEGIN TRY
                BEGIN TRANSACTION;
        
                -- نجيب الستوك ايدي والكمية قبل الحذف
                SELECT @StockID = StockID, @Quantity = Quantity
                FROM SaleDetails
                WHERE SaleDetailID = @SaleDetailID;
        
                -- نرجع الكمية للمخزن
                UPDATE Stock
                SET Quantity = Quantity + @Quantity
                WHERE StockID = @StockID;
        
                -- إرجاع الكمية إلى المخزن إذا كان مطلوبًا
                IF @ReturnToStock = 1
                BEGIN
                    UPDATE Stock
                    SET Quantity = Quantity + @Quantity
                    WHERE StockID = @StockID;
                END

                DELETE FROM SaleDetails WHERE SaleDetailID = @SaleDetailID;
        
                COMMIT TRANSACTION;
                SET @Result = 1;
            END TRY
            BEGIN CATCH
                -- التراجع عن المعاملة في حالة حدوث خطأ
                IF @@TRANCOUNT > 0
                    ROLLBACK TRANSACTION;
        
                SET @Result = 0;
            END CATCH;
        
            -- نرجع النتيجة
            SELECT @Result = 0;
        END;

         */
        public static async Task<bool> DeleteSaleDetailByIDAsync(int saleDetailId, int userId, bool returnToStock = false)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@SaleDetailID", saleDetailId),
                new SqlParameter("@ReturnToStock", returnToStock),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.DeleteAsync("SP_DeleteSaleDetail", parameters);
        }

        // نحتاج ننشئ ستورد بروسيجر تستقبل باراميتر بوليان ارجاع الكميات للمخزن او لا بعد الحذف
        public static async Task<bool> DeleteMultipleSaleDetailsAsync(List<int> detailIDs, int userId, bool returnToStock = false)
            => await CRUD.DeleteRecordsByIdsAsync("SP_", "SalesDetails", "DetailID", 0, detailIDs, "UserID", userId);
    }
}
