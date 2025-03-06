using System.Data;
using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;

namespace DAL
{
    public class clsUsersData
    {
        public static async Task<int?> AddAsync(string userName, string password, int? personId)
        {
            SqlParameter[] prameters =
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password),
                new SqlParameter("@PersonID", personId)
            };
            return await CRUD.AddAsync("SP_AddUsers", prameters);
        }

        public static async Task<Dictionary<string, object>?> GetByUserNameAndPasswordAsync(string userName, string password)
        {
            SqlParameter[] prameters =
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password)
            };
            return await CRUD.GetAsync("SP_GetAllUsers", prameters);
        }

        public static async Task<Dictionary<string, object>?> GetByIdAsync(int userId)
            => await CRUD.GetByColumnValueAsync("SP_GetAllUsers", "UserID", userId);

        public static async Task<Dictionary<string, object>?> GetByUserNameAsync(string userName) 
            => await CRUD.GetByColumnValueAsync("SP_GetAllUsers", "UserName", userName);

        public static async Task<DataTable?> GetAllAsync() => await CRUD.GetAllAsDataTableAsync("SP_GetAllUsers");

        public static async Task<bool> UpdateAsync(int? userId, string userName, string? password, bool isActive, string? fullName = null, string? nationalNum = null, string? phoneNumber = null, string? address = null)
        {
            SqlParameter[] prameters =
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password),
                new SqlParameter("@IsActive", isActive)
            };
            if (fullName != null) prameters.Append(new SqlParameter("@FullName", fullName));
            if (nationalNum != null) prameters.Append(new SqlParameter("@NationalNum", nationalNum));
            if (phoneNumber != null) prameters.Append(new SqlParameter("@PhoneNumber", phoneNumber));
            if (address != null) prameters.Append(new SqlParameter("@Address", address));

            return await CRUD.UpdateAsync("SP_UpdateUser", prameters);
        }

        public static async Task<bool> DeleteByIdAsync(int userId) => await CRUD.DeleteAsync("SP_DeleteUser", "UserID", userId);
    }

}

/*

-- إجراء مخزن للإضافة
CREATE PROCEDURE AddUser
    @UserName NVARCHAR(255),
    @Password NVARCHAR(255),
    @PersonID INT
AS
BEGIN
    INSERT INTO Users (UserName, Password, IsActive, PersonID)
    VALUES (@UserName, @Password, 1, @PersonID);
    
    SELECT SCOPE_IDENTITY() AS NewUserID;
END;

-- إجراء مخزن للتحديث
CREATE PROCEDURE UpdateUser
    @UserID INT,
    @UserName NVARCHAR(255),
    @Password NVARCHAR(255),
    @IsActive BIT,
    @FullName NVARCHAR(255),
    @NationalNum NVARCHAR(50) = NULL,
    @PhoneNumber NVARCHAR(50) = NULL,
    @Address NVARCHAR(255) = NULL
AS
BEGIN
    UPDATE Users
    SET UserName = @UserName,
        Password = @Password,
        IsActive = @IsActive
    WHERE UserID = @UserID;

    -- تحديث بيانات الشخص المرتبط
    UPDATE Persons
    SET FullName = @FullName,
        NationalNum = @NationalNum,
        PhoneNumber = @PhoneNumber,
        Address = @Address
    WHERE PersonID = (SELECT PersonID FROM Users WHERE UserID = @UserID);
END;

-- إجراء مخزن للحذف باستخدام ID
CREATE PROCEDURE DeleteUserById
    @UserID INT
AS
BEGIN
    DELETE FROM Users WHERE UserID = @UserID;
END;

-- إجراء مخزن لاسترجاع بيانات مستخدم حسب ID
CREATE PROCEDURE GetUserById
    @UserID INT
AS
BEGIN
    SELECT U.*, P.*
    FROM Users U
    JOIN Persons P ON U.PersonID = P.PersonID
    WHERE U.UserID = @UserID;
END;

-- إجراء مخزن لاسترجاع بيانات مستخدم حسب اسم المستخدم وكلمة المرور
CREATE PROCEDURE GetUserByUserNameAndPassword
    @UserName NVARCHAR(255),
    @Password NVARCHAR(255)
AS
BEGIN
    SELECT U.*, P.*
    FROM Users U
    JOIN Persons P ON U.PersonID = P.PersonID
    WHERE U.UserName = @UserName AND U.Password = @Password;
END;

-- إجراء مخزن لاسترجاع جميع المستخدمين
CREATE PROCEDURE GetAllUsers
AS
BEGIN
    SELECT U.*, P.*
    FROM Users U
    JOIN Persons P ON U.PersonID = P.PersonID;
END;

 */