using System.Data;
using DAL;

namespace BLL
{
    // Create By Abu Sanad
    public class clsUsers : absClassesHelperBasc
    {
        public string UserName { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; }

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
            Password = null;
            IsActive = isActive;
            Person = person;
        }

        public async Task<bool> AddAsync(bool addNewPerson = false)
        {
            var dict = await clsUsersData.AddAsync(UserName, Password, addNewPerson ? null : Person.Id, Person.FullName, Person.NationalNum, Person.PhoneNumber, Person.Address);
            if (dict == null) return false;
            Id = (int)dict["UserID"];
            if (addNewPerson)
                Person.Id = (int)dict["PersonID"];
            return true;
        }

        public static async Task<clsUsers?> GetByUserNameAndPasswordAsync(string userName, string password)
        {
            var dict = await clsUsersData.GetByUserNameAndPasswordAsync(userName, password);
            if (dict == null) return null;
            return __FetchUserData(ref dict);
        }

        public static async Task<clsUsers?> GetByIdAsync(int userId)
        {
            var dict = await clsUsersData.GetByIdAsync(userId);
            if (dict == null) return null;
            return __FetchUserData(ref dict);
        }

        public static async Task<DataTable?> GetAllAsync() => await clsUsersData.GetAllAsync();

        public async Task<bool> UpdateAsync() => await clsUsersData.UpdateAsync(Id, UserName, Password, IsActive);
        public async Task<bool> UpdateUserWithPersonAsync() => await clsUsersData.UpdateAsync(Id, UserName, Password, IsActive, Person.FullName, Person.NationalNum, Person.PhoneNumber, Person.Address);
        
        public static async Task<bool> DeleteAsync(int userId) => await clsUsersData.DeleteByIdAsync(userId);

        private static clsUsers __FetchUserData(ref Dictionary<string, object> dict)
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
