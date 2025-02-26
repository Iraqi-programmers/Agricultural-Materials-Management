using System.Data;
using DAL;

namespace BLL
{
    public class clsPeoplePayment : absClassesHelperBasc
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int SaleId { get; set; }

        public clsPeoplePayment(double amount, int userId, int saleId)
        {
            Amount = amount;
            UserId = userId;
            SaleId = saleId;
        }

        private clsPeoplePayment(int paymentId, double amount, DateTime date, int userId, int saleId)
        {
            Id = paymentId;
            Amount = amount;
            Date = date;
            UserId = userId;
            SaleId = saleId;
        }

        public async Task<bool> AddAsync()
        {
            Id = await clsPeoplePaymentsData.AddAsync(Amount, UserId, SaleId);
            return Id != null;
        }

        public static async Task<DataTable?> GetAllAsync() => await clsPeoplePaymentsData.GetAllAsync();

        public static async Task<clsPeoplePayment?> GetAsync(int paymentId)
        {
            var data = await clsPeoplePaymentsData.GetAsync(paymentId);

            if (data != null)
                return new clsPeoplePayment(paymentId, (double)data["Amount"], (DateTime)data["Date"], (int)data["UserID"], (int)data["SaleID"]);

            return null;
        }

        public async Task<bool> UpdateAsync(int paymentId, double amount, int userId) => await clsPeoplePaymentsData.UpdateAsync(paymentId, amount, userId);

        public static async Task<bool> DeleteAsync(int paymentId) => await clsPeoplePaymentsData.DeleteAsync(paymentId);
    }
}
