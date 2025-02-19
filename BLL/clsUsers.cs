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

        private clsUsers(int userId, string userName, string password, bool isActive, clsPerson person)
        {
            Id = userId;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            Person.Id = person.Id;  
            Person.FullName = person.FullName;
            Person.NationalNum = person.NationalNum;
            Person.PhoneNumber = person.PhoneNumber;
            Person.Address = person.Address;
             _mode = enMode.Update;
        }

        public clsUsers()
        {
            //Person = new clsPerson();
            _mode = enMode.AddNew;
        }

        // هاي الطريقة اكثر اختصار 
        public static async Task<clsUsers?> GetUserByID(int userId)
        {
            var dict = await clsUsersData.GetUserByIDAsync(userId);
            if (dict == null) 
                return null;

            int GetInt(string key) => dict.TryGetValue(key, out var value) && value != null ? Convert.ToInt32(value) : 0;
            string GetString(string key) => dict.TryGetValue(key, out var value) ? value?.ToString() ?? string.Empty : string.Empty;
            bool GetBool(string key) => dict.TryGetValue(key, out var value) && value != null && Convert.ToBoolean(value);

            return new clsUsers(
                GetInt("UserID"),
                GetString("UserName"),
                GetString("Password"),
                GetBool("IsActive"),
                new clsPerson(
                    GetInt("PersonID"),
                    GetString("FullName"),
                    GetString("NationalNum"),
                    GetString("PhoneNumber"),
                    GetString("Address")
                )
            );
        }

        //public static async Task<clsUsers?> GetUserByID(int userId)
        //{
        //    Dictionary<string, object>? dict = await clsUsersData.GetUserByIDAsync(userId);

        //    if (dict != null)
        //    {
        //        dict.TryGetValue("UserID", out object? _id);
        //        dict.TryGetValue("UserName", out object? _userName);
        //        dict.TryGetValue("Password", out object? _password);
        //        dict.TryGetValue("IsActive", out object? _isActive);
        //        dict.TryGetValue("PersonID", out object? _personId);
        //        dict.TryGetValue("FullName", out object? _fullName);
        //        dict.TryGetValue("NationalNum", out object? _nationalNum);
        //        dict.TryGetValue("PhoneNumber", out object? _phoneNumber);
        //        dict.TryGetValue("Address", out object? _address);

        //        int UserId = _id != null ? Convert.ToInt32(_id) : 0;
        //        string userName = _userName?.ToString() ?? string.Empty;
        //        string password = _password?.ToString() ?? string.Empty;
        //        bool isActive = _isActive != null && Convert.ToBoolean(_isActive);
        //        int personId = _personId != null ? Convert.ToInt32(_personId) : 0;
        //        string fullName = _fullName?.ToString() ?? string.Empty;
        //        string nationalNum = _nationalNum?.ToString() ?? string.Empty;
        //        string phoneNumber = _phoneNumber?.ToString() ?? string.Empty;
        //        string address = _address?.ToString() ?? string.Empty;

        //        return new clsUsers(UserId, userName, password, isActive, new clsPerson(personId, fullName, nationalNum, phoneNumber, address));
        //    }
        //    return null;
        //}

        public static async Task<DataTable?> GetAllUsers()
           => await clsUsersData.GetAllUsersAsync();

        public static bool AuthenticateUser(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("يجب إدخال اسم المستخدم وكلمة المرور");

            return clsUsersData.CheckIfUserNameAndPasswordExisst(userName, password) != null;
        }

        public static async Task<bool> DeleteUser(int userID)
        {
            if (userID <= 0)
                throw new ArgumentException("User ID غير صالح");
            return await clsUsersData.DeleteUserByIDAsync(userID);
        }

        /// <summary>
        ///  "This Function Adds Person First and Then User Second.")]
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddNew()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                throw new ArgumentException("بيانات المستخدم غير مكتملة");

            Id = await clsUsersData.AddNewUsers(this.UserName, this.Password, this.IsActive, Person.FullName, Person.NationalNum, Person.PhoneNumber, Person.Address);

            return Id.HasValue;

        }

        /// <summary>
        /// "This Function Update User.")]
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Update()
        {
            if (Id <= 0 || string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                throw new ArgumentException("بيانات المستخدم غير مكتملة");

            return await clsUsersData.UpdateUsers(Id, this.UserName, this.Password, this.IsActive);
        }

        public async Task<bool> Save()
        {
            bool result = false;

            switch (_mode)
            {
                case enMode.AddNew:
                    result = await AddNew();
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
