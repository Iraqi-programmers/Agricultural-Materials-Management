using Microsoft.Data.SqlClient;
using MyLib_DotNet.DatabaseExecutor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class clsUsersData
    {
        public static async Task<int?> AddNewUsers(int PersonID,string UserName,string Password,bool IsActive)
        {
            SqlParameter[] prameter =
            {
                new SqlParameter("@PersonID",PersonID),
                new SqlParameter("@UserName",UserName),
                new SqlParameter("@Password",Password),
                new SqlParameter("@IsActive",IsActive)
            };
           
           return await CRUD.AddAsync("SP_AddUser",prameter, CommandType.StoredProcedure);

        }

        public static async Task<bool>UpdateUsers(int UserID,string UserName,string Password,bool IsActive)
        {
            SqlParameter[] Prameter =
            {
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@UserName",UserName),
                new SqlParameter("@Password",Password),
                new SqlParameter("@IsActive",IsActive)
               
            };

            return await CRUD.UpdateAsync("SP_UpdateUser", Prameter, CommandType.StoredProcedure);
        }

        public static async Task<bool>DeleteUsers(int UserID)
        {
          return await  CRUD.DeleteAsync("SP_DeleteUser","UserID",UserID,CommandType.StoredProcedure);
        }

        //public static async Task<object>GetUserByID()
        //{

        //}

        public static async Task<DataTable?>GetAllUsers()
        {
            return await CRUD.GetAllAsDataTableAsync("",null, CommandType.StoredProcedure);
        }

       


    }
}
