using System.Data;
using DAL;

namespace BLL
{
    // Create By Abu Sanad
    public class clsUsers : absClassesHelperBasc
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsPerson Person { get; set; }

        private clsUsers(int? userId, string userName, string password, bool isActive, clsPerson person)
        {
            Id = userId;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            Person = person;
        }

        public static async Task<clsUsers?> GetUserByUserNameAndPasswordAsync(string userName, string password)
        {
            Dictionary<string, object>? dict = await clsUsersData.GetUserByUserNameAndPasswordAsync(userName, password);
            if (dict == null)
                return null;
            return __GetUserDataAsync(ref dict);
        }

        public static async Task<clsUsers?> GetUserByID(int userId)
        {
            Dictionary<string, object>? dict = await clsUsersData.GetUserByIdAsync(userId);
            if (dict == null)
                return null;
            return __GetUserDataAsync(ref dict);
        }

        // هاي الطريقة اكثر اختصار 
        //public static async Task<clsUsers?> GetUserByIdAsync(int userId)
        //{
        //    var dict = await clsUsersData.GetUserByIDAsync(userId);

        //    if (dict == null)
        //        return null;

        //    return new clsUsers(
        //        _GetInt(ref dict, "UserID"),
        //        _GetString(ref dict, "UserName"),
        //        _GetString(ref dict, "Password"),
        //        _GetBool(ref dict, "IsActive"),
        //        new clsPerson(
        //            _GetInt(ref dict, "PersonID"),
        //            _GetString(ref dict, "FullName"),
        //            _GetString(ref dict, "NationalNum"),
        //            _GetString(ref dict, "PhoneNumber"),
        //            _GetString(ref dict, "Address")
        //        )
        //    );
        //}

        public static async Task<DataTable?> GetAllUsersAsync()
           => await clsUsersData.GetAllUsersAsync();

        public async Task<bool> AddNewAsync(string userName, string password, int? personId = null, clsPerson? person = null)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("بيانات المستخدم غير مكتملة");

            // نحتاج ستورد بروسيجر تضيف يوزر موجودة بياناته بالنظام
            if (personId != null)
                Id = await clsUsersData.AddNewUsersAsync(userName, password, personId);
            // نحتاج ستورد بروسيجر تضيف بيانات يوزر جديد مع البيرسن داتا
            else if (person != null)
                Id = await clsUsersData.AddNewUsersAsync(userName, password, null, person.FullName, person.NationalNum, person.PhoneNumber, person.Address);

            return Id.HasValue;
        }

        public static async Task<bool> DeleteUserByIdAsync(int userId)
            => await clsUsersData.DeleteUserByIdAsync(userId);

        public async Task<bool> Update(int? userId, string userName, string password, bool isActive, string? fullName = null, string? nationalNum = null, string? phoneNumber = null, string? address = null)
            => await clsUsersData.UpdateUsersAsync(Id, UserName, Password, IsActive);
        
        private static clsUsers __GetUserDataAsync(ref Dictionary<string, object> dict)
        {
            int? userId = (int)dict["UserID"];
            string? userName = (string)dict["UserName"];
            string? password = (string)dict["Password"];
            bool isActive = (bool)dict["IsActive"];

            int? personId = (int)dict["PersonID"];
            string fullName = (string)dict["FullName"];
            string? nationalNum = dict["NationalNum"] == null ? null : (string)dict["NationalNum"];
            string? phoneNumber = dict["PhoneNumber"] == null ? null : (string)dict["PhoneNumber"];
            string? address = dict["Address"] == null ? null : (string)dict["Address"];

            return new clsUsers(userId, userName, password, isActive, new clsPerson(personId, fullName, nationalNum, phoneNumber, address));
        }
    }
}
