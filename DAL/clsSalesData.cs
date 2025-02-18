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
            @UserID INT,
            @Details NVARCHAR(MAX),
            @SaleTotalCost DOUBLE(10,2),
            @PersonID INT = NULL,
            @FullName NVARCHAR(255) = NULL,
            @NationalNum NVARCHAR(50) = NULL,
            @PhoneNumber NVARCHAR(50) = NULL,
            @Address NVARCHAR(255) = NULL,
            @SaleID INT OUTPUT
        AS
        BEGIN
            SET NOCOUNT ON;
            DECLARE @NewPersonID INT = NULL;
            DECLARE @TransactionSuccess BIT = 1;
    
            BEGIN TRANSACTION;
    
            -- في حال مررنة ايدي شخص نضيفة مباشرة
            IF @PersonID IS NOT NULL
            BEGIN
                SET @NewPersonID = @PersonID;
            END
            ELSE
            BEGIN
                -- في حال مررنة بيانات شخص
                IF @NationalNum IS NOT NULL OR @FullName IS NOT NULL
                BEGIN
                    -- نجيك بالرقم الوطني اذا ضايفيه قبل او لا
                    IF @NationalNum IS NOT NULL
                    BEGIN
                        SELECT @NewPersonID = PersonID FROM Persons WHERE NationalNum = @NationalNum;
                    END
    
                    -- اذا ما لكينة بالرقم الوطني نبحث عن اسمه او رقمه
                    IF @NewPersonID IS NULL 
                    BEGIN
                        SELECT @NewPersonID = PersonID FROM Persons 
                        WHERE 
                            (@PhoneNumber IS NOT NULL AND PhoneNumber = @PhoneNumber)
                            OR (@FullName IS NOT NULL AND FullName = @FullName));
                    END
    
                    -- نضيفه في حال لم نجده في الجدول
                    IF @NewPersonID IS NULL 
                    BEGIN
                        INSERT INTO Persons (FullName, NationalNum, PhoneNumber, Address)
                        VALUES (@FullName, @NationalNum, @PhoneNumber, @Address);

                        SET @NewPersonID = SCOPE_IDENTITY();
                    END
                END
            END;

            -- نسجل عملية البيع ونحدد التاريخ والوقت
            INSERT INTO Sales (SaleDate, PersonID, UserID, SaleTotalCost)
            VALUES (GETDATE(), @NewPersonID, @UserID, @SaleTotalCost);
    
            SET @SaleID = SCOPE_IDENTITY();
    
            IF @SaleID IS NULL
            BEGIN
                SET @TransactionSuccess = 0;
                GOTO RollbackTransaction;
            END
    
            -- نسجل تفاصيل البيع
            INSERT INTO SalesDetails (SaleID, StockID, WarrantyDate, Price, Quantity, TotalCost)
            SELECT @SaleID, StockID, WarrantyDate, Price, Quantity, TotalCost
            FROM OPENJSON(@Details)
            WITH (
                StockID INT,
                Quantity INT,
                WarrantyDate DATETIME,
                Price DECIMAL(10,2),
                TotalCost DECIMAL(10,2)
            );
    
            -- نحدث المخزن المخزون
            UPDATE Stock
            SET Quantity = s.Quantity - d.Quantity
            FROM Stock s
            INNER JOIN OPENJSON(@Details)
            WITH (
                StockID INT,
                Quantity INT
            ) AS d ON s.StockID = d.StockID;
    
            IF @@ERROR <> 0
            BEGIN
                SET @TransactionSuccess = 0;
                GOTO RollbackTransaction;
            END
    
            COMMIT TRANSACTION;
            SELECT @SaleID AS SaleID;
    
        RollbackTransaction:
            ROLLBACK TRANSACTION;
            SELECT @SaleID = NULL;
        END;


         */
        public static async Task<int?> AddSaleWithDetailsAsync(List<object[]>? details, int userId, double saleTotalCost, int? personId, string? fullName, string? nationalNum, string? phoneNum, string? address)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Details", JsonConvert.SerializeObject(details)),
                new SqlParameter("@UserID", userId)
            };
            if (personId.HasValue) parameters.Append(new SqlParameter("@PersonID", personId));
            if (!string.IsNullOrWhiteSpace(fullName)) parameters.Append(new SqlParameter("@FullName", fullName));
            if (!string.IsNullOrWhiteSpace(nationalNum)) parameters.Append(new SqlParameter("@NationalNum", nationalNum));
            if (!string.IsNullOrWhiteSpace(phoneNum)) parameters.Append(new SqlParameter("@PhoneNumber", phoneNum));
            if (!string.IsNullOrWhiteSpace(address)) parameters.Append(new SqlParameter("@Address", address));
            return await CRUD.AddAsync("SP_AddSaleWithDetails", parameters);
        }

        public static async Task<object[]?> GetSaleInfoByIDAsync(int saleId)
            => await CRUD.GetByColumnValueAsync("SP_", "SaleID", saleId);

        public static async Task<List<object[]>?> GetAllSalesAsListAsync()
            => await CRUD.GetAllAsListAsync("SP_");

        public static async Task<DataTable?> GetAllSalesAsDataTableAsync()
            => await CRUD.GetAllAsDataTableAsync("SP_");

        public static async Task<bool> IsSaleExistAsync(int saleId)
            => await CRUD.IsExistAsync("SP_", "SaleID", saleId);

        /*
        CREATE PROCEDURE SP_UpdateSaleDetails
            @SaleID INT,
            @SaleTotalCost DOUBLE(10,2),
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

                -- نحدث المبلغ الكلي للقائمة ككل
                UPDATE Sales
                SET SaleTotalCost = SaleTotalCost
        
                -- جدول مؤقت لتخزين البيانات الجديدة من جيسن
                DECLARE @SaleDetails TABLE (SaleDetailID INT NULL, StockID INT, WarrintyDate DATETIME, Price DECIMAL(10,2), Quantity INT, TotaCost DECIMAL(10,2));
        
                INSERT INTO @SaleDetails (SaleDetailID, StockID, WarrintyDate, Price, Quantity, TotaCost)
                SELECT SaleDetailID, StockID, WarrintyDate, Price, Quantity, TotaCost
                FROM OPENJSON(@Details)
                WITH (
                    SaleDetailID INT,
                    StockID INT,
                    WarrintyDate DATETIME,
                    Price DECIMAL(10,2),
                    Quantity INT,
                    TotaCost DECIMAL(10,2)
                );
        
                -- التعامل مع المنتجات الموجودة بالفعل
                DECLARE product_cursor CURSOR FOR
                SELECT sd.SaleDetailID, sd.StockID, sd.WarrintyDate, sd.Price, sd.Quantity, sd.TotaCost
                FROM @SaleDetails sd
                WHERE sd.SaleDetailID IS NOT NULL; 
        
                OPEN product_cursor;
                FETCH NEXT FROM product_cursor INTO @SaleDetailID, @StockID, @NewWarrintyDate, @NewPrice, @NewQuantity, @NewTotaCost;
        
                WHILE @@FETCH_STATUS = 0
                BEGIN
                    -- جلب الكمية والسعر القديمة من SaleDetails
                    SELECT @OldWarrintyDate = WarrintyDate, @OldPrice = Price, @OldQuantity = Quantity, @OldTotaCost = TotaCost FROM SaleDetails
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
        
                    FETCH NEXT FROM product_cursor INTO @SaleDetailID, @StockID, @NewWarrintyDate, @NewPrice, @NewQuantity, @NewTotaCost;
                END
        
                CLOSE product_cursor;
                DEALLOCATE product_cursor;
        
                -- نضيف المنتجات الجديدة بالقائمة في حال توجد منتجات مضافة على القائمة
                INSERT INTO SaleDetails (SaleID, StockID, WarrintyDate, Price, Quantity, TotaCost)
                SELECT @SaleID, StockID, WarrintyDate, Price, Quantity, TotaCost FROM @SaleDetails
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
            SELECT @Result;
        END;

         */
        public static async Task<bool> UpdateSaleDetailsAsync(int saleId, List<object[]>? details, double SaleTotalCost)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Details", JsonConvert.SerializeObject(details)),
                new SqlParameter("@SaleTotalCost", SaleTotalCost),
                new SqlParameter("@SaleID", saleId)
            };
            return await CRUD.UpdateAsync("SP_UpdateSaleDetails", parameters);
        }

        // لازم نحذف السلس ديتيلز الي مرتبطه بيها ونرجع الكميات للمخزن اذا مررنة ترو
        /*
        CREATE PROCEDURE SP_DeleteSaleByID
            @SaleID INT,
            @UserID INT,
            @returnToStock BIT
        AS
        BEGIN
            SET NOCOUNT ON;

            DECLARE @TransactionSuccess BIT = 1;

            BEGIN TRY
                BEGIN TRANSACTION;

                -- التحقق من وجود البيع
                IF NOT EXISTS (SELECT 1 FROM Sales WHERE SaleID = @SaleID)
                BEGIN
                    SET @TransactionSuccess = 0;
                    GOTO RollbackTransaction;
                END

                -- استرجاع الكميات للمخزون اذا مررنة ترو
                IF @returnToStock = 1
                BEGIN
                    UPDATE Stock
                    SET Quantity = s.Quantity + d.Quantity
                    FROM Stock s
                    INNER JOIN SalesDetails d ON s.StockID = d.StockID
                    WHERE d.SaleID = @SaleID;
                END

                -- حذف تفاصيل البيع
                DELETE FROM SalesDetails WHERE SaleID = @SaleID;

                -- حذف عملية البيع
                DELETE FROM Sales WHERE SaleID = @SaleID;

                COMMIT TRANSACTION;
                SELECT 1 AS Success; 
                RETURN;

        RollbackTransaction:
                ROLLBACK TRANSACTION;
                SELECT 0 AS Success; 
            END TRY
            BEGIN CATCH
                IF @@TRANCOUNT > 0
                    ROLLBACK TRANSACTION;

                SELECT 0 AS Success; 
            END CATCH;
        END;
         */
        public static async Task<bool> DeleteSaleByIDAsync(int saleId, int userId, bool returnToStock = false)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@SaleID", saleId),
                new SqlParameter("@returnToStock", returnToStock)
            };
            return await CRUD.DeleteAsync("SP_DeleteSaleByID", parameters);
        }
        /*
         CREATE PROCEDURE SP_DeleteMultipleSales
            @SaleIDs NVARCHAR(MAX),  -- قائمة معرفات البيع بصيغة JSON
            @UserID INT,
            @returnToStock BIT
        AS
        BEGIN
            SET NOCOUNT ON;

            BEGIN TRY
                BEGIN TRANSACTION;

                -- جدول مؤقت لتخزين معرفات المبيعات
                DECLARE @SalesToDelete TABLE (SaleID INT);

                -- إدخال البيانات من JSON إلى الجدول المؤقت
                INSERT INTO @SalesToDelete (SaleID)
                SELECT value FROM OPENJSON(@SaleIDs);

                -- التحقق من وجود عمليات البيع
                IF NOT EXISTS (SELECT 1 FROM Sales WHERE SaleID IN (SELECT SaleID FROM @SalesToDelete))
                BEGIN
                    ROLLBACK TRANSACTION;
                    SELECT 0 AS Success; -- إرجاع False إذا لم يتم العثور على أي عمليات بيع
                    RETURN;
                END

                -- إرجاع الكميات إلى المخزون إذا كان @returnToStock = 1
                IF @returnToStock = 1
                BEGIN
                    UPDATE Stock
                    SET Quantity = s.Quantity + d.Quantity
                    FROM Stock s
                    INNER JOIN SalesDetails d ON s.StockID = d.StockID
                    INNER JOIN @SalesToDelete t ON d.SaleID = t.SaleID;
                END

                -- حذف تفاصيل المبيعات المرتبطة
                DELETE FROM SalesDetails WHERE SaleID IN (SELECT SaleID FROM @SalesToDelete);

                -- حذف عمليات البيع
                DELETE FROM Sales WHERE SaleID IN (SELECT SaleID FROM @SalesToDelete);

                COMMIT TRANSACTION;
                SELECT 1 AS Success; -- إرجاع True عند النجاح
            END TRY
            BEGIN CATCH
                IF @@TRANCOUNT > 0
                    ROLLBACK TRANSACTION;

                SELECT 0 AS Success; -- إرجاع False عند حدوث خطأ
            END CATCH;
        END;

         */
        public static async Task<bool> DeleteMultipleSalesAsync(List<int> saleIDs, int userId, bool returnToStock = false)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@SaleIDs", JsonConvert.SerializeObject(saleIDs)),
                new SqlParameter("@UserID", userId),
                new SqlParameter("@returnToStock", returnToStock)
            };
            return await CRUD.DeleteAsync("SP_DeleteMultipleSales", parameters);
        }
    }
}
