using System.Data;
using DAL;

namespace BLL
{
    public class clsPerson : absClassesHelperAdvance
    {
        public string FullName { get; set; }
        public string? NationalNum { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public clsPerson(string fullName, string? nationalNum = null, string? phoneNumber = null, string? address = null)
        {
            FullName = fullName;
            NationalNum = nationalNum;
            PhoneNumber = phoneNumber;
            Address = address;
            _mode = enMode.AddNew;
        }

        public clsPerson(int? personId, string fullName, string? nationalNum = null, string? phoneNumber = null, string? address = null)
        {
            Id = personId;
            FullName = fullName;
            NationalNum = nationalNum;
            PhoneNumber = phoneNumber;
            Address = address;
            _mode = enMode.Update;
        }

        public async Task<clsPerson?> GetPersonByIdAsync(int personId)
        {
            var data = await clsPersonData.GetPersonInfoByIdAsync(personId);
            if (data == null) return null;
            return __GetPersonData(ref data);
        }

        public async Task<clsPerson?> GetPersonByFullNameAsync(string fullName)
        {
            var data = await clsPersonData.GetPersonInfoByFullNameAsync(fullName);
            if (data == null) return null;
            return __GetPersonData(ref data);
        }

        public async Task<clsPerson?> GetPersonInfoByNationalNumAsync(string nationalNum)
        {
            var data = await clsPersonData.GetPersonInfoByNationalNumAsync(nationalNum);
            if (data == null) return null;
            return __GetPersonData(ref data);
        }

        public static async Task<DataTable?> GetAllPeopleAsDataTableAsync() => await clsPersonData.GetAllPersonsAsDataTableAsync();
        public static async Task<List<Dictionary<string, object>>?> GetAllPeopleAsListAsync() => await clsPersonData.GetAllPersonsAsListAsync();

        public static async Task<bool> IsPersonExistByIdAsync(int personId) => await clsPersonData.IsPersonExistAsync(personId);
        public static async Task<bool> IsPersonExistByFullNameAsync(string fullName) => await clsPersonData.IsPersonExistByFullNameAsync(fullName);
        public static async Task<bool> IsPersonExistByNationalNumAsync(string nationalNum) => await clsPersonData.IsPersonExistByNationalNumAsync(nationalNum);

        public static async Task<bool> AddAsync()
            => await clsPersonData.AddNewPersonAsync();

        //public async Task<bool> SaveAsync(int userId)
        //{
        //    if (_mode == enMode.AddNew)
        //    {
        //        // نحتاج بداخل الستورد بروسيجر نجيك انو ماموجد حتى نضيفه
        //        var newId = await clsPersonData.AddNewPersonAsync(FullName, NationalNum, PhoneNumber, Address, userId);
        //        if (newId.HasValue)
        //        {
        //            Id = newId;
        //            _mode = enMode.Update;
        //            return true;
        //        }
        //        return false;
        //    }
        //    else
        //        return await clsPersonData.UpdatePersonDataAsync(Id, FullName, NationalNum, PhoneNumber, Address, userId);
        //}

        public static async Task<bool> DeleteAsync(int personId, int userId) => await clsPersonData.DeletePersonByIDAsync(personId, userId);
        public static async Task<bool> DeleteByFullNameAsync(string fullName, int userId) => await clsPersonData.DeletePersonByFullNameAsync(fullName, userId);
        public static async Task<bool> DeleteByNationalNumAsync(string nationalNum, int userId) => await clsPersonData.DeletePersonByNationalNumAsync(nationalNum, userId);

        public static async Task<bool> DeleteMultipleAsync(List<int> personIDs, int userId) => await clsPersonData.DeleteMultiplePersonsAsync(personIDs, userId);

        private static clsPerson __GetPersonData(ref Dictionary<string, object> dict)
        {
            int? personId = (int)dict["PersonID"];
            string fullName = (string)dict["FullName"];
            string? nationalNum = dict["NationalNum"] == null ? null : (string)dict["NationalNum"];
            string? phoneNumber = dict["PhoneNumber"] == null ? null : (string)dict["PhoneNumber"];
            string? address = dict["Address"] == null ? null : (string)dict["Address"];

            return new clsPerson(personId, fullName, nationalNum, phoneNumber, address);
        }
    }
}
