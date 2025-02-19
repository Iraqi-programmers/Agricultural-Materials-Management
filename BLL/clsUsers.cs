using DAL;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;

namespace BLL
{
    // Create By Abu Sanad
    public class clsUsers : absClassesHelper 
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsPerson Person { get; set; }

        private clsUsers(int userID, string userName, string password, bool isActive)
            
        {
            Id = userID;
            UserName = userName;
            Password = password;
            IsActive = isActive;
           
            _mode = enMode.Update;
        }

        public clsUsers()
        {
            Person = new clsPerson();
            _mode = enMode.AddNew;
        }

        public static async Task<clsUsers?> GetUserByID(int userID)
        {
            try
            {
                var obj = await clsUsersData.GetUserByIDAsync(userID);

                if (obj != null)
                {
                   

                    //return new clsUsers(
                    //    Convert.ToInt32(obj[0]), personID, obj[2]?.ToString() ?? "", obj[3]?.ToString() ?? "", Convert.ToBoolean(obj[4]),
                    //    person.FullName.ToString() ?? "", person.NationalNum.ToString(), person.PhoneNumber.ToString(), person.Address ?? ""
                        
                    //);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentEx:" + ex);
                return null;
            }
            catch (Exception exo)
            {
                Console.WriteLine("Erorr:" + exo);
                return null;
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
