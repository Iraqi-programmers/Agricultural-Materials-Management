using System.Data;
using DAL;

namespace BLL
{
    public class clsSale : absClassesHelper
    {
        public DateTime Date { get; set; }
        public int? PersonId { get; set; }
        public int UserId { get; set; }
        public double SaleTotalCost { get; set; }

        public List<object[]>? Details { get; private set; }

        public clsSale(List<object[]>? details, int? personId = null)
        {
            Id = null;
            PersonId = personId;
            Details = details;
        }

        private clsSale(int id, int userId, DateTime date, double saleTotalCost, int? personId = null) 
        { 
            Id = id;
            Date = date;
            PersonId = personId;
            UserId = userId;
            SaleTotalCost = saleTotalCost;
        }

        public static async Task<clsSale?> FindAsync(int saleId)
        {
            var data = await clsSalesData.GetSaleInfoByIDAsync(saleId);
            return new clsSale(data?[0] as int? ?? 0, data?[3] as int? ?? 0, data?[1] as DateTime? ?? DateTime.MinValue, data?[4] as double? ?? 0.0, data?[2] as int? ?? 0) ?? null;
        }

        public static async Task<DataTable?> GetAllSalesAsDataTableAsync() => await clsSalesData.GetAllSalesAsDataTableAsync();
        public static async Task<List<object[]>?> GetAllSalesAsListAsync() => await clsSalesData.GetAllSalesAsListAsync();

        public static async Task<bool> IsSaleExistAsync(int saleId) => await clsSalesData.IsSaleExistAsync(saleId);

        public async Task<int?> AddSaleWithDetailsAsync(int userId, string? fullName = null, string? nationalNum = null, string? phoneNum = null, string? address = null)
        {
            Id = await clsSalesData.AddSaleWithDetailsAsync(Details, userId, SaleTotalCost, PersonId, fullName, nationalNum, phoneNum, address);
            return Id;
        }          

        public  async Task<bool> UpdateSaleDataAsync(int userId) => await clsSalesData.UpdateSaleDetailsAsync(userId, Details, SaleTotalCost);

        public static async Task<bool> DeleteAsync(int saleId, int userId) => await clsSalesData.DeleteSaleByIDAsync(saleId, userId);
        public static async Task<bool> DeleteMultipleAsync(List<int> saleIDs, int userId) => await clsSalesData.DeleteMultipleSalesAsync(saleIDs, userId);
    }
}
