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
            _id = null;
            FullName = fullName;
            NationalNum = nationalNum;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        private clsPerson(int personId, string fullName, string nationalNum, string phoneNumber, string address)
        {
            _id = personId;
            FullName = fullName;
            NationalNum = nationalNum;
            PhoneNumber = phoneNumber;
            Address = address;
            _mode = enMode.Update;
        }

        public static async Task<clsPerson?> FindAsync(int personId)
        {
            var data = await clsPersonData.GetPersonInfoByIDAsync(personId);
            return data != null ? new clsPerson(personId, (string)data[1], (string)data[2], (string)data[3], (string)data[4]) : null;
        }

        public static async Task<clsPerson?> FindAsync(string nationalNum)
        {
            var data = await clsPersonData.GetPersonInfoByNationalNumAsync(nationalNum);
            return data != null ? new clsPerson(Convert.ToInt32(data[0]), (string)data[1], (string)data[2], (string)data[3], (string)data[4]) : null;
        } 

        public static async Task<bool> IsPersonExistAsync(int personId) => await clsPersonData.IsPersonExistAsync(personId);
        public static async Task<bool> IsPersonExistAsync(string fullName) => await clsPersonData.IsPersonExistAsync(fullName);

        public async Task<bool> SaveAsync()
        {
            if (_mode == enMode.AddNew)
            {
                var data = await clsPersonData.GetPersonInfoByNationalNumAsync(NationalNum);
                if (data != null)
                {
                    _id = Convert.ToInt32(data[0]);
                    _mode = enMode.Update;
                    return true;
                }
                var newId = await clsPersonData.AddNewPersonAsync(FullName, NationalNum, PhoneNumber, Address);
                if (newId.HasValue)
                {
                    _id = newId;
                    _mode = enMode.Update;
                    return true;
                }
            }
            else
                return await clsPersonData.UpdatePersonDataAsync(_id, FullName, NationalNum, PhoneNumber, Address);
            return false;
        }

        public static async Task<bool> DeleteAsync(int personId) => await clsPersonData.DeletePersonByIDAsync(personId);
        public static async Task<bool> DeleteByFullNameAsync(string fullName) => await clsPersonData.DeletePersonByFullNameAsync(fullName);
        public static async Task<bool> DeleteByNationalNumAsync(string nationalNum) => await clsPersonData.DeletePersonByNationalNumAsync(nationalNum);

        public static async Task<bool> DeleteMultipleAsync(List<int> personIDs) => await clsPersonData.DeleteMultiplePersonsAsync(personIDs);

        public static async Task<DataTable?> GetAllPeopleAsDataTableAsync() => await clsPersonData.GetAllPersonsAsDataTableAsync();
        public static async Task<List<object[]>?> GetAllPeopleAsListAsync() => await clsPersonData.GetAllPersonsAsListAsync();
    }

}
