using System.Data;
using DAL;

namespace BLL
{
    public class clsPerson : absClassesHelper
    {
        public string FullName { get; set; }
        public string NationalNum { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public clsPerson(string fullName, string nationalNum, string phoneNumber, string address)
        {
            Id = null;
            FullName = fullName;
            NationalNum = nationalNum;
            PhoneNumber = phoneNumber;
            Address = address;
            _mode = enMode.AddNew;
        }

        private clsPerson(int personId, string fullName, string nationalNum, string phoneNumber, string address)
        {
            Id = personId;
            FullName = fullName;
            NationalNum = nationalNum;
            PhoneNumber = phoneNumber;
            Address = address;
            _mode = enMode.Update;
        }

        public static async Task<clsPerson?> FindAsync(int personId, int userId)
        {
            var data = await clsPersonData.GetPersonInfoByIDAsync(personId);
            return new clsPerson(personId, (string)data[1], (string)data[2], (string)data[3], (string)data[4]) ?? null;
        }

        public static async Task<clsPerson?> FindAsync(string nationalNum, int userId)
        {
            var data = await clsPersonData.GetPersonInfoByNationalNumAsync(nationalNum);
            return new clsPerson(Convert.ToInt32(data[0]), (string)data[1], nationalNum, (string)data[3], (string)data[4]) ?? null;
        } 

        public static async Task<bool> IsPersonExistAsync(int personId) => await clsPersonData.IsPersonExistAsync(personId);
        public static async Task<bool> IsPersonExistByFullNameAsync(string fullName) => await clsPersonData.IsPersonExistByFullNameAsync(fullName);
        public static async Task<bool> IsPersonExistByNationalNumAsync(string nationalNum) => await clsPersonData.IsPersonExistByNationalNumAsync(nationalNum);

        public async Task<bool> SaveAsync(int userId)
        {
            if (_mode == enMode.AddNew)
            {
                // نحتاج بداخل الستورد بروسيجر نجيك انو ماموجد حتى نضيفه
                var newId = await clsPersonData.AddNewPersonAsync(FullName, NationalNum, PhoneNumber, Address, userId);
                if (newId.HasValue)
                {
                    Id = newId;
                    _mode = enMode.Update;
                    return true;
                }
                return false;
            }
            else
                return await clsPersonData.UpdatePersonDataAsync(Id, FullName, NationalNum, PhoneNumber, Address, userId);
        }

        public static async Task<bool> DeleteAsync(int personId, int userId) => await clsPersonData.DeletePersonByIDAsync(personId, userId);
        public static async Task<bool> DeleteByFullNameAsync(string fullName, int userId) => await clsPersonData.DeletePersonByFullNameAsync(fullName, userId);
        public static async Task<bool> DeleteByNationalNumAsync(string nationalNum, int userId) => await clsPersonData.DeletePersonByNationalNumAsync(nationalNum, userId);

        public static async Task<bool> DeleteMultipleAsync(List<int> personIDs, int userId) => await clsPersonData.DeleteMultiplePersonsAsync(personIDs, userId);

        public static async Task<DataTable?> GetAllPeopleAsDataTableAsync() => await clsPersonData.GetAllPersonsAsDataTableAsync();
        public static async Task<List<object[]>?> GetAllPeopleAsListAsync() => await clsPersonData.GetAllPersonsAsListAsync();
    }
}
