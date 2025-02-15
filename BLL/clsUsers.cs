using DAL;
using System.Data;

namespace BLL
{
    public class clsUsers
    {
        public enum Mod { AddNew, Update }

        public Mod mod = Mod.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }


        private clsUsers(int userID, int personID, string userName, string password, bool isActive)
        {
            UserID = userID;
            PersonID = personID;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            mod = Mod.Update;
        }

        public clsUsers()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = true;
            mod = Mod.AddNew;
        }

        public static async Task<clsUsers?> GetUserByID(int userID)
        {
            var obj = await clsUsersData.GetUserByID(userID);

            if (obj != null)
            {
                return new clsUsers(
                    Convert.ToInt32(obj[0]),
                    Convert.ToInt32(obj[1]),
                    obj[2]?.ToString() ?? "",
                    obj[3]?.ToString() ?? "",
                    Convert.ToBoolean(obj[4])
                );
            }
            return null;
        }

        public static async Task<clsUsers?> GetUserByUserName(string userName)
        {
            var obj = await clsUsersData.GetUserByUserName(userName);

            if (obj != null)
            {
                return new clsUsers(
                    Convert.ToInt32(obj[0]),
                    Convert.ToInt32(obj[1]),
                    obj[2]?.ToString() ?? "",
                    obj[3]?.ToString() ?? "",
                    Convert.ToBoolean(obj[4])
                );
            }
            return null;
        }

        public static async Task<bool> DeleteUser(int userID)
        {
            if (userID <= 0)
                throw new ArgumentException("User ID غير صالح");

            return await clsUsersData.DeleteUsers(userID);
        }

        public static async Task<DataTable?> GetAllUsers()
        {
            return await clsUsersData.GetAllUsers();
        }

        public static bool AuthenticateUser(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("يجب إدخال اسم المستخدم وكلمة المرور");

            return  clsUsersData.CheckIfUserNameAndPasswordExisst(userName, password) != null;
        }

        private async Task<bool> AddNew()
        {
            if (PersonID <= 0 || string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                throw new ArgumentException("بيانات المستخدم غير مكتملة");

            this.UserID = await clsUsersData.AddNewUsers(this.PersonID, this.UserName, this.Password, this.IsActive) ?? -1;
            return this.UserID != -1;
        }

        private async Task<bool> Update()
        {
            if (UserID <= 0 || PersonID <= 0 || string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                throw new ArgumentException("بيانات المستخدم غير مكتملة");

            return await clsUsersData.UpdateUsers(this.UserID, this.UserName, this.Password, this.IsActive);
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
