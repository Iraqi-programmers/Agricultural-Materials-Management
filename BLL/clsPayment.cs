using System.Data;
using DAL;

namespace BLL
{
    public class clsPayment : absClassesHelper
    {
        public int PersonId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; private set; }

        public clsPayment(int personId, double amount, int userId)
        {
            Id = null;
            PersonId = personId;
            Amount = amount;
            UserId = userId;
            _mode = enMode.AddNew;
        }

        private clsPayment(int paymentId, int personId, double amount, DateTime date, int userId)
        {
            Id = paymentId;
            PersonId = personId;
            Amount = amount;
            Date = date;
            UserId = userId;
            _mode = enMode.Update;
        }

        public static async Task<DataTable?> GetAllPaymentsAsDataTableAsync()
            => await clsPaymentsData.GetAllPaymentsAsDataTableAsync();

        public static async Task<List<object[]>?> GetAllPaymentsAsListAsync()
            => await clsPaymentsData.GetAllPaymentsAsListAsync();

        public static async Task<clsPayment?> FindAsync(int paymentId)
        {
            var data = await clsPaymentsData.GetPaymentInfoByIDAsync(paymentId);
            return new clsPayment(paymentId, data?[1] as int? ?? 0, data?[2] as double? ?? 0.0, data?[3] as DateTime? ?? DateTime.MinValue, data?[4] as int? ?? 0) ?? null;
        }

        public static async Task<bool> IsPaymentExistAsync(int paymentId)
            => await clsPaymentsData.IsPaymentExistAsync(paymentId);

        public async Task<bool> SaveAsync(int paymentId, int userId)
        {
            if (_mode == enMode.AddNew)
            {
                var newId = await clsPaymentsData.AddNewPaymentAsync(paymentId, Amount, userId);
                if (newId.HasValue)
                {
                    Id = newId;
                    _mode = enMode.Update;
                    return true;
                }
            }
            else
                return await clsPaymentsData.UpdatePaymentDataAsync(paymentId, Amount, userId);
            return false;
        }

        public static async Task<bool> DeleteAsync(int paymentId, int userId)
            => await clsPaymentsData.DeletePaymentByIDAsync(paymentId, userId);
    }
}
