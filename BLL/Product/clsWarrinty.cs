using System.Data;
using DAL;

namespace BLL.Product
{
    //Create By Abu Sanad

    //updated ByYousif
    public class clsWarrinty : absBaseEntity
    {
        public int Period { get; set; }

        public clsWarrinty(int period)
        {
            Period = period;
        }

        internal clsWarrinty(int warrintyId, int period)
        {
            Id = warrintyId;
            this.Period = period;
        }

        private async Task<bool> __AddNewAsync()
        {
            Id = await clsWarrintyData.AddAsync(Period);
            return Id.HasValue;
        }


        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddNewAsync();
            return await __UpdateAsync();
        }
        private async Task<bool> __UpdateAsync() => await clsWarrintyData.UpdateAsync(Id, Period);

        public static async Task<bool> DeleteAsync(int warrintyId) => await clsWarrintyData.DeleteAsync(warrintyId);

        public static async Task<clsWarrinty?> GetByIDAsync(int warrantyId)
        {
            var result = await clsWarrintyData.GetByIdAsync(warrantyId);
            if (result == null) return null;
            int period = (int)result["Period"];
            return new clsWarrinty(warrantyId, period);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsWarrintyData.GetAllAsync();

        public ushort CalculateTotalDays(byte years = 0, byte months = 0, byte days = 0)
        => (ushort)((years > 0 ? years * 365 : 0) + (months > 0 ? months * 30 : 0) + days);
    }
}
