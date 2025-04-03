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
         
CREATE PROCEDURE sp_GetSalesById
    @SalesID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- التحقق من وجود بيانات للمبيعات
    IF NOT EXISTS (SELECT 1 FROM Sales WHERE SalesID = @SalesID)
    BEGIN
        SELECT NULL AS SalesID;
        RETURN;
    END

    -- استرجاع بيانات عملية المبيعات الأساسية
    DECLARE @SalesData NVARCHAR(MAX);
    SELECT @SalesData = (
        SELECT 
            S.SalesID,
            S.Date,
            S.SaleTotalCost,
            S.PaidAmount,
            S.IsDebt,
            S.UserID,
            -- بيانات الشخص إن وجدت
            P.PersonID,
            P.FullName AS PersonFullName,
            P.NationalNum AS PersonNationalNum,
            P.PhoneNumber AS PersonPhoneNumber,
            P.Address AS PersonAddress
        FROM Sales S
        LEFT JOIN Persons P ON S.PersonID = P.PersonID
        WHERE S.SalesID = @SalesID
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    );

    -- استرجاع تفاصيل المبيعات
    DECLARE @SalesDetailsData NVARCHAR(MAX);
    SELECT @SalesDetailsData = (
        SELECT 
            SD.SalesDetailID,
            SD.SalesID,
            SD.ProductID,
            SD.Price,
            SD.Status,
            SD.Quantity,
            SD.TotalCost,
            SD.WarrantyDate
        FROM SalesDetails SD
        WHERE SD.SalesID = @SalesID
        FOR JSON PATH
    );

    -- استرجاع المدفوعات المرتبطة بعملية المبيعات (إن وجدت)
    DECLARE @PaymentData NVARCHAR(MAX);
    SELECT @PaymentData = (
        SELECT 
            SP.PaymentID,
            SP.SalesID,
            SP.AmountPaid,
            SP.PaymentDate,
            SP.PaymentMethod
        FROM SalesPayments SP
        WHERE SP.SalesID = @SalesID
        FOR JSON PATH
    );

    -- استرجاع بيانات اليوزر المرتبط بعملية المبيعات
    DECLARE @UserData NVARCHAR(MAX);
    SELECT @UserData = (
        SELECT 
            U.UserID,
            U.UserName,
            U.IsActive,
            P.FullName AS UserFullName,
            P.NationalNum AS UserNationalNum,
            P.PhoneNumber AS UserPhoneNumber,
            P.Address AS UserAddress
        FROM Users U
        LEFT JOIN Persons P ON U.PersonID = P.PersonID
        WHERE U.UserID = (SELECT UserID FROM Sales WHERE SalesID = @SalesID)
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    );

    -- تجميع جميع البيانات في JSON واحد
    SELECT 
        @SalesData AS SalesJson,
        @SalesDetailsData AS SalesDetailsJson,
        @PaymentData AS PaymentsJson,
        @UserData AS UserJson
    FOR JSON PATH, WITHOUT_ARRAY_WRAPPER;
