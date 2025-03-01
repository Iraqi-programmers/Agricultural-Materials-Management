using System.Data;
using DAL;

namespace BLL
{
    public class clsPeoplePayment : absClassesHelperBasc
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public clsUsers UserInfo { get; private set; }
        public clsSales SaleInfo { get; private set; }

        public clsPeoplePayment(double amount, clsUsers userInfo, clsSales saleInfo)
        {
            Amount = amount;
            UserInfo = userInfo;
            SaleInfo = saleInfo;
        }

        private clsPeoplePayment(int paymentId, double amount, DateTime date, clsUsers userInfo, clsSales saleInfo)
        {
            Id = paymentId;
            Amount = amount;
            Date = date;
            UserInfo = userInfo;
            SaleInfo = saleInfo;
        }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsPeoplePaymentsData.AddAsync(Amount, UserInfo.Id, SaleInfo.Id);
            return Id.HasValue;
        }

        public static async Task<DataTable?> GetAllAsync() => await clsPeoplePaymentsData.GetAllAsync();

        public static async Task<clsPeoplePayment?> GetAsync(int paymentId)
        {
            var dict = await clsPeoplePaymentsData.GetAsync(paymentId);
            if (dict == null) return null;
            return FetchPeoplePaymentData(ref dict);
        }

        private async Task<bool> __UpdateAsync() => await clsPeoplePaymentsData.UpdateAsync(Id, Amount, UserInfo.Id);

        public static async Task<bool> DeleteAsync(int paymentId) => await clsPeoplePaymentsData.DeleteAsync(paymentId);

        internal static clsPeoplePayment FetchPeoplePaymentData(ref Dictionary<string, object> dict)
        {
            return new clsPeoplePayment(
                (int)dict["PaymentID"],
                (double)dict["Amount"],
                (DateTime)dict["Date"],
                clsUsers.FetchUserData(ref dict),
                clsSales.FetchSaleData(ref dict)
            );
        }
    }
}
