using System.Data;
using DAL;

namespace BLL
{
    public class clsDebt : absClassesHelper
    {
        public int PersonID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DebtPaymentDate { get; set; }

        public clsDebt()
        {
            _id = null;
            PersonID = 0;
            TotalAmount = 0;
            DebtPaymentDate = DateTime.Now;
            _mode = enMode.AddNew;
        }

        private clsDebt(int id, int personID, decimal totalAmount, DateTime debtPaymentDate, bool isHeDebits)
        {
            _id = id;
            PersonID = personID;
            TotalAmount = totalAmount;
            DebtPaymentDate = debtPaymentDate;
            _mode = enMode.Update;
        }

        public static async Task<clsDebt> FindAsync(int id)
        {
            var data = await clsDebtData.GetDebtInfoByIDAsync(id);
            return data != null ? new clsDebt(id, Convert.ToInt32(data[0]), Convert.ToDecimal(data[1]), Convert.ToDateTime(data[2]), Convert.ToBoolean(data[3])) : null;
        }

        public static async Task<bool> IsDebtExistAsync(int id) => await clsDebtData.IsDebtExistAsync(id);

        public async Task<bool> SaveAsync()
        {
            if (_mode == enMode.AddNew)
            {
                var newId = await clsDebtData.AddNewDebtAsync(PersonID, TotalAmount, DebtPaymentDate);
                if (newId.HasValue)
                {
                    _id = newId;
                    _mode = enMode.Update;
                    return true;
                }
            }
            else
            {
                return await clsDebtData.UpdateDebtDataAsync(_id, TotalAmount, DebtPaymentDate);
            }
            return false;
        }

        public static async Task<bool> DeleteAsync(int id) => await clsDebtData.DeleteDebtByIDAsync(id);
        public static async Task<bool> DeleteMultipleAsync(List<int> debtIDs) => await clsDebtData.DeleteMultipleDebtsAsync(debtIDs);
        public static async Task<List<object[]>> GetDebtsByPersonIDAsync(int personID) => await clsDebtData.GetDebtsByPersonIDAsync(personID);
        public static async Task<List<object[]>> GetDebtsByDateRangeAsync(DateTime startDate, DateTime endDate) => await clsDebtData.GetDebtsByDateRangeAsync(startDate, endDate);
        public static async Task<List<object[]>> GetAllDebtsAsListAsync() => await clsDebtData.GetAllDebtsAsListAsync();
        public static async Task<DataTable> GetAllDebtsAsDataTableAsync() => await clsDebtData.GetAllDebtsAsDataTableAsync();
    }

}
