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

        public static async Task<Dictionary<string, object>?> GetAsync(int paymentId) => await CRUD.GetByColumnValueAsync("SP_GetFullPaymentDetails_JSON", "PaymentID", paymentId);

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllPeoplePayments");

        public static async Task<bool> UpdateAsync(int? paymentId, double amount, int? userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PaymentID", paymentId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@UserID", userId)
            };
            return await CRUD.UpdateAsync("sp_UpdatePeoplePayment", parameters);
        }

        public static async Task<bool> DeleteAsync(int paymentId) => await CRUD.DeleteAsync("sp_DeletePeoplePayment", "PaymentID", paymentId);
    }
}

/*

CREATE PROCEDURE sp_AddPeoplePayment
    @Amount DECIMAL(18,2),
    @UserID INT,
    @SaleID INT
AS
BEGIN
    INSERT INTO PeoplePayments (Amount, UserID, SaleID, Date)
    VALUES (@Amount, @UserID, @SaleID, GETDATE());

    SELECT SCOPE_IDENTITY() AS PaymentID;
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