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
    public class clsPersonData
    {
        public static async Task<int?> AddPerson(string fullName, string nationalNum, string phoneNumber, string address)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@FullName", fullName),
            new SqlParameter("@NationalNum", nationalNum),
            new SqlParameter("@PhoneNumber", phoneNumber),
            new SqlParameter("@Address", address)
        };

            return await CRUD.AddAsync("SP_AddPeople", parameters, CommandType.StoredProcedure);
        }

        public static async Task<bool> UpdatePerson(int personId, string fullName, string nationalNum, string phoneNumber, string address)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@PersonID", personId),
            new SqlParameter("@FullName", fullName),
            new SqlParameter("@NationalNum", nationalNum),
            new SqlParameter("@PhoneNumber", phoneNumber),
            new SqlParameter("@Address", address)
        };

            return await CRUD.UpdateAsync("SP_UpdatePeople", parameters, CommandType.StoredProcedure);
        }

        public static async Task<bool> DeletePerson(int personId)
        {
            return await CRUD.DeleteAsync("SP_DeletePeople", "PersonID", personId, CommandType.StoredProcedure);
        }

        public static async Task<object[]?> GetPersonByID(int personId)
        {
            return await CRUD.GetByColumnValueAsync("SP_GetPeopleByID", "PersonID", personId, CommandType.StoredProcedure);
        }

        public static async Task<DataTable?> GetAllPersons()
        {
            return await CRUD.GetAllAsDataTableAsync("SP_GetALLPeople", null, CommandType.StoredProcedure);
        }

    }


}
