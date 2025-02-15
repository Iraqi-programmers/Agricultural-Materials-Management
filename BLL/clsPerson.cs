using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public  class clsPerson
    {

        public int PersonID { get; set; }
        public string FullName { get; set; }
        public string NationalNum { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public clsPerson()
        {
            this.PersonID = -1;
            this.FullName = string.Empty;
            this.NationalNum = string.Empty;
            this.PhoneNumber = string.Empty;
            this.Address = string.Empty;
        }

        private clsPerson(int personID, string fullName, string nationalNum, string phoneNumber, string address)
        {
            this.PersonID = personID;
            this.FullName = fullName;
            this.NationalNum = nationalNum;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
        }

   
        public static async Task<clsPerson?> GetPersonByID(int personID)
        {
            try
            {
                var obj = await clsPersonData.GetPersonByID(personID);
                if (obj != null)
                {
                    return new clsPerson(
                        Convert.ToInt32(obj[0]),
                        obj[1]?.ToString() ?? "",
                        obj[2]?.ToString() ?? "",
                        obj[3]?.ToString() ?? "",
                        obj[4]?.ToString() ?? ""
                    );
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentEx:" + ex);
                return null;
            }
            catch (Exception exo)
            {
                Console.WriteLine("Erorr:" + exo);
                return null;
            }
            return null;
        }

      
        public async Task<bool> AddNew()
        {
            try
            {


                this.PersonID = await clsPersonData.AddPerson(this.FullName, this.NationalNum, this.PhoneNumber, this.Address) ?? -1;
                return this.PersonID > -1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentEx:" + ex);
                return false;
            }
            catch (Exception exo)
            {
                Console.WriteLine("Erorr:" + exo);
                return false;
            }
        }

      
        public async Task<bool> Update()
        {
            try
            {
                return await clsPersonData.UpdatePerson(this.PersonID, this.FullName, this.NationalNum, this.PhoneNumber, this.Address);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentEx:" + ex);
                return false;
            }
            catch (Exception exo)
            {
                Console.WriteLine("Erorr:" + exo);
                return false;
            }
        }

    }

}
