using System.Data;
using DAL;

namespace BLL
{
    // Create By Abu Sanad
    public class clsUsers : absClassesHelperBasc
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public bool IsActive { get; set; } = true;

        public clsPerson Person { get; private set; }

        public clsUsers(string userName, string password, clsPerson person)
        {
            UserName = userName;
            Password = password;
            IsActive = true;
            Person = person;
        }

        private clsUsers(int? userId, string userName, bool isActive, clsPerson person)
        {
            Id = userId;
            UserName = userName;
            IsActive = isActive;
            Person = person;
        }

        internal clsUsers() { }

        public async Task<bool> SaveAsync()
        {
            if (!Id.HasValue) 
                return await __AddAsync();
            return await __UpdateAsync();
        }

        private async Task<bool> __AddAsync()
        {
            Id = await clsUsersData.AddAsync(UserName, Password, Person?.Id);
            return Id.HasValue;
        }

        public static async Task<clsUsers?> GetAsync(string userName, string password)
        {
            var dict = await clsUsersData.GetByUserNameAndPasswordAsync(userName, password);
            if (dict == null) return null;
            return FetchUserData(ref dict);
        }

        public static async Task<clsUsers?> GetByIdAsync(int userId)
        {
            var dict = await clsUsersData.GetByIdAsync(userId);
            if (dict == null) return null;
            return FetchUserData(ref dict);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsUsersData.GetAllAsync();

        public async Task<bool> __UpdateAsync() => await clsUsersData.UpdateAsync(Id, UserName, Password, IsActive, Person.FullName, Person.NationalNum, Person.PhoneNumber, Person.Address);
        
        public static async Task<bool> DeleteAsync(int userId) => await clsUsersData.DeleteByIdAsync(userId);

        internal static clsUsers FetchUserData(ref Dictionary<string, object> dict)
        {
            return new clsUsers(
                (int)dict["UserID"],
                (string)dict["UserName"],
                (bool)dict["IsActive"],
                clsPerson.FetchPersonData(ref dict)
            );
        }
    }
}
