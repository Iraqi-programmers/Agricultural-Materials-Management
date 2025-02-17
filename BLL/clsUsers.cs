using DAL;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;

namespace BLL
{
    // Create By Abu Sanad
    public class clsUsers : clsPerson 
    {
       
        public int? UserID { get; private set; }
        public int? PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }


        private clsUsers(int userID,int personID,  string userName, string password, bool isActive,
                         string FullName,string No,string PhoneNum,string Address)
            :base(FullName, No, PhoneNum,Address)
        {
            this.UserID = userID;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            base.FullName = FullName;
            base.NationalNum = No;
            base.PhoneNumber = PhoneNum;
            base.Address = Address;
            _mode = enMode.Update;
        }

        public clsUsers(string UserName,string Password,bool IsActive, string FullName, string No, string PhoneNum, string Address)
             : base(FullName, No, PhoneNum, Address)
        {
            
            this.UserID = null;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            base.FullName = FullName;
            base.NationalNum = No;
            base.PhoneNumber = PhoneNum;
            base.Address = Address;

            _mode = enMode.AddNew;
        }

      
        public static async Task<clsUsers?> GetUserByID(int userID)
        {
            try
            {
                var obj = await clsUsersData.GetUserByIDAsync(userID);

                if (obj != null)
                {
                    int personID = Convert.ToInt32(obj[1]);
                    var person = await clsPerson.FindAsync(personID,userID);

                    return new clsUsers(
                        Convert.ToInt32(obj[0]), personID, obj[2]?.ToString() ?? "", obj[3]?.ToString() ?? "", Convert.ToBoolean(obj[4]),
                        person.FullName.ToString() ?? "", person.NationalNum.ToString(), person.PhoneNumber.ToString(), person.Address ?? ""
                        
                    );
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

      
        public static async Task<bool> DeleteUser(int userID)
        {
            try
            {
                if (userID <= 0)
                    throw new ArgumentException("User ID غير صالح");

                return await clsUsersData.DeleteUserByIDAsync(userID,null);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentEx:" + ex);
                return false;
            }
            catch (Exception exo)
            {
                Console.WriteLine("Erorr:" + exo);
                return false;
            }
        }

        public static async Task<DataTable?> GetAllUsers()
        {
            return await clsUsersData.GetAllUsersAsync();
        }

        public static bool AuthenticateUser(string userName, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("يجب إدخال اسم المستخدم وكلمة المرور");

                return clsUsersData.CheckIfUserNameAndPasswordExisst(userName, password) != null;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentEx:" + ex);
                return false;
            }
            catch (Exception exo)
            {
                Console.WriteLine("Erorr:" + exo);
                return false;
            }
        }

        
        [Description("This Function Adds Person First and Then User Second.")]
        public async Task<bool> AddNew()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                    throw new ArgumentException("بيانات المستخدم غير مكتملة");

                

                this.UserID = await clsUsersData.AddNewUsers(this.UserName, this.Password, this.IsActive,base.FullName,base.NationalNum,base.PhoneNumber,base.Address,null);

                return this.UserID.HasValue;
            }
            catch (ArgumentException ex)
            {
                
                Console.WriteLine($"خطأ: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"خطأ غير متوقع: {ex.Message}");
                return false;
            }
        }

       
        [Description("This Function Update User.")]
        public async Task<bool> Update()
        {
            try
            {
                if (UserID <= 0 || string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                    throw new ArgumentException("بيانات المستخدم غير مكتملة");

               

                return await clsUsersData.UpdateUsers(this.UserID, this.UserName, this.Password, this.IsActive,null);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentEx:" + ex);
                return false;
            }
            catch (Exception exo)
            {
                Console.WriteLine("Erorr:" + exo);
                return false;
            }
        
           
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
