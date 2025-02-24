﻿using System.Data;
using DAL;

namespace BLL
{
    public class clsPerson : absClassesHelperBasc
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
        }

        public clsPerson(int? personId, string fullName, string? nationalNum = null, string? phoneNumber = null, string? address = null)
        {
            Id = personId;
            FullName = fullName;
            NationalNum = nationalNum;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public async Task<bool> AddAsync()
        {
            Id = await clsPersonData.AddAsync(FullName, NationalNum, PhoneNumber, Address);
            return Id != null;
        }

        public static async Task<clsPerson?> GetPersonByIdAsync(int personId)
        {
            var data = await clsPersonData.GetPersonInfoByIdAsync(personId);
            if (data == null) return null;
            return FetchPersonData(ref data);
        }
        public static async Task<clsPerson?> GetPersonByFullNameAsync(string fullName)
        {
            var data = await clsPersonData.GetPersonInfoByFullNameAsync(fullName);
            if (data == null) return null;
            return FetchPersonData(ref data);
        }
        public static async Task<clsPerson?> GetPersonInfoByNationalNumAsync(string nationalNum)
        {
            var data = await clsPersonData.GetPersonInfoByNationalNumAsync(nationalNum);
            if (data == null) return null;
            return FetchPersonData(ref data);
        }

        public static async Task<Dictionary<string, object>?> GetPersonFullDataByIdAsync(int personId) => await clsPersonData.GetPersonFullDataByIdAsync(personId);
        public static async Task<Dictionary<string, object>?> GetPersonFullDataByFullNameAsync(string fullName) => await clsPersonData.GetPersonFullDataByFullNameAsync(fullName);
        public static async Task<Dictionary<string, object>?> GetPersonFullDataPhoneNumAsync(string phoneNum) => await clsPersonData.GetPersonFullDataPhoneNumAsync(phoneNum);

        public static async Task<DataTable?> GetAllAsDataTableAsync() => await clsPersonData.GetAllAsDataTableAsync();
        public static async Task<List<Dictionary<string, object>>?> GetAllAsListAsync() => await clsPersonData.GetAllAsListAsync();

        public static async Task<bool> IsPersonExistByIdAsync(int personId) => await clsPersonData.IsPersonExistAsync(personId);
        public static async Task<bool> IsPersonExistByFullNameAsync(string fullName) => await clsPersonData.IsPersonExistByFullNameAsync(fullName);
        public static async Task<bool> IsPersonExistByNationalNumAsync(string nationalNum) => await clsPersonData.IsPersonExistByNationalNumAsync(nationalNum);

        public async Task<bool> UpdateAsync() => await clsPersonData.UpdateAsync(Id, FullName, NationalNum, PhoneNumber, Address);

        public static async Task<bool> DeleteByIdAsync(int? personId) => await clsPersonData.DeletePersonByIdAsync(personId);
        public static async Task<bool> DeleteByFullNameAsync(string fullName) => await clsPersonData.DeletePersonByFullNameAsync(fullName);
        public static async Task<bool> DeleteByNationalNumAsync(string nationalNum) => await clsPersonData.DeletePersonByNationalNumAsync(nationalNum);

        public async Task<bool> DeleteAsync() => await DeleteByIdAsync(Id);
        
        public static clsPerson FetchPersonData(ref Dictionary<string, object> dict)
        {
            return new clsPerson(
                (int)dict["PersonID"],
                (string)dict["FullName"],
                dict["NationalNum"] as string,
                dict["PhoneNumber"] as string,
                dict["Address"] as string
            );
        }
    }
}
