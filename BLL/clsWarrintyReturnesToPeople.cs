using System.Data;
using DAL;

namespace BLL
{
    public class clsWarrintyReturnesToPeople : absBaseEntity
    {
        public clsReturnedStocks Stockinfo { get; private set; }
        public clsUsers UserInfo { get; private set; }
        public clsPerson PersonInfo { get; private set; }

        public clsWarrintyReturnesToPeople(clsReturnedStocks stockInfo, clsPerson person, clsUsers userInfo)
        {
            Stockinfo = stockInfo;
            PersonInfo = person;
            UserInfo = userInfo;
        }

        private clsWarrintyReturnesToPeople(int warrantyRetId, clsReturnedStocks stockInfo, clsPerson person, clsUsers userInfo)
        {
            Id = warrantyRetId;
            Stockinfo = stockInfo;
            PersonInfo = person;
            UserInfo = userInfo;
        }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue)
                return await __AddAsync();
            return await __UpdateAsync();
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsWarrintyReturnesToPeopleData.AddAsync(Stockinfo.Id, UserInfo.Person.Id, UserInfo.Id);
            return Id.HasValue;
        }

        public static async Task<clsWarrintyReturnesToPeople?> GetByIdAsync(int warrintyReturnesId)
        {
            var dict = await clsWarrintyReturnesToPeopleData.GetByIdAsync(warrintyReturnesId);
            if (dict == null) return null;
            return FetchWarrintyReturnesData(ref dict);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsWarrintyReturnesToPeopleData.GetAllAsync();

        public  async Task<bool> UpdateAsync(int warrantyReturnedId) => await clsWarrintyReturnesToPeopleData.UpdateAsync(warrantyReturnedId, UserInfo.Person.Id, UserInfo.Id);

        private async Task<bool> __UpdateAsync() => await clsWarrintyReturnesToPeopleData.UpdateAsync(Id, UserInfo.Person.Id, UserInfo.Id);

        public static async Task<bool> DeleteAsync(int warrintyReturnesId) => await clsWarrintyReturnesToPeopleData.DeleteAsync(warrintyReturnesId);

        internal static clsWarrintyReturnesToPeople FetchWarrintyReturnesData(ref Dictionary<string, object> dict)
        {
            return new clsWarrintyReturnesToPeople(
                (int)dict["WarrantyReturnedID"],
                clsReturnedStocks.FetchReturnedStocksData(ref dict),
                clsPerson.FetchPersonData(ref dict),
                clsUsers.FetchUserData(ref dict)
            );
        }
    }
}
