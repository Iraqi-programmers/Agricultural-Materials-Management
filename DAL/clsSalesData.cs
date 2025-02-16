using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using Newtonsoft.Json;

namespace DAL
{
    public class clsSalesData
    {
        /*
        CREATE PROCEDURE SP_AddSaleWithDetails
            @Date DATETIME,
            @PersonID INT NULL,
            @UserID INT,
            @Details NVARCHAR(MAX) 
        AS
        BEGIN
            DECLARE @SaleID INT;
        
            BEGIN TRY
                BEGIN TRANSACTION;
        
                -- إدراج عملية البيع في الجدول
                IF @PersonID IS NOT NULL
                BEGIN
                    INSERT INTO Sales (SaleDate, PersonID, UserID)
                    VALUES (@Date, @PersonID, @UserID);
                END
                ELSE
                BEGIN
                    INSERT INTO Sales (SaleDate, UserID)
                    VALUES (@Date, @UserID);
                END
        
                SET @SaleID = SCOPE_IDENTITY(); 
        
                -- إدراج تفاصيل البيع
                INSERT INTO SaleDetails (SaleID, StockID, Quantity, WarrintyDate, Price, TotaCost)
                SELECT @SaleID, StockID, Quantity, WarrintyDate, Price, TotaCost
                FROM OPENJSON(@Details)
                WITH (
                    StockID INT,
                    Quantity INT,
                    WarrintyDate DATETIME, 
                    Price DECIMAL(10,2),
                    TotaCost DECIMAL(10,2)
                ) AS details;
        
                -- تحديث المخزون
                UPDATE Stock
                SET Quantity = s.Quantity - d.Quantity
                FROM Stock s
                INNER JOIN OPENJSON(@Details)
                WITH (
                    StockID INT,
                    Quantity INT
                ) AS d ON s.StockID = d.StockID;
        
                -- إنهاء المعاملة بنجاح
                COMMIT TRANSACTION;
        
                -- إرجاع معرف البيع
                SELECT @SaleID AS SaleID;
            END TRY
            BEGIN CATCH
                -- في حالة حدوث أي خطأ، يتم التراجع عن جميع العمليات
                IF @@TRANCOUNT > 0
                    ROLLBACK TRANSACTION;
        
                -- إرجاع NULL في حال الفشل
                SELECT NULL AS SaleID;
            END CATCH
        END;
         */
        public static async Task<int?> AddNewSaleWithDetailsAsync(DateTime date, List<Dictionary<string, object>> details, int? personId = null)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Date", date),
                new SqlParameter("@Details", JsonConvert.SerializeObject(details)),
                new SqlParameter("@PersonID", personId ?? (object)DBNull.Value),
                new SqlParameter("@UserID", clsUsersData.CurrentUserAuditInfo.UserId)
            };
            return await CRUD.AddAsync("SP_AddSaleWithDetails", parameters);
        }

        /*
        CREATE PROCEDURE SP_AddSaleWithDetailsAndAddNewPerson
            @Date DATETIME,
            @UserID INT,
            @Details NVARCHAR(MAX),
            @FullName NVARCHAR(255),
            @NationalNum INT,
            @PhoneNum NVARCHAR(50),
            @Address NVARCHAR(255)
        AS
        BEGIN
            DECLARE @SaleID INT;
            DECLARE @PersonID INT;
        
            BEGIN TRY
                BEGIN TRANSACTION;
        
                -- ندرج الشخص في الجدول 
                INSERT INTO Persons (FullName, NationalNum, PhoneNum, Address)
                VALUES (@FullName, @NationalNum, @PhoneNum, @Address);
        
                SET @PersonID = SCOPE_IDENTITY();
        
                -- ندرج عملية البيع في الجدول 
                INSERT INTO Sales (SaleDate, PersonID, UserID)
                VALUES (@Date, @PersonID, @UserID);
        
                SET @SaleID = SCOPE_IDENTITY();
        
                -- ندرج تفاصيل البيع
                INSERT INTO SaleDetails (SaleID, StockID, Quantity, WarrintyDate, Price, TotaCost)
                SELECT @SaleID, StockID, Quantity, WarrintyDate, Price, TotaCost
                FROM OPENJSON(@Details)
                WITH (
                    StockID INT,
                    Quantity INT,
                    WarrintyDate DATETIME,
                    Price DECIMAL(10,2),
                    TotaCost DECIMAL(10,2)
                ) AS details;
        
                -- نحدث بيانات المخزون
                UPDATE Stock
                SET Quantity = s.Quantity - d.Quantity
                FROM Stock s
                INNER JOIN OPENJSON(@Details)
                WITH (
                    StockID INT,
                    Quantity INT
                ) AS d ON s.StockID = d.StockID;
        
                -- ناكد المعاملة في حال النجاح
                COMMIT TRANSACTION;
        
                -- نرجع معرف البيع 
                SELECT @SaleID AS SaleID;
            END TRY
            BEGIN CATCH
                -- في حالة حدوث أي خطأ، يتم التراجع عن جميع العمليات
                IF @@TRANCOUNT > 0
                    ROLLBACK TRANSACTION;
        
                --  في حال الفشل نرجع نال
                SELECT NULL AS SaleID;
            END CATCH
        END;

         */
        public static async Task<int?> AddNewSaleWithDetailsAsync(DateTime date, List<Dictionary<string, object>> details, string fullName, string nationalNum, string phoneNum, string address)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Date", date),
                new SqlParameter("@Details", JsonConvert.SerializeObject(details)),
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@NationalNum", nationalNum),
                new SqlParameter("@PhoneNum", phoneNum),
                new SqlParameter("@Address", address),
                new SqlParameter("@UserID", clsUsersData.CurrentUserAuditInfo.UserId)
            };
            return await CRUD.AddAsync("SP_AddSaleWithDetailsAndAddNewPerson", parameters);
        }

        public static async Task<object[]?> GetSaleInfoByIDAsync(int saleID)
            => await CRUD.GetByColumnValueAsync("SP_", "SaleID", saleID, CommandType.StoredProcedure);

        public static async Task<List<object[]>?> GetAllSalesAsListAsync()
            => await CRUD.GetAllAsListAsync("SP_", type: CommandType.StoredProcedure);

        public static async Task<DataTable?> GetAllSalesAsDataTableAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_", type: CommandType.StoredProcedure);

        public static async Task<bool> IsSaleExistAsync(int saleID)
            => await CRUD.IsExistAsync("SP_", "SaleID", saleID, CommandType.StoredProcedure);

        public static async Task<bool> UpdateSaleDataAsync(int? saleID, DateTime date, int userID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SaleID", saleID),
                new SqlParameter("@Date", date),
                new SqlParameter("@UserID", clsUsersData.CurrentUserAuditInfo.UserId)
            };
            return await CRUD.UpdateAsync("SP_", parameters, CommandType.StoredProcedure);
        }

        /*
        CREATE PROCEDURE SP_UpdateSaleDetails
            @SaleID INT,
            @Details NVARCHAR(MAX) 
        AS
        BEGIN
            DECLARE @SaleDetailID INT;
            DECLARE @OldQuantity INT;
            DECLARE @NewQuantity INT;
            DECLARE @OldWarrintyDate DATETIME;
            DECLARE @NewWarrintyDate DATETIME;
            DECLARE @StockID INT;
            DECLARE @OldPrice DECIMAL(10,2);
            DECLARE @NewPrice DECIMAL(10,2);
            DECLARE @OldTotaCost DECIMAL(10,2);
            DECLARE @NewTotaCost DECIMAL(10,2);
            DECLARE @Result BIT = 1;
        
            BEGIN TRY
                BEGIN TRANSACTION;
        
                -- جدول مؤقت لتخزين البيانات الجديدة من JSON
                DECLARE @SaleDetails TABLE (SaleDetailID INT NULL, StockID INT, Quantity INT, WarrintyDate DATETIME, Price DECIMAL(10,2), TotaCost DECIMAL(10,2));
        
                INSERT INTO @SaleDetails (SaleDetailID, StockID, Quantity, WarrintyDate, Price, TotaCost)
                SELECT SaleDetailID, StockID, Quantity, WarrintyDate, Price, TotaCost
                FROM OPENJSON(@Details)
                WITH (
                    SaleDetailID INT,
                    StockID INT,
                    Quantity INT,
                    WarrintyDate DATETIME,
                    Price DECIMAL(10,2),
                    TotaCost DECIMAL(10,2)
                );
        
                -- التعامل مع المنتجات الموجودة بالفعل
                DECLARE product_cursor CURSOR FOR
                SELECT sd.SaleDetailID, sd.StockID, sd.Quantity, sd.WarrintyDate, sd.Price, sd.TotaCost
                FROM @SaleDetails sd
                WHERE sd.SaleDetailID IS NOT NULL; 
        
                OPEN product_cursor;
                FETCH NEXT FROM product_cursor INTO @SaleDetailID, @StockID, @NewQuantity, @NewWarrintyDate, @NewPrice, @NewTotaCost;
        
                WHILE @@FETCH_STATUS = 0
                BEGIN
                    -- جلب الكمية والسعر القديمة من SaleDetails
                    SELECT @OldQuantity = Quantity, @OldWarrintyDate = WarrintyDate, @OldPrice = Price, @OldTotaCost = TotaCost
                    FROM SaleDetails
                    WHERE SaleDetailID = @SaleDetailID;
        
                    IF @NewQuantity > @OldQuantity
                    BEGIN
                        -- نقلل من المخزن
                        UPDATE Stock
                        SET Quantity = Quantity - (@NewQuantity - @OldQuantity)
                        WHERE StockID = @StockID;
                    END
                    ELSE IF @NewQuantity < @OldQuantity
                    BEGIN
                        -- نرجع الزايد للمخزن
                        UPDATE Stock
                        SET Quantity = Quantity + (@OldQuantity - @NewQuantity)
                        WHERE StockID = @StockID;
                    END
        
                    -- تحديث تفاصيل البيع فقط إذا تغيرت أي قيمة
                    IF @NewPrice <> @OldPrice 
                    BEGIN
                        UPDATE SaleDetails
                        SET Price = @NewPrice
                        WHERE SaleDetailID = @SaleDetailID;
                    END
                    
                    IF @NewQuantity <> @OldQuantity 
                    BEGIN
                        UPDATE SaleDetails
                        SET Quantity = @NewQuantity
                        WHERE SaleDetailID = @SaleDetailID;
                    END
                    
                    IF @NewTotaCost <> @OldTotaCost
                    BEGIN
                        UPDATE SaleDetails
                        SET TotaCost = @NewTotaCost
                        WHERE SaleDetailID = @SaleDetailID;
                    END

                    IF @NewWarrintyDate <> @OldWarrintyDate 
                    BEGIN
                        UPDATE SaleDetails
                        SET WarrintyDate = @NewWarrintyDate
                        WHERE SaleDetailID = @SaleDetailID;
                    END
        
                    FETCH NEXT FROM product_cursor INTO @SaleDetailID, @StockID, @NewQuantityو @NewWarrintyDate, @NewPriceو @NewTotaCost;
                END
        
                CLOSE product_cursor;
                DEALLOCATE product_cursor;
        
                -- نضيف المنتجات الجديدة بالقائمة في حال توجد منتجات مضافة على القائمة
                INSERT INTO SaleDetails (SaleID, StockID, Quantity, WarrintyDate, Price, TotaCost)
                SELECT @SaleID, StockID, Quantity, WarrintyDate, Price, TotaCost
                FROM @SaleDetails
                WHERE SaleDetailID IS NULL;
        
                -- انقص المنتجات المضافة للقائمة من المخزن
                UPDATE Stock
                SET Quantity = s.Quantity - d.Quantity
                FROM Stock s
                INNER JOIN @SaleDetails d ON s.StockID = d.StockID
                WHERE d.SaleDetailID IS NULL; 
        
                -- نحذف المنتجات الي تم الغائها من القائمة
                DELETE FROM SaleDetails
                OUTPUT deleted.StockID, deleted.Quantity
                INTO #DeletedStock (StockID, Quantity)
                WHERE SaleDetailID NOT IN (SELECT SaleDetailID FROM @SaleDetails)
                AND SaleID = @SaleID;
        
                -- نرجع الكمية المحذوفة للمخزن
                UPDATE Stock
                SET Quantity = s.Quantity + d.Quantity
                FROM Stock s
                INNER JOIN #DeletedStock d ON s.StockID = d.StockID;
        
                DROP TABLE #DeletedStock;
        
                -- إنهاء المعاملة بنجاح
                COMMIT TRANSACTION;
        
                -- إرجاع النجاح
                SELECT @Result = 1;
            END TRY
            BEGIN CATCH
                -- في حالة حدوث أي خطأ، يتم التراجع عن جميع العمليات
                IF @@TRANCOUNT > 0
                    ROLLBACK TRANSACTION;
        
                -- إرجاع الفشل
                SELECT @Result = 0;
            END CATCH;
        
            -- إرجاع النتيجة
            SELECT @Result AS Success;
        END;

         */
        public static async Task<bool> UpdateSaleDetailsAsync(int saleID, List<Dictionary<string, object>> details)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@SaleID", saleID),
                new SqlParameter("@Details", JsonConvert.SerializeObject(details)),
                new SqlParameter("@UserID", clsUsersData.CurrentUserAuditInfo.UserId)
            };

            return await CRUD.UpdateAsync("SP_UpdateSaleDetails", parameters, CommandType.StoredProcedure);
        }

        public static async Task<bool> DeleteSaleByIDAsync(int saleID)
            => await CRUD.DeleteAsync("SP_", "SaleID", saleID, clsUsersData.CurrentUserAuditInfo, CommandType.StoredProcedure);

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
        public static async Task<bool> DeleteSaleDetailByIDAsync(int saleDetailID, bool returnToStock = false)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@SaleDetailID", saleDetailID),
                new SqlParameter("@ReturnToStock", returnToStock)
            };

            return await CRUD.DeleteAsync("SP_DeleteSaleDetail", parameters, clsUsersData.CurrentUserAuditInfo, CommandType.StoredProcedure);
        }

        public static async Task<bool> DeleteMultipleSalesAsync(List<int> saleIDs)
            => await CRUD.DeleteRecordsByIdsAsync("SP_", "Sales", "SaleID", 0, saleIDs, clsUsersData.CurrentUserAuditInfo, CommandType.StoredProcedure);
    }
}