END;

         */
        //public static async Task<Dictionary<string, object>?> GetByIdAsync(int saleId)
        //{
        //    var dict = await CRUD.GetByColumnValueAsync("sp_GetSalesById", "SalesID", saleId);

        //    if (dict == null) return null;


        //    //foreach (var kvp in dict)
        //    //{

        //    //    Console.WriteLine(kvp.Key);
        //    //    Console.WriteLine(kvp.Value);
        //    //    if (kvp.Key.Contains("SalesJson"))
        //    //    {
        //    //        string salesJson = dict["SalesJson"].ToString()!;
        //    //        dict["SalesData"] = JsonConvert.DeserializeObject<Dictionary<string, object>>(salesJson) ?? new Dictionary<string, object>();
        //    //    }
        //    //}

        //    if (dict.ContainsKey("SalesJson") && dict["SalesJson"] != DBNull.Value)
        //    {
        //        string salesJson = dict["SalesJson"].ToString()!;
        //        dict["SalesData"] = JsonConvert.DeserializeObject<Dictionary<string, object>>(salesJson) ?? new Dictionary<string, object>();
        //    }

        //    if (dict.ContainsKey("SalesDetailsJson") && dict["SalesDetailsJson"] != DBNull.Value)
        //    {
        //        string detailsJson = dict["SalesDetailsJson"].ToString()!;
        //        dict["SalesDetailsData"] = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(detailsJson) ?? new List<Dictionary<string, object>>();
        //    }

        //    //if (dict.ContainsKey("PaymentsJson") && dict["PaymentsJson"] != DBNull.Value)
        //    //{
        //    //    string paymentsJson = dict["PaymentsJson"].ToString()!;
        //    //    dict["PaymentsData"] = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(paymentsJson) ?? new List<Dictionary<string, object>>();
        //    //}

        //    if (dict.ContainsKey("UserJson") && dict["UserJson"] != DBNull.Value)
        //    {
        //        string userJson = dict["UserJson"].ToString()!;
        //        dict["UserData"] = JsonConvert.DeserializeObject<Dictionary<string, object>>(userJson) ?? new Dictionary<string, object>();
        //    }
        //    return dict;
        //}

        //public static async Task<Dictionary<string, object>?> GetByIdAsync(int saleId)
        //{
        //    var dict = await CRUD.GetByColumnValueAsync("sp_GetSalesById", "SalesID", saleId);

        //    if (dict == null) return null;

        //    // استخراج السلسلة JSON الكاملة من النتيجة
        //    string jsonResult = dict.Values.FirstOrDefault()?.ToString()!;

        //    if (string.IsNullOrEmpty(jsonResult))
        //    {
        //        return null;
        //    }

        //    try
        //    {


        //        // تحويل النتيجة إلى ديكشنري
        //        var resultDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResult);

        //        if (resultDict == null) return null;

        //        //// معالجة SalesJson
        //        //if (resultDict.ContainsKey("SalesJson") && resultDict["SalesJson"] != null)
        //        //{
        //        //    string salesJson = resultDict["SalesJson"].ToString()!;
        //        //    resultDict["SalesData"] = JsonConvert.DeserializeObject<Dictionary<string, object>>(salesJson!)!;
        //        //}

        //        //// معالجة SalesDetailsJson
        //        //if (resultDict.ContainsKey("SalesDetailsJson") && resultDict["SalesDetailsJson"] != null)
        //        //{
        //        //    string detailsJson = resultDict["SalesDetailsJson"].ToString()!;
        //        //    resultDict["SalesDetailsData"] = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(detailsJson)!;
        //        //}

        //        //// معالجة UserJson
        //        //if (resultDict.ContainsKey("UserJson") && resultDict["UserJson"] != null)
        //        //{
        //        //    string userJson = resultDict["UserJson"].ToString()!;
        //        //    resultDict["UserData"] = JsonConvert.DeserializeObject<Dictionary<string, object>>(userJson)!;
        //        //}

        //        return resultDict;
        //    }
        //    catch (Exception ex)
        //    {
        //        // يمكنك تسجيل الخطأ هنا للتصحيح
        //        Console.WriteLine($"Error parsing JSON: {ex.Message}");
        //        return null;
        //    }
        //}


        public static async Task<Dictionary<string, object>?> GetByIdAsync(int saleId)
        {
            var dict = await CRUD.GetByColumnValueAsync("sp_GetSalesById", "SalesID", saleId);

            if (dict == null) return null;

            string jsonResult = dict.Values.FirstOrDefault()?.ToString()!;
            if (string.IsNullOrEmpty(jsonResult)) return null;

            try
            {
                var resultDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResult);
                if (resultDict == null) return null;

                Dictionary<string, object> finalResult = new();

                if (resultDict.TryGetValue("SalesJson", out var salesJson) && salesJson != null)
                {
                    finalResult["SalesData"] = JsonConvert.DeserializeObject<Dictionary<string, object>>(salesJson.ToString()!)!;
                }

                if (resultDict.TryGetValue("SalesDetailsJson", out var detailsJson) && detailsJson != null)
                {
                    finalResult["SalesDetailsData"] = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(detailsJson.ToString()!)!;
                }

                if (resultDict.TryGetValue("PaymentsJson", out var paymentsJson) && paymentsJson != null)
                {
                    finalResult["PaymentsData"] = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(paymentsJson.ToString()!)!;
                }

                if (resultDict.TryGetValue("UserJson", out var userJson) && userJson != null)
                {
                    finalResult["UserData"] = JsonConvert.DeserializeObject<Dictionary<string, object>>(userJson.ToString()!)!;
                }

                return finalResult;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllSales");

        /*

CREATE PROCEDURE SP_UpdateSaleDetails
    @SaleID INT,
    @SaleTotalCost DOUBLE PRECISION,
    @Details NVARCHAR(MAX)
AS
BEGIN
    DECLARE @SaleDetailID INT;
    DECLARE @OldQuantity INT;
    DECLARE @NewQuantity INT;
    DECLARE @OldWarrintyDate DATETIME;
    DECLARE @NewWarrintyDate DATETIME;
    DECLARE @StockID INT;
    DECLARE @OldPrice DOUBLE PRECISION;
    DECLARE @NewPrice DOUBLE PRECISION;
    DECLARE @OldTotaCost DOUBLE PRECISION;
    DECLARE @NewTotaCost DOUBLE PRECISION;
    DECLARE @Result BIT = 1;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- تحديث المبلغ الكلي للقائمة
        UPDATE Sales
        SET SaleTotalCost = @SaleTotalCost
        WHERE SaleID = @SaleID;

        -- جدول مؤقت لتخزين البيانات الجديدة من JSON
        DECLARE @SalesDetails TABLE (SaleDetailID INT NULL, StockID INT, WarrintyDate DATETIME, Price DOUBLE PRECISION, Quantity INT, TotaCost DOUBLE PRECISION);

        INSERT INTO @SalesDetails (SaleDetailID, StockID, WarrintyDate, Price, Quantity, TotaCost)
        SELECT SaleDetailID, StockID, WarrintyDate, Price, Quantity, TotaCost
        FROM OPENJSON(@Details)
        WITH (
            SaleDetailID INT,
            StockID INT,
            WarrintyDate DATETIME,
            Price DOUBLE PRECISION,
            Quantity INT,
            TotaCost DOUBLE PRECISION
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
            -- جلب الكمية والسعر القديمة
            SELECT @OldWarrintyDate = WarrintyDate, @OldPrice = Price, @OldQuantity = Quantity, @OldTotaCost = TotaCost
            FROM SalesDetails
            WHERE SaleDetailID = @SaleDetailID;

            IF @NewQuantity > @OldQuantity
            BEGIN
                UPDATE Stock
                SET Quantity = Quantity - (@NewQuantity - @OldQuantity)
                WHERE StockID = @StockID;
            END
            ELSE IF @NewQuantity < @OldQuantity
            BEGIN
                UPDATE Stock
                SET Quantity = Quantity + (@OldQuantity - @NewQuantity)
                WHERE StockID = @StockID;
            END

            -- تحديث تفاصيل البيع إذا تغيرت أي قيمة
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

        -- إضافة المنتجات الجديدة
        INSERT INTO SalesDetails (SaleID, StockID, WarrintyDate, Price, Quantity, TotaCost)
        SELECT @SaleID, StockID, WarrintyDate, Price, Quantity, TotaCost FROM @SalesDetails
        WHERE SaleDetailID IS NULL;

        -- إنقاص المنتجات الجديدة من المخزن
        UPDATE Stock
        SET Quantity = s.Quantity - d.Quantity
        FROM Stock s
        INNER JOIN @SalesDetails d ON s.StockID = d.StockID
        WHERE d.SaleDetailID IS NULL;

        -- حذف المنتجات الملغاة
        DELETE FROM SalesDetails
        OUTPUT deleted.StockID, deleted.Quantity
        INTO #DeletedStock (StockID, Quantity)
        WHERE SaleDetailID NOT IN (SELECT SaleDetailID FROM @SalesDetails)
        AND SaleID = @SaleID;

        -- إعادة الكمية المحذوفة إلى المخزن
        UPDATE Stock
        SET Quantity = s.Quantity + d.Quantity
        FROM Stock s
        INNER JOIN #DeletedStock d ON s.StockID = d.StockID;

        DROP TABLE #DeletedStock;

        COMMIT TRANSACTION;

        SELECT @Result = 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SELECT @Result = 0;
    END CATCH;

    SELECT @Result;
END;

         */
        public static async Task<bool> UpdateAsync(int? saleId, string details, double SaleTotalCost, double? paidAmount = null)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@SaleID", saleId),
                new SqlParameter("@Details", details),
                new SqlParameter("@SaleTotalCost", SaleTotalCost)
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
                new SqlParameter("@SaleID", saleId),
                new SqlParameter("@UserID", userId),
                new SqlParameter("@returnToStock", returnToStock)
            };
            return await CRUD.DeleteAsync("SP_DeleteSaleByID", parameters);
        }
    }
}