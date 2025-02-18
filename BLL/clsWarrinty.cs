using DAL;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BLL
{
    //Create By Abu Sanad
    public class clsWarrinty
    {

        public int? WarrantyID { get; private set; }
        public int Period { get; set; }

        public clsWarrinty(int? warrantyID, int period)
        {
            this.WarrantyID = warrantyID;
            this.Period = period;
        }

        private clsWarrinty(int WarrantyID,int Period)
        {
            this.WarrantyID = WarrantyID;
            this.Period = Period;
        }

        public async Task<bool> AddNew()
        {
            this.WarrantyID = await clsWarrintyData.AddNewWarrantyAsync(this.Period);
            return this.WarrantyID.HasValue;
        }

        public async Task<bool> Update()
        {
            if (this.WarrantyID == null)
                throw new ArgumentException("WarrantyID غير موجود للتحديث!");

            return await clsWarrintyData.UpdateWarrantyAsync(this.WarrantyID.Value, this.Period);
        }

        public async Task<bool> Delete()
        {
            if (this.WarrantyID == null)
                throw new ArgumentException("WarrantyID غير موجود للحذف!");

            return await clsWarrintyData.DeleteWarrantyAsync(this.WarrantyID.Value);
        }

        public static async Task<clsWarrinty?> GetByID(int warrantyId)
        {
            var result = await clsWarrintyData.GetWarrantyByIDAsync(warrantyId);
            if (result == null) return null;

            return new clsWarrinty(
                warrantyId,
                Convert.ToInt32(result[1]) 
            );
        }

        public static async Task<DataTable?> GetAll()
        {
            return await clsWarrintyData.GetAllWarrantiesAsync();
        }




    }

}
