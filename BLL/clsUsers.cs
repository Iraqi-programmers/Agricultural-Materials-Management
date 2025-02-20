using System.Data;
using DAL;

namespace BLL
{
    // Create By Abu Sanad
    public class clsUsers : absClassesHelper
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsPerson Person { get; set; }

        //private clsUsers(string userName, string password, int? personId)
        //{
        //    UserName = userName;
        //    Password = password;
        //    IsActive = true;
        //}

        private clsUsers(string userName, string password, clsPerson person)
        {
            UserName = userName;
            Password = password;
            IsActive = true;
            Person = person;
        }

        private clsUsers(int userId, string userName, string password, bool isActive, clsPerson person)
        {
            Id = userId;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            Person = person;
            //_mode = enMode.Update;
        }

        public static async Task<clsUsers?> GetUserByID(int userId)
        {
            Dictionary<string, object>? dict = await clsUsersData.GetUserByIDAsync(userId);
            if (dict == null)
                return null;

            int? userID = (int)dict["UserID"];
            string? userName = (string)dict["UserName"];
            string? password = (string)dict["Password"];
            bool isActive = (bool)dict["IsActive"];

            int? personId = (int)dict["PersonID"];
            string fullName = (string)dict["FullName"];
            string? nationalNum = dict["NationalNum"] == null ? null : (string)dict["NationalNum"];
            string? phoneNumber = dict["PhoneNumber"] == null ? null : (string)dict["PhoneNumber"];
            string? address = dict["Address"] == null ? null : (string)dict["Address"];

            clsPerson person = new clsPerson(personId, fullName, nationalNum, phoneNumber, address);

            return new clsUsers(userId, userName, password, isActive, person);
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

        public static async Task<DataTable?> GetAllUsers()
           => await clsUsersData.GetAllUsersAsync();

        public static async Task<clsUsers?> GetUserByUsernameAndPasswordAsync(string userName, string password)
        {
            var dict = clsUsersData.GetUserByUserNameAndPasswordAsync(userName, password);
            if (dict == null) return null;

        }




        public async Task<bool> AddNewAsync(string userName, string password, int? personId = null, clsPerson? person = null)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("بيانات المستخدم غير مكتملة");

            if (personId != null)
                Id = await clsUsersData.AddNewUsersAsync(userName, password, personId);
            else if (person != null)
                Id = await clsUsersData.AddNewUsersAsync(userName, password, person.FullName, person.NationalNum, person.PhoneNumber, person.Address);

            return Id.HasValue;
        }

        public static async Task<bool> DeleteUserAsync(int userId)
            => await clsUsersData.DeleteUserByIDAsync(userId);
        
        /// <summary>
        /// "This Function Update User.")]
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Update()
        {
            if (Id <= 0 || string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                throw new ArgumentException("بيانات المستخدم غير مكتملة");

            return await clsUsersData.UpdateUsersAsync(Id, this.UserName, this.Password, this.IsActive);
        }

        public async Task<bool> Save()
        {
            bool result = false;

            switch (_mode)
            {
                case enMode.AddNew:
                    result = await AddNewAsync();
                    if (result)
                        _mode = enMode.Update;
                    break;

                case enMode.Update:
                    result = await Update();
                    break;

                default:
                    result = false;
                    break;
            }
            return result;
        }
    }
}
