using DAL;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;

namespace BLL
{
    public class clsUsers 
    {
        public enum Mod { AddNew, Update }
        public Mod mod = Mod.AddNew;

        public int? UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsPerson PersonInfo { get; set; }

        private clsUsers(int userID, clsPerson person, string userName, string password, bool isActive)
        {
            UserID = userID;
            PersonInfo = person;
            UserName = userName;
            Password = password;
            IsActive = isActive;

            mod = Mod.Update;
        }

        public clsUsers()
        {
            this.UserID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = true;
            this.PersonInfo = new clsPerson();
            mod = Mod.AddNew;
        }

      
        public static async Task<clsUsers?> GetUserByID(int userID)
        {
            try
            {
                var obj = await clsUsersData.GetUserByID(userID);

                if (obj != null)
                {
                    int personID = Convert.ToInt32(obj[1]);
                    var person = await clsPerson.GetPersonByID(personID);

                    return new clsUsers(
                        Convert.ToInt32(obj[0]),
                        person ?? new clsPerson(),
                        obj[2]?.ToString() ?? "",
                        obj[3]?.ToString() ?? "",
                        Convert.ToBoolean(obj[4])
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

                return await clsUsersData.DeleteUsers(userID);
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
            return await clsUsersData.GetAllUsers();
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

        private async Task<bool> AddNew()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                    throw new ArgumentException("بيانات المستخدم غير مكتملة");

                bool personAdded = await PersonInfo.AddNew();
                if (!personAdded) return false;

                this.UserID = await clsUsersData.AddNewUsers(PersonInfo.PersonID, this.UserName, this.Password, this.IsActive);

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

        private async Task<bool> Update()
        {
            try
            {
                if (UserID <= 0 || string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                    throw new ArgumentException("بيانات المستخدم غير مكتملة");

                bool personUpdated = await PersonInfo.Update();
                if (!personUpdated) return false;

                return await clsUsersData.UpdateUsers(this.UserID, this.UserName, this.Password, this.IsActive);
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
            return mod switch
            {
                Mod.AddNew => await AddNew(),
                Mod.Update => await Update(),
                _ => false
            };
        }

    }
}
