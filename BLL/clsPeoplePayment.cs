using System.Data;
using DAL;

namespace BLL
{
    public class clsPeoplePayment : absClassesHelperBasc
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; private set; }
        public int SaleId { get; private set; }

        public clsPeoplePayment(double amount, int userId, int saleId)
        {
            Amount = amount;
            UserId = userId;
            SaleId = saleId;
        }

        //private clsPeoplePayment(int paymentId, int personId, double amount, DateTime date, int userId)
        //{
        //    Id = paymentId;
        //    PersonId = personId;
        //    Amount = amount;
        //    Date = date;
        //    UserId = userId;
        //}

        public async Task<bool> AddAsync()
        {
            Id = await clsPeoplePaymentsData.AddAsync(Amount, UserId, SaleId);
            return Id != null;
        }

        public static async Task<DataTable?> GetAllAsync()
            => await clsPeoplePaymentsData.GetAllAsync();

        public static async Task<clsPeoplePayment?> GetAsync(int paymentId)
        {
            var data = await clsPeoplePaymentsData.GetAsync(paymentId);
            return new clsPayment(paymentId, data?[1] as int? ?? 0, data?[2] as double? ?? 0.0, data?[3] as DateTime? ?? DateTime.MinValue, data?[4] as int? ?? 0) ?? null;
        }

        public static async Task<bool> DeleteAsync(int paymentId)
            => await clsPeoplePaymentsData.DeleteAsync(paymentId);
    }
}
