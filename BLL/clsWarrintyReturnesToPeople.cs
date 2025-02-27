using BLL.Product;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //Create By Abu Sanad

    public class clsWarrintyReturnesToPeople :absClassesHelperBasc
    {
        //يجب التعديل على هذا الكلاس
        public clsStocks stockinfo { get; set; }
       // public  clsPerson PersonInfo { get; set; }
        public  clsUsers UserInfo { get; set; }

        public clsWarrintyReturnesToPeople(clsStocks stockInfo, clsUsers userInfo)
        {
            this.stockinfo = stockInfo;
           // PersonInfo = personInfo;
            UserInfo = userInfo;
        }

        private clsWarrintyReturnesToPeople(int warrantyRetID,clsUsers userInfo)
        {
             Id = warrantyRetID;
           // this.PersonInfo = person;
            this.UserInfo = userInfo;
        }


        public static async Task<clsWarrintyReturnesToPeople?> GetByID(int WarrintyReturnesID)
        {
            var obj = await clsWarrintyReturnesToPeopleData.GetByID(WarrintyReturnesID);
            if (obj != null) return null;
            else
                return __FetchWarrintyReturnesData(ref obj);
        }

        public static async Task<bool> DeleteAsync(int WarrintyReturnesID) => await clsWarrintyReturnesToPeopleData.Delete(WarrintyReturnesID);

        public  async Task<bool> UpdateAsync(int warrantyReturnedID) => await clsWarrintyReturnesToPeopleData.Update(warrantyReturnedID, UserInfo.Person.Id, UserInfo.Id);

        public static async Task<DataTable?> GetAllAsync() => await clsWarrintyReturnesToPeopleData.GetAll();

        public async Task<bool> AddAsync()
        {
            var Dic = await clsWarrintyReturnesToPeopleData.AddNew(Id, -1, -1, UserInfo.Id);
            if (Dic == null) return false;
            else
            {
                Id = (int)Dic["warrantyReturnedID"];
               
            }
            return true;
        }
        //public async Task<bool> UpdateAsync()
        //=> await clsWarrintyReturnesToPeopleData.Update(Id);




        //التعديل هنا بعد اكتمال كلاس الارجاع للمخزن
        private static clsWarrintyReturnesToPeople __FetchWarrintyReturnesData(ref Dictionary<string, object> dict)
        {
            return new clsWarrintyReturnesToPeople(
                (int)dict["WarrantyReturnedID"],
                  clsUsers.__FetchUserData(ref dict)

            );
        }
    }
}
