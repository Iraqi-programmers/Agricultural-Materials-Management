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

        private clsUsers(int userID, string userName, string password, bool isActive, clsPerson person)
        {
            Id = userID;
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
            Person = new clsPerson();
            _mode = enMode.AddNew;
            /*
             new SqlParameter("@Password",password),
                new SqlParameter("@IsActive",isActive),
                new SqlParameter("@FullName",FullName),
                new SqlParameter("@NationalNum",No),
                new SqlParameter("@PhoneNumber",PhoneNum),
                new SqlParameter("@Address",Addrees)
             */
        }

        public static async Task<clsUsers?> GetUserByID(int userId)
        {
            Dictionary<string, object>? dict = await clsUsersData.GetUserByIDAsync(userId);

            if (dict != null)
            {
                dict.TryGetValue("UserID", out object? Id);
                dict.TryGetValue("UserName", out object? UserName);
                dict.TryGetValue("Password", out object? Password);
                dict.TryGetValue("IsActive", out object? IsActive);
                dict.TryGetValue("FullName", out object? FullName);
                dict.TryGetValue("NationalNum", out object? NationalNum);
                dict.TryGetValue("PhoneNumber", out object? PhoneNumber);
                dict.TryGetValue("Address", out object? Address);

                int userId = Id != null ? Convert.ToInt32(Id) : 0;
                string userName = UserName?.ToString() ?? string.Empty;
                string password = Password?.ToString() ?? string.Empty;
                bool isActive = (bool)IsActive;
                string fullName = FullName?.ToString() ?? string.Empty;
                string nationalNum = NationalNum?.ToString() ?? string.Empty;
                string phoneNumber = PhoneNumber?.ToString() ?? string.Empty;
                string address = Address?.ToString() ?? string.Empty;

                return this;
            }

            return null;
        }


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
