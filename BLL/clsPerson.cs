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

        public static async Task<clsPerson?> GetByIdAsync(int personId)
        {
            var data = await clsPersonData.GetByIdAsync(personId);
            if (data == null) return null;
            return FetchPersonData(ref data);
        }
        public static async Task<clsPerson?> GetByFullNameAsync(string fullName)
        {
            var data = await clsPersonData.GetByFullNameAsync(fullName);
            if (data == null) return null;
            return FetchPersonData(ref data);
        }
        public static async Task<clsPerson?> GetByNationalNumAsync(string nationalNum)
        {
            var data = await clsPersonData.GetByNationalNumAsync(nationalNum);
            if (data == null) return null;
            return FetchPersonData(ref data);
        }

        public static async Task<Dictionary<string, object>?> GetFullDataByIdAsync(int personId) => await clsPersonData.GetFullDataByIdAsync(personId);
        public static async Task<Dictionary<string, object>?> GetFullDataByFullNameAsync(string fullName) => await clsPersonData.GetFullDataByFullNameAsync(fullName);
        public static async Task<Dictionary<string, object>?> GetFullDataPhoneNumAsync(string phoneNum) => await clsPersonData.GetFullDataPhoneNumAsync(phoneNum);

        public static async Task<DataTable?> GetAllAsDataTableAsync() => await clsPersonData.GetAllAsDataTableAsync();
        public static async Task<List<Dictionary<string, object>>?> GetAllAsListAsync() => await clsPersonData.GetAllAsListAsync();

        public static async Task<bool> IsExistByIdAsync(int personId) => await clsPersonData.IsExistAsync(personId);
        public static async Task<bool> IsExistByFullNameAsync(string fullName) => await clsPersonData.IsExistByFullNameAsync(fullName);
        public static async Task<bool> IsExistByNationalNumAsync(string nationalNum) => await clsPersonData.IsExistByNationalNumAsync(nationalNum);

        public async Task<bool> UpdateAsync() => await clsPersonData.UpdateAsync(Id, FullName, NationalNum, PhoneNumber, Address);

        public static async Task<bool> DeleteByIdAsync(int? personId) => await clsPersonData.DeleteByIdAsync(personId);
        public static async Task<bool> DeleteByFullNameAsync(string fullName) => await clsPersonData.DeleteByFullNameAsync(fullName);
        public static async Task<bool> DeleteByNationalNumAsync(string nationalNum) => await clsPersonData.DeleteByNationalNumAsync(nationalNum);

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
