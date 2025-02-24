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
            var dict = await clsUsersData.AddNewUserAsync(UserName, Password, addNewPerson ? null : Person.Id, Person.FullName, Person.NationalNum, Person.PhoneNumber, Person.Address);
            if (dict == null) return false;
            Id = (int)dict["UserID"];
            if (addNewPerson)
                Person.Id = (int)dict["PersonID"];
            return true;
        }

        public static async Task<clsUsers?> GetUserByUserNameAndPasswordAsync(string userName, string password)
        {
            var dict = await clsUsersData.GetUserByUserNameAndPasswordAsync(userName, password);
            if (dict == null) return null;
            return __FetchUserData(ref dict);
        }

        public static async Task<clsUsers?> GetUserByIdAsync(int userId)
        {
            var dict = await clsUsersData.GetUserByIdAsync(userId);
            if (dict == null) return null;
            return __FetchUserData(ref dict);
        }

        public static async Task<DataTable?> GetAllUsersAsync() => await clsUsersData.GetAllUsersAsync();

        public async Task<bool> UpdateUserAsync() => await clsUsersData.UpdateUsersAsync(Id, UserName, Password, IsActive);
        public async Task<bool> UpdateUserWithPersonAsync() => await clsUsersData.UpdateUsersAsync(Id, UserName, Password, IsActive, Person.FullName, Person.NationalNum, Person.PhoneNumber, Person.Address);
        
        public static async Task<bool> DeleteUserByIdAsync(int userId) => await clsUsersData.DeleteUserByIdAsync(userId);

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
