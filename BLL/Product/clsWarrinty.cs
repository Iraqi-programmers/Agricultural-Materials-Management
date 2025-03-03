using System.Data;
using DAL;

namespace BLL.Product
{
    public class clsWarrinty : absBaseEntity
    {
        public ushort Period { get; set; }

        public clsWarrinty(ushort period)
        {
            Period = period;
        }

        internal clsWarrinty(int warrintyId, ushort period)
        {
            Id = warrintyId;
            Period = period;
        }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsWarrintyData.AddAsync(Period);
            return Id.HasValue;
        }

        private async Task<bool> __UpdateAsync() => await clsWarrintyData.UpdateAsync(Id, Period);

        public static async Task<clsWarrinty?> GetByIdAsync(int warrantyId)
        {
            var dict = await clsWarrintyData.GetByIdAsync(warrantyId);
            if (dict == null) return null;
            return FetchWarrintyData(dict);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsWarrintyData.GetAllAsync();

        public static async Task<bool> DeleteAsync(int warrintyId) => await clsWarrintyData.DeleteAsync(warrintyId);

        internal static clsWarrinty FetchWarrintyData(Dictionary<string, object> dict)
        {
            return new clsWarrinty(
                (int)dict["WarrintyID"],
                (ushort)dict["Period"]
            );
        }

        public ushort CalculateTotalDays(byte years = 0, byte months = 0, byte days = 0)
            => (ushort)((years > 0 ? years * 365 : 0) + (months > 0 ? months * 30 : 0) + days);
    }
}
