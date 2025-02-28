using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using Newtonsoft.Json;

namespace DAL
{
    public class clsSalesData
    {
        // هاي نلغي اضافة الشخص من داخلها افضل تبقى بس تاخذ ايدي شخص او لا 
        // يعني اذا نحتاج نضيف بيانات شخص لازم نضيفه من الواجه بالبداية او نمرر ايديه او نختار ايدي الشخص اذا جان اله بيانات بالسستم 
        /*
        CREATE PROCEDURE SP_AddSaleWithDetails
            @UserID INT,
            @Details NVARCHAR(MAX),
            @SaleTotalCost DECIMAL(10,2),
            @PaidAmount DECIMAL(10,2) = NULL,
            @PersonID INT = NULL,
            @SaleID INT OUTPUT
        AS
        BEGIN
            SET NOCOUNT ON;
            DECLARE @NewPersonID INT = NULL;
            DECLARE @IsDebt BIT;
            DECLARE @ErrorMessage NVARCHAR(4000);
            DECLARE @ErrorSeverity INT;
            DECLARE @ErrorState INT;

            BEGIN TRY
                BEGIN TRANSACTION;

                -- تحديد حالة IsDebt
                IF @PaidAmount IS NULL OR @PaidAmount < @SaleTotalCost
                    SET @IsDebt = 1; -- True (دين)
                ELSE
                    SET @IsDebt = 0; -- False (ليس دين)

                -- معالجة بيانات الشخص
                IF @PersonID IS NOT NULL
                BEGIN
                    SET @NewPersonID = @PersonID;
                END
                
                -- إدراج عملية البيع
                INSERT INTO Sales (SaleDate, PersonID, UserID, SaleTotalCost, PaidAmount, IsDebt)
                VALUES (GETDATE(), @NewPersonID, @UserID, @SaleTotalCost, @PaidAmount, @IsDebt);

                SET @SaleID = SCOPE_IDENTITY();

                -- إدراج الدفع إذا كان هناك مبلغ مدفوع
                IF @PaidAmount IS NOT NULL AND @PaidAmount > 0
                BEGIN
                    INSERT INTO PayablePayments (SaleID, PaidAmount, PaymentDate)
                    VALUES (@SaleID, @PaidAmount, GETDATE());
                END

                -- إدراج تفاصيل البيع
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

                -- تحديث المخزون
                UPDATE s
                SET s.Quantity = s.Quantity - d.Quantity
                FROM Stock s
                INNER JOIN OPENJSON(@Details)
                WITH (
                    StockID INT,
                    Quantity INT
                ) AS d ON s.StockID = d.StockID;

                COMMIT TRANSACTION;
                SELECT @SaleID AS SaleID;
            END TRY
            BEGIN CATCH
                ROLLBACK TRANSACTION;
                SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
                RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
                SET @SaleID = NULL;
            END CATCH;
        END;
         */
        public static async Task<int?> AddAsync(string details, int? userId, double saleTotalCost, double? paidAmount = null, int? personId = null)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Details", details),
                new SqlParameter("@UserID", userId),
                new SqlParameter("@SaleTotalCost", saleTotalCost),
                new SqlParameter("@PaidAmount", paidAmount)
            };

            if (personId.HasValue) 
                parameters.Append(new SqlParameter("@PersonID", personId));

