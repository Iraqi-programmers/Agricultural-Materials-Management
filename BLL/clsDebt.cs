using System.Data;
using DAL;

namespace BLL
{
    public class clsDebt : absClassesHelper
    {
        public int PersonID { get; set; }
        public double TotalAmount { get; set; }
        public DateTime DebtPaymentDate { get; set; }

        public clsDebt(int personId, double totalAmount)
        {
            Id = null;
            PersonID = personId;
            TotalAmount = totalAmount;
            DebtPaymentDate = DateTime.Now;
            _mode = enMode.AddNew;
        }

        private clsDebt(int debtId, int personId, double totalAmount, DateTime debtPaymentDate)
        {
            Id = debtId;
            PersonID = personId;
            TotalAmount = totalAmount;
            DebtPaymentDate = debtPaymentDate;
            _mode = enMode.Update;
        }

        public static async Task<clsDebt?> FindAsync(int debtId)
        {
            var data = await clsDebtData.GetDebtInfoByIDAsync(debtId);
            return new clsDebt(debtId, Convert.ToInt32(data[0]), Convert.ToDouble(data[1]), Convert.ToDateTime(data[2])) ?? null;
        }

        public static async Task<bool> IsDebtExistAsync(int debtId) => await clsDebtData.IsDebtExistAsync(debtId);

        public async Task<bool> SaveAsync(int userId)
        {
            if (_mode == enMode.AddNew)
            {
                var newId = await clsDebtData.AddNewDebtAsync(PersonID, TotalAmount, DebtPaymentDate, userId);
                if (newId.HasValue)
                {
                    Id = newId;
                    _mode = enMode.Update;
                    return true;
                }
            }
            else
                return await clsDebtData.UpdateDebtDataAsync(Id, TotalAmount, DebtPaymentDate);
            return false;
        }

        public static async Task<bool> DeleteAsync(int debtId, int userId) => await clsDebtData.DeleteDebtByIDAsync(debtId, userId);
        public static async Task<bool> DeleteMultipleAsync(List<int> debtIDs, int userId) => await clsDebtData.DeleteMultipleDebtsAsync(debtIDs, userId);
        public static async Task<List<object[]>?> GetDebtsByPersonIDAsync(int personId) => await clsDebtData.GetDebtsByPersonIDAsync(personId);
        public static async Task<List<object[]>?> GetDebtsByDateRangeAsync(DateTime startDate, DateTime endDate) => await clsDebtData.GetDebtsByDateRangeAsync(startDate, endDate);
        public static async Task<List<object[]>?> GetAllDebtsAsListAsync() => await clsDebtData.GetAllDebtsAsListAsync();
        public static async Task<DataTable?> GetAllDebtsAsDataTableAsync() => await clsDebtData.GetAllDebtsAsDataTableAsync();
    }
}
