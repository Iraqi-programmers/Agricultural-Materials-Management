using System;
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

        public clsUsers(string userName, string password, int personId)
        {
            UserName = userName;
            Password = password;
            IsActive = true;
        }

        public clsUsers(string userName, string password, clsPerson person)
        {
            UserName = userName;
            Password = password;
            IsActive = true;
            Person = person;
        }

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
            return __GetUserData(ref dict);
        }

        public static async Task<clsUsers?> GetUserByIdAsync(int userId)
        {
            var dict = await clsUsersData.GetUserByIdAsync(userId);
            if (dict == null) return null;
            return __GetUserData(ref dict);
        }

        public static async Task<DataTable?> GetAllUsersAsync()
           => await clsUsersData.GetAllUsersAsync();

        public static async Task<clsUsers?> AddAsync(string userName, string password, int personId)
        {
            int? id = await clsUsersData.AddNewUsersAsync(userName, password, null, person.FullName, person.NationalNum, person.PhoneNumber, person.Address);
            return id != null ? new clsUsers(id, userName, password, true, person) : null;
            //int? id;

            id = await clsUsersData.AddNewUsersAsync(userName, password, personId);
            return id != null ? new clsUsers(id, userName, password) : null;
         
        }

        public static async Task<clsUsers?> AddAsync(string userName, string password, clsPerson person)
        {
            int? id = await clsUsersData.AddNewUsersAsync(userName, password, null, person.FullName, person.NationalNum, person.PhoneNumber, person.Address);
            return id != null ? new clsUsers(id, userName, password, true, person): null;
            //int? id;
            //// نحتاج ستورد بروسيجر تضيف يوزر موجودة بياناته بالنظام
            //if (personId != null)
            //{
            //    id = await clsUsersData.AddNewUsersAsync(userName, password, personId);
            //    return id != null ? new clsUsers(id, userName, password) : null;
            //}
            //// نحتاج ستورد بروسيجر تضيف بيانات يوزر جديد مع البيرسن داتا
            //else if (person != null)
            //{
            //    id = await clsUsersData.AddNewUsersAsync(userName, password, null, person.FullName, person.NationalNum, person.PhoneNumber, person.Address);
            //    return new clsUsers(id, userName, password, true, person);
            //}
            //return null;
        }

        public async Task<bool> AddAsync(string userName, string password, int? personId = null, clsPerson? person = null)
        {
            UserName = userName;
            Password = password;
            IsActive = true;

            // نحتاج ستورد بروسيجر تضيف يوزر موجودة بياناته بالنظام
            if (personId != null)
                Id = await clsUsersData.AddNewUsersAsync(userName, password, personId);
            // نحتاج ستورد بروسيجر تضيف بيانات يوزر جديد مع البيرسن داتا
            else if (person != null)
            {
                Person = person;
                Id = await clsUsersData.AddNewUsersAsync(userName, password, null, person.FullName, person.NationalNum, person.PhoneNumber, person.Address);
            }
            return Id.HasValue;
        }

        public async Task<bool> AddAsync(bool addNewPerson = false)
        {
            // نحتاج ستورد بروسيجر تضيف يوزر موجودة بياناته بالنظام
            if (!addNewPerson)
                Id = await clsUsersData.AddNewUsersAsync(UserName, Password, Person.Id);
            // نحتاج ستورد بروسيجر تضيف بيانات يوزر جديد مع البيرسن داتا
            else 
                Id = await clsUsersData.AddNewUsersAsync(UserName, Password, null, Person.FullName, Person.NationalNum, Person.PhoneNumber, Person.Address);
            return Id.HasValue;
        }

        public async Task<bool> UpdateUserAsync()
            => await clsUsersData.UpdateUsersAsync(Id, UserName, Password, IsActive);

        public async Task<bool> UpdateUserWithPersonAsync()
            => await clsUsersData.UpdateUsersAsync(Id, UserName, Password, IsActive, Person.FullName, Person.NationalNum, Person.PhoneNumber, Person.Address);
        
        public static async Task<bool> DeleteUserByIdAsync(int userId)
            => await clsUsersData.DeleteUserByIdAsync(userId);

        private static clsUsers __GetUserData(ref Dictionary<string, object> dict)
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
