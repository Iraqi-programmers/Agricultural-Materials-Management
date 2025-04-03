using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using Newtonsoft.Json;
using System.Data;

namespace DAL
{
    public class clsPurchasesData
    {
        public static async Task<int?> AddAsync(string details, DateTime purchaseDate, int? supplierId, double totalPrice, double? totalPaid, bool isDebt, int? userId)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@Details", details),
                new SqlParameter("@PurchaseDate", purchaseDate),
                new SqlParameter("@SupplierID", supplierId),
                new SqlParameter("@TotalPrice", totalPrice),
                new SqlParameter("@TotalPaid", totalPaid),
                new SqlParameter("@IsDebt", isDebt),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.AddAsync("AddPurchase", parameters);
        }

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int purchaseId)
        {
            var dict = await CRUD.GetByColumnValueAsync("sp_GetPurchaseById", "PurchaseID", purchaseId);

            if (dict == null || dict.Count == 0) return null;


            string JsoinResult = dict.Values.FirstOrDefault()?.ToString()!;

            if(string.IsNullOrEmpty(JsoinResult)||JsoinResult == null) return null;

            try
            {
                var ResultDic = JsonConvert.DeserializeObject<Dictionary<string, object>> (JsoinResult);
                if (ResultDic == null || ResultDic.Count == 0) return null;
                
                Dictionary<string, object> finalResult = new();

                if (ResultDic.TryGetValue("PurchaseJson", out var PurchesesJson) && PurchesesJson != null) 
                {
                    finalResult["PurchaseData"] = JsonConvert.DeserializeObject<Dictionary<string, object>>(PurchesesJson.ToString()!)!;
                }

                if (ResultDic.TryGetValue("PurchaseDetailsJson", out var PurchaseDetails) && PurchaseDetails != null)
                {
                    finalResult["PurchaseDetailsData"] = JsonConvert.DeserializeObject<List<Dictionary<string, object >>>(PurchaseDetails.ToString()!)!;
                }

                if (ResultDic.TryGetValue("SupplierPaymentsJson", out var SupplierPaymentsJson) && SupplierPaymentsJson != null)
                {
                    finalResult["SupplierPaymentsData"] = JsonConvert.DeserializeObject<Dictionary<string, object>>(SupplierPaymentsJson.ToString()!)!;
                }

                if (ResultDic.TryGetValue("PersonJson", out var PersonJson) && PersonJson != null)
                {
                    finalResult["PersonData"] = JsonConvert.DeserializeObject<Dictionary<string, object>>(PersonJson.ToString()!)!;
                }

                return finalResult;
            }
            catch (Exception )
            {
                return null;
            }
          
           
        }

       
        public static async Task<DataTable?> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            SqlParameter[] parameters = 
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            return await CRUD.GetAllAsDataTableAsync("SP_GetPurchasesByDateRange", parameters);
        }

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllPurchases");

        public static async Task<bool> UpdateAsync(string details, DateTime purchaseDate, int? purchaseId, int? supplierId, double totalPrice, double? totalPaid, bool isDebt, int? userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Details", details),
                new SqlParameter("@PurchaseID", purchaseId),
                new SqlParameter("@PurchaseDate", purchaseDate),
                new SqlParameter("@SupplierID", supplierId),
                new SqlParameter("@TotalPrice", totalPrice),
                new SqlParameter("@TotalPaid", totalPaid),
                new SqlParameter("@IsDebt", isDebt),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.UpdateAsync("SP_UpdatePurchaseDetails", parameters);
        }

        public static async Task<bool> DeleteAsync(int purchaseId) => await CRUD.DeleteAsync("SP_DeletePurchase", "@PurchaseID", purchaseId);
    }
}