            return await CRUD.AddAsync("SP_AddSaleWithDetails", parameters);
        }

        /*
        CREATE PROCEDURE SP_GetSaleWithDetailsById
            @SaleID INT
        AS
        BEGIN
            SET NOCOUNT ON;

            -- جلب بيانات المبيعات
            SELECT 
                S.SaleID,
                S.UserID,
                S.Date,
                S.SaleTotalCost,
                S.IsDebt,
                S.PaidAmount,
                S.PersonID,
                (
                    SELECT 
                        SD.DetailID,
                        SD.StockID,
                        SD.WarrantyDate,
                        SD.Price,
                        SD.Quantity
                    FROM SalesDetails SD
                    WHERE SD.SaleID = S.SaleID
                    FOR JSON PATH
                ) AS SalesDetailsJson
            FROM Sales S
            WHERE S.SaleID = @SaleID;
        END
         */
        public static async Task<Dictionary<string, object>?> GetByIdAsync(int saleId)
        {
            // dict لازم يحتوي على بيانات اليوزر كاملة
            // وبيانات البيرسن الزبون ايظا اذا كانت موجودة
            // وبيانات السيل مع جميع الريكوردز الي تمثل المنجات الموجودة بالقائمة
            var dict = await CRUD.GetByColumnValueAsync("SP_GetSaleWithDetailsById", "SaleID", saleId);

            if (dict == null) return null;

            if (dict.ContainsKey("SalesDetailsJson") && dict["SalesDetailsJson"] != DBNull.Value)
            {
                string detailsJson = dict["SalesDetailsJson"].ToString()!;
                dict["SalesDetails"] = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(detailsJson) ?? new List<Dictionary<string, object>>();
            }
            return dict;
        }

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_");

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
                DECLARE @SalesDetails TABLE (SaleDetailID INT NULL, StockID INT, WarrintyDate DATETIME, Price DECIMAL(10,2), Quantity INT, TotaCost DECIMAL(10,2));
        
                INSERT INTO @SalesDetails (SaleDetailID, StockID, WarrintyDate, Price, Quantity, TotaCost)
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
                FROM @SalesDetails sd
                WHERE sd.SaleDetailID IS NOT NULL; 
        
                OPEN product_cursor;
                FETCH NEXT FROM product_cursor INTO @SaleDetailID, @StockID, @NewWarrintyDate, @NewPrice, @NewQuantity, @NewTotaCost;
        
                WHILE @@FETCH_STATUS = 0
                BEGIN
                    -- جلب الكمية والسعر القديمة من SaleDetails
                    SELECT @OldWarrintyDate = WarrintyDate, @OldPrice = Price, @OldQuantity = Quantity, @OldTotaCost = TotaCost FROM SalesDetails
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
                        UPDATE SalesDetails
                        SET Price = @NewPrice
                        WHERE SaleDetailID = @SaleDetailID;
                    END
                    
                    IF @NewQuantity <> @OldQuantity 
                    BEGIN
                        UPDATE SalesDetails
                        SET Quantity = @NewQuantity
                        WHERE SaleDetailID = @SaleDetailID;
                    END
                    
                    IF @NewTotaCost <> @OldTotaCost
                    BEGIN
                        UPDATE SalesDetails
                        SET TotaCost = @NewTotaCost
                        WHERE SaleDetailID = @SaleDetailID;
                    END

                    IF @NewWarrintyDate <> @OldWarrintyDate 
                    BEGIN
                        UPDATE SalesDetails
                        SET WarrintyDate = @NewWarrintyDate
                        WHERE SaleDetailID = @SaleDetailID;
                    END
        
                    FETCH NEXT FROM product_cursor INTO @SaleDetailID, @StockID, @NewWarrintyDate, @NewPrice, @NewQuantity, @NewTotaCost;
                END
        
                CLOSE product_cursor;
                DEALLOCATE product_cursor;
        
                -- نضيف المنتجات الجديدة بالقائمة في حال توجد منتجات مضافة على القائمة
                INSERT INTO SalesDetails (SaleID, StockID, WarrintyDate, Price, Quantity, TotaCost)
                SELECT @SaleID, StockID, WarrintyDate, Price, Quantity, TotaCost FROM @SalesDetails
                WHERE SaleDetailID IS NULL;
        
                -- انقص المنتجات المضافة للقائمة من المخزن
                UPDATE Stock
                SET Quantity = s.Quantity - d.Quantity
                FROM Stock s
                INNER JOIN @SalesDetails d ON s.StockID = d.StockID
                WHERE d.SaleDetailID IS NULL; 
        
                -- نحذف المنتجات الي تم الغائها من القائمة
                DELETE FROM SalesDetails
                OUTPUT deleted.StockID, deleted.Quantity
                INTO #DeletedStock (StockID, Quantity)
                WHERE SaleDetailID NOT IN (SELECT SaleDetailID FROM @SalesDetails)
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
        public static async Task<bool> UpdateAsync(int? saleId, string details, double SaleTotalCost, double? paidAmount = null)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Details", details),
                new SqlParameter("@SaleTotalCost", SaleTotalCost),
                new SqlParameter("@SaleID", saleId)
            };
            if (paidAmount != null)
                parameters.Append(new SqlParameter("@PaidAmount", paidAmount));
            return await CRUD.UpdateAsync("SP_UpdateSaleDetails", parameters);
        }

        /*
        CREATE PROCEDURE SP_DeleteSaleByID
            @SaleID INT,
            @UserID INT,
            @returnToStock BIT,
            @Success BIT OUTPUT
        AS
        BEGIN
            SET NOCOUNT ON;
            SET @Success = 0; -- تعيين القيمة الافتراضية كـ False
    
            BEGIN TRY
                BEGIN TRANSACTION;

                -- التحقق من وجود البيع
                IF NOT EXISTS (SELECT 1 FROM Sales WHERE SaleID = @SaleID)
                BEGIN
                    ROLLBACK TRANSACTION;
                    RETURN;
                END

                -- استرجاع الكميات للمخزون إذا كان returnToStock = 1
                IF @returnToStock = 1
                BEGIN
                    UPDATE s
                    SET s.Quantity = s.Quantity + d.Quantity
                    FROM Stock s
                    INNER JOIN SalesDetails d ON s.StockID = d.StockID
                    WHERE d.SaleID = @SaleID;
                END

                -- حذف المدفوعات المرتبطة إذا كان returnToStock = 1
                IF @returnToStock = 1
                BEGIN
                    DELETE FROM PayablePayments WHERE SaleID = @SaleID;
                END

                -- حذف تفاصيل البيع
                DELETE FROM SalesDetails WHERE SaleID = @SaleID;

                -- حذف عملية البيع
                DELETE FROM Sales WHERE SaleID = @SaleID;

                COMMIT TRANSACTION;
                SET @Success = 1; -- تعيين القيمة إلى True عند نجاح العملية
            END TRY
            BEGIN CATCH
                IF @@TRANCOUNT > 0
                    ROLLBACK TRANSACTION;

                SET @Success = 0; -- تعيين القيمة إلى False عند حدوث خطأ
            END CATCH;
        END;
         */
        public static async Task<bool> DeleteAsync(int? saleId, int? userId, bool returnToStock = false)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@SaleID", saleId),
                new SqlParameter("@returnToStock", returnToStock)
            };
            return await CRUD.DeleteAsync("SP_DeleteSaleByID", parameters);
        }
    }
}
