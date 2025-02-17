using System.Data;
using DAL;

namespace BLL
{
    public class clsPayment : absClassesHelper
    {
        public float Amount { get; set; }
        public DateTime Date { get; set; }

        public clsPayment()
        {
            _id = null;
            Amount = 0;
            Date = DateTime.Now;
            _mode = enMode.AddNew;
        }

        private clsPayment(int personID, float amount, DateTime date)
        {
            _id = personID;
            Amount = amount;
            Date = date;
            _mode = enMode.Update;
        }

        public static async Task<DataTable?> GetAllPaymentsAsDataTableAsync()
            => await clsPaymentsData.GetAllPaymentsAsDataTableAsync();

        public static async Task<List<object[]>?> GetAllPaymentsAsListAsync()
            => await clsPaymentsData.GetAllPaymentsAsListAsync();

        public static async Task<clsPayment> FindAsync(int personId, int userId)
        {
            var data = await clsPaymentsData.GetPaymentInfoByIDAsync(personId);
            return data != null ? new clsPayment(personId, (float)data[0], (DateTime)data[1], (int)data[2]) : null;
        }

        public static async Task<bool> IsPaymentExistAsync(int personId)
            => await clsPaymentsData.IsPaymentExistAsync(personId);

        public async Task<bool> SaveAsync(int userId)
        {
            if (_mode == enMode.AddNew)
            {
                var newId = await clsPaymentsData.AddNewPaymentAsync(_id, Amount, Date, userId);
                if (newId.HasValue)
                {
                    _id = newId;
                    _mode = enMode.Update;
                    return true;
                }
            }
            else
                return await clsPaymentsData.UpdatePaymentDataAsync(_id, Amount, Date, userId);
            return false;
        }

        public static async Task<bool> DeleteAsync(int personID, int userId)
            => await clsPaymentsData.DeletePaymentByIDAsync(personID, userId);

        public static async Task<bool> DeleteMultipleAsync(List<int> personIDs, int userId)
            => await clsPaymentsData.DeleteMultiplePaymentsAsync(personIDs, userId);
    }

}