/*
 
CREATE PROCEDURE sp_AddPurchase
    @PurchaseDetails NVARCHAR(MAX),
    @Date DATETIME,
    @SupplierID INT,
    @TotalPrice DECIMAL(18,2),
    @TotalPaid DECIMAL(18,2) = NULL,
    @IsDebt BIT,
    @UserID INT
AS
BEGIN
    DECLARE @PurchaseID INT;
    DECLARE @Result INT = 1; 
    BEGIN TRY
        BEGIN TRANSACTION;

        -- إضافة العملية الرئيسية في جدول المشتريات
        INSERT INTO Purchases (Date, SupplierID, TotalPrice, TotalPaid, IsDebt, UserID)
        VALUES (@Date, @SupplierID, @TotalPrice, @TotalPaid, @IsDebt, @UserID);

        SET @PurchaseID = SCOPE_IDENTITY();

        -- إنشاء جدول مؤقت لتخزين تفاصيل الشراء
        CREATE TABLE #TempStock (
            ProductID INT,
            Quantity INT,
            Status NVARCHAR(50),
            Price DECIMAL(18,2)
        );

        -- إدراج تفاصيل الشراء
        INSERT INTO PurchaseDetails (PurchaseID, ProductID, Price, Status, Quantity, WarrantyDate)
        OUTPUT INSERTED.ProductID, INSERTED.Quantity, INSERTED.Status, INSERTED.Price
        INTO #TempStock (ProductID, Quantity, Status, Price)
        SELECT @PurchaseID, ProductID, Price, Status, Quantity, WarrantyDate
        FROM OPENJSON(@PurchaseDetails)
        WITH (
            ProductID INT '$.ProductID',
            Price DECIMAL(18,2) '$.Price',
            Status NVARCHAR(50) '$.Status',
            Quantity INT '$.Quantity',
            WarrantyDate DATETIME '$.WarrantyDate'
        );

        -- إضافة الكميات إلى المخزون
        DECLARE @ProductID INT, @Quantity INT, @Status NVARCHAR(50), @Price DECIMAL(18,2);
        DECLARE stock_cursor CURSOR FOR
        SELECT ProductID, Quantity, Status, Price FROM #TempStock;

        OPEN stock_cursor;
        FETCH NEXT FROM stock_cursor INTO @ProductID, @Quantity, @Status, @Price;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            EXEC AddStock @ProductId = @ProductID, @Quantity = @Quantity, @Status = @Status, @Price = @Price;
            FETCH NEXT FROM stock_cursor INTO @ProductID, @Quantity, @Status, @Price;
        END;

        CLOSE stock_cursor;
        DEALLOCATE stock_cursor;

        DROP TABLE #TempStock;

        -- إنهاء الترانزاكشن
        COMMIT TRANSACTION;

        -- إرجاع رقم العملية
        SELECT @PurchaseID AS PurchaseID;
    END TRY
    BEGIN CATCH
        -- في حال وجود خطأ
        ROLLBACK TRANSACTION;
        SET @Result = 0;
    END CATCH;

    IF @Result = 0
    BEGIN
        SELECT NULL AS PurchaseID;
    END;
END;

-- إجراء مخزن لتحديث عملية شراء
CREATE PROCEDURE sp_UpdatePurchase
    @PurchaseDetails NVARCHAR(MAX),
    @Date DATETIME,
    @PurchaseID INT,
    @SupplierID INT,
    @TotalPrice DECIMAL(18,2),
    @TotalPaid DECIMAL(18,2) = NULL,
    @IsDebt BIT,
    @UserID INT
AS
BEGIN
    DECLARE @Success BIT = 0;
    BEGIN TRANSACTION;

    BEGIN TRY
        -- 1️ تحديث بيانات عملية الشراء الأساسية
        UPDATE Purchases
        SET Date = @Date,
            SupplierID = @SupplierID,
            TotalPrice = @TotalPrice,
            TotalPaid = @TotalPaid,
            IsDebt = @IsDebt,
            UserID = @UserID
        WHERE PurchaseID = @PurchaseID;

        -- 2️ حفظ البيانات القديمة من التفاصيل قبل التعديل
        DECLARE @OldDetails TABLE (
            ProductID INT,
            Price DECIMAL(18,2),
            Quantity INT,
            WarrantyDate DATETIME
        );

        INSERT INTO @OldDetails
        SELECT ProductID, Price, Quantity, WarrantyDate
        FROM PurchaseDetails
        WHERE PurchaseID = @PurchaseID;

        -- 3️ تحديث بيانات المخزون بناءً على الفرق بين البيانات الجديدة والقديمة
        DECLARE @NewDetails TABLE (
            ProductID INT,
            Price DECIMAL(18,2),
            Quantity INT,
            WarrantyDate DATETIME
        );

        INSERT INTO @NewDetails
        SELECT ProductID, Price, Quantity, WarrantyDate
        FROM OPENJSON(@PurchaseDetails)
        WITH (
            ProductID INT '$.ProductID',
            Price DECIMAL(18,2) '$.Price',
            Quantity INT '$.Quantity',
            WarrantyDate DATETIME '$.WarrantyDate'
        );

        -- استرجاع الكميات المحذوفة إلى المخزون
        UPDATE Inventory
        SET Quantity = Inventory.Quantity + OD.Quantity
        FROM Inventory
        INNER JOIN @OldDetails OD ON Inventory.ProductID = OD.ProductID
        LEFT JOIN @NewDetails ND ON OD.ProductID = ND.ProductID
        WHERE ND.ProductID IS NULL;

        -- حذف التفاصيل القديمة غير الموجودة في التفاصيل الجديدة
        DELETE FROM PurchaseDetails
        WHERE PurchaseID = @PurchaseID
        AND ProductID NOT IN (SELECT ProductID FROM @NewDetails);

        -- تحديث البيانات الموجودة بالفعل
        UPDATE PD
        SET PD.Price = ND.Price,
            PD.Quantity = ND.Quantity,
            PD.WarrantyDate = ND.WarrantyDate
        FROM PurchaseDetails PD
        INNER JOIN @NewDetails ND ON PD.ProductID = ND.ProductID
        WHERE PD.PurchaseID = @PurchaseID;

        -- إضافة التفاصيل الجديدة
        INSERT INTO PurchaseDetails (PurchaseID, ProductID, Price, Quantity, WarrantyDate)
        SELECT @PurchaseID, ND.ProductID, ND.Price, ND.Quantity, ND.WarrantyDate
        FROM @NewDetails ND
        LEFT JOIN PurchaseDetails PD ON ND.ProductID = PD.ProductID AND PD.PurchaseID = @PurchaseID
        WHERE PD.ProductID IS NULL;

        -- تحديث المخزون بالكميات الجديدة
        UPDATE Inventory
        SET Quantity = Inventory.Quantity - ND.Quantity
        FROM Inventory
        INNER JOIN @NewDetails ND ON Inventory.ProductID = ND.ProductID;

        -- 4️ تحديث بيانات المدفوعات (Supplier Payments)
        UPDATE SupplierPayments
        SET AmountPaid = @TotalPaid
        WHERE PurchaseID = @PurchaseID AND SupplierID = @SupplierID;

        -- نجاح العملية
        SET @Success = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Success = 0;
    END CATCH;

    -- إرجاع النتيجة
    SELECT @Success AS Result;
END;


-- إجراء مخزن لاسترجاع عملية شراء حسب المعرف
CREATE PROCEDURE sp_GetPurchaseById
    @PurchaseID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- التحقق من وجود بيانات للشراء
    IF NOT EXISTS (SELECT 1 FROM Purchases WHERE PurchaseID = @PurchaseID)
    BEGIN
        SELECT NULL AS PurchaseID;
        RETURN;
    END

    -- استرجاع بيانات عملية الشراء الأساسية مع بيانات المورد والمستخدم
    DECLARE @PurchaseData NVARCHAR(MAX);
    SELECT @PurchaseData = (
        SELECT 
            P.PurchaseID,
            P.Date,
            P.TotalPrice,
            P.TotalPaid,
            P.IsDebt,
            P.UserID,
            -- بيانات المورد
            S.SupplierID,
            S.SupplierName,
            S.Phone AS SupplierPhone,
            S.Address AS SupplierAddress,
            -- بيانات المستخدم
            U.UserID,
            U.UserName,
            PData.FullName AS UserFullName,
            PData.NationalNum,
            PData.PhoneNumber AS UserPhoneNumber,
            PData.Address AS UserAddress,
            U.IsActive
        FROM Purchases P
        LEFT JOIN Suppliers S ON P.SupplierID = S.SupplierID
        LEFT JOIN Users U ON P.UserID = U.UserID
        LEFT JOIN Persons PData ON U.PersonID = PData.PersonID
        WHERE P.PurchaseID = @PurchaseID
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    );

    -- استرجاع تفاصيل عملية الشراء بدون بيانات المخزون
    DECLARE @PurchaseDetailsData NVARCHAR(MAX);
    SELECT @PurchaseDetailsData = (
        SELECT 
            PD.PurchaseDetailID,
            PD.PurchaseID,
            PR.ProductID,
            PR.CompanyID,
            C.CompanyName,
            PR.TypeID,
            T.TypeName,
            PR.SizeID,
            SZ.Size,
            PR.ThicknessID,
            TH.Thickness,
            PR.WarrintyID,
            W.Period,
            PD.Price,
            PD.Quantity,
            PD.WarrantyDate,
            PD.Status
        FROM PurchaseDetailsData PD
        LEFT JOIN Products PR ON PD.ProductID = PR.ProductID
        LEFT JOIN Companies C ON PR.CompanyID = C.CompanyID
        LEFT JOIN ProductTypes T ON PR.TypeID = T.TypeID
        LEFT JOIN Sizes SZ ON PR.SizeID = SZ.SizeID
        LEFT JOIN Thicknesses TH ON PR.ThicknessID = TH.ThicknessID
        LEFT JOIN Warranties W ON PR.WarrintyID = W.WarrintyID
        WHERE PD.PurchaseID = @PurchaseID
        FOR JSON PATH
    );

    -- استرجاع المدفوعات المرتبطة بعملية الشراء
    DECLARE @SupplierPaymentsData NVARCHAR(MAX);
    SELECT @SupplierPaymentsData = (
        SELECT 
            SP.PaymentID,
            SP.PurchaseID,
            SP.SupplierID,
            SP.AmountPaid,
            SP.PaymentDate,
            SP.PaymentMethod
        FROM SupplierPayments SP
        WHERE SP.PurchaseID = @PurchaseID
        FOR JSON PATH
    );

    -- استرجاع بيانات الشخص المرتبط إن وجدت
    DECLARE @PersonData NVARCHAR(MAX);
    SELECT @PersonData = (
        SELECT 
            P.PersonID,
            P.FullName,
            P.NationalNum,
            P.PhoneNumber,
            P.Address
        FROM Persons P
        INNER JOIN Users U ON P.PersonID = U.PersonID
        INNER JOIN Purchases Pr ON U.UserID = Pr.UserID
        WHERE Pr.PurchaseID = @PurchaseID
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    );

    -- تجميع جميع البيانات في JSON واحد
    SELECT 
        @PurchaseData AS PurchaseJson,
        @PurchaseDetailsData AS PurchaseDetailsJson,
        @SupplierPaymentsData AS SupplierPaymentsJson,
        @PersonData AS PersonJson
    FOR JSON PATH, WITHOUT_ARRAY_WRAPPER;
END;

-- إجراء مخزن لاسترجاع جميع عمليات الشراء
CREATE PROCEDURE sp_GetAllPurchases
AS
BEGIN
    SELECT P.PurchaseID, P.Date, P.SupplierID, P.TotalPrice, P.TotalPaid, P.IsDebt, P.UserID, PD.DetailID, PD.ProductID, PD.Price, PD.Status, PD.Quantity, PD.WarrantyDate
    FROM Purchases P
    JOIN PurchaseDetails PD ON P.PurchaseID = PD.PurchaseID;
END;

-- إجراء مخزن لحذف عملية شراء
CREATE PROCEDURE sp_DeletePurchase
    @PurchaseID INT
AS
BEGIN
    DELETE FROM PurchaseDetails WHERE PurchaseID = @PurchaseID;
    DELETE FROM Purchases WHERE PurchaseID = @PurchaseID;
END;

 */
