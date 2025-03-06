using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsPeoplePaymentsData
    {
        public static async Task<int?> AddAsync(double amount, int? userId, int? saleId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Amount", amount),
                new SqlParameter("@UserID", userId),
                new SqlParameter("@SaleID", saleId)
            };
            return await CRUD.AddAsync("SP_AddPeoplePayment", parameters);
        }

        public static async Task<Dictionary<string, object>?> GetAsync(int paymentId) => await CRUD.GetByColumnValueAsync("SP_GetPaymentByID", "PaymentID", paymentId);

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllPayment");

        public static async Task<bool> UpdateAsync(int? paymentId, double amount, int? userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PaymentID", paymentId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.UpdateAsync("SP_UpdatePayment", parameters);
        }

        public static async Task<bool> DeleteAsync(int paymentId) => await CRUD.DeleteAsync("SP_DeletePaymentByID", "PaymentID", paymentId);
    }
}

/*

CREATE PROCEDURE sp_UpdateSalesPayment
    @SaleIDs NVARCHAR(MAX),  -- قائمة المبيعات مفصولة بفواصل (مثلاً: '1,2,3')
    @Amount DECIMAL(18,2),   -- المبلغ المدفوع
    @UserID INT              -- معرف المستخدم الذي قام بالدفع
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SaleID INT, @DebtAmount DECIMAL(18,2), @Paid DECIMAL(18,2);
    DECLARE @SaleTable TABLE (SaleID INT);
    DECLARE @InsertedPayments TABLE (PaymentID INT);

    -- تحويل القائمة النصية إلى جدول
    INSERT INTO @SaleTable (SaleID)
    SELECT value FROM STRING_SPLIT(@SaleIDs, ',');

    -- البدء بمعالجة كل مبيع حسب الترتيب
    DECLARE cur CURSOR FOR 
    SELECT SaleID FROM @SaleTable ORDER BY SaleID;

    OPEN cur;
    FETCH NEXT FROM cur INTO @SaleID;

    WHILE @@FETCH_STATUS = 0 AND @Amount > 0
    BEGIN
        -- جلب المبلغ المتبقي للدين لهذا المبيع
        SELECT @DebtAmount = SaleTotalCost - ISNULL(PaidAmount, 0)
        FROM SalesTable WHERE SaleID = @SaleID;

        -- تحديد المبلغ الذي سيتم دفعه لهذا السجل
        SET @Paid = CASE 
                        WHEN @Amount >= @DebtAmount THEN @DebtAmount
                        ELSE @Amount 
                    END;

        -- تحديث بيانات المبيع
        UPDATE SalesTable
        SET PaidAmount = ISNULL(PaidAmount, 0) + @Paid,
            IsDebt = CASE WHEN @Paid = @DebtAmount THEN 0 ELSE IsDebt END
        WHERE SaleID = @SaleID;

        -- إدراج سجل الدفع
        INSERT INTO PeoplePayments (Amount, UserID, SaleID, Date)
        OUTPUT INSERTED.PaymentID INTO @InsertedPayments  -- حفظ معرفات المدفوعات
        VALUES (@Paid, @UserID, @SaleID, GETDATE());

        -- خصم المبلغ المدفوع من الإجمالي المتاح
        SET @Amount = @Amount - @Paid;

        FETCH NEXT FROM cur INTO @SaleID;
    END

    CLOSE cur;
    DEALLOCATE cur;

    -- إرجاع جميع معرفات المدفوعات التي تم إدراجها
    SELECT * FROM @InsertedPayments;
END;



CREATE PROCEDURE sp_GetPeoplePaymentById
    @PaymentID INT
AS
BEGIN
    SELECT pp.PaymentID, pp.Amount, pp.Date, u.UserID, u.UserName, s.SaleID
    FROM PeoplePayments pp
    JOIN Users u ON pp.UserID = u.UserID
    JOIN Sales s ON pp.SaleID = s.SaleID
    WHERE pp.PaymentID = @PaymentID;
END;

CREATE PROCEDURE sp_GetAllPeoplePayments
AS
BEGIN
    SELECT pp.PaymentID, pp.Amount, pp.Date, u.UserID, u.UserName, s.SaleID
    FROM PeoplePayments pp
    JOIN Users u ON pp.UserID = u.UserID
    JOIN Sales s ON pp.SaleID = s.SaleID;
END;

CREATE PROCEDURE sp_UpdatePeoplePayment
    @PaymentID INT,
    @Amount DECIMAL(18,2),
    @UserID INT
AS
BEGIN
    UPDATE PeoplePayments
    SET Amount = @Amount,
        UserID = @UserID
    WHERE PaymentID = @PaymentID;
END;

CREATE PROCEDURE sp_DeletePeoplePayment
    @PaymentID INT
AS
BEGIN
    DELETE FROM PeoplePayments WHERE PaymentID = @PaymentID;
END;
 
 */