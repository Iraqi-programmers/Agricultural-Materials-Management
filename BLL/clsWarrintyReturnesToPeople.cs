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
        public clsReturnedStocks Stockinfo { get; set; }
        public  clsUsers UserInfo { get; set; }

        public clsWarrintyReturnesToPeople(clsReturnedStocks stockInfo, clsUsers userInfo)
        {
            Id = null;
            this.Stockinfo = stockInfo;
            UserInfo = userInfo;
        }

        private clsWarrintyReturnesToPeople(int warrantyRetID, clsReturnedStocks stockInfo, clsUsers userInfo)
        {
             Id = warrantyRetID;
            this.Stockinfo = stockInfo;
            this.UserInfo = userInfo;
        }


        public static async Task<clsWarrintyReturnesToPeople?> GetByID(int warrintyReturnesID)
        {
            var obj = await clsWarrintyReturnesToPeopleData.GetByID(warrintyReturnesID);
            if (obj == null) return null;
            return __FetchWarrintyReturnesData(ref obj);
        }

        public static async Task<bool> DeleteAsync(int warrintyReturnesID) => await clsWarrintyReturnesToPeopleData.Delete(warrintyReturnesID);

        public  async Task<bool> UpdateAsync(int warrantyReturnedID) => await clsWarrintyReturnesToPeopleData.Update(warrantyReturnedID, UserInfo.Person.Id, UserInfo.Id);

        public static async Task<DataTable?> GetAllAsync() => await clsWarrintyReturnesToPeopleData.GetAll();

        public async Task<bool> AddAsync()
        {
            var Dic = await clsWarrintyReturnesToPeopleData.AddNew(Stockinfo.Id, UserInfo.Person.Id, UserInfo.Id);
            if (Dic == null) return false;
            else
            {
                Id = (int)Dic["warrantyReturnedID"];
               
               
            }
            return true;
        }
        public async Task<bool> UpdateAsync()
        => await clsWarrintyReturnesToPeopleData.Update(Id,UserInfo.Person.Id,UserInfo.Id);

        internal static clsWarrintyReturnesToPeople __FetchWarrintyReturnesData(ref Dictionary<string, object> dict)
        {
            return new clsWarrintyReturnesToPeople(
                (int)dict["WarrantyReturnedID"],
                clsReturnedStocks.FetchReturnedStocksData(ref dict),
                  clsUsers.FetchUserData(ref dict)

            );
        }
    }
}
