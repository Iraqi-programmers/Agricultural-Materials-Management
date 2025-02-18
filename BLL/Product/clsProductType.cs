﻿using DAL.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Yousif 
namespace BLL.Product
{
    public class clsProductType :absClassesHelper
    {
        string TypeName { set; get; }
        
        public clsProductType(string TypeName)
        {
            _mode = enMode.AddNew;
            Id = null;
            this.TypeName = TypeName;
        }

        private clsProductType(int TypeID,  string TypeName)
        {
            this.Id = TypeID;
            this.TypeName = TypeName;
            _mode = enMode.Update;
        }

        public static async Task<DataTable?> GetAllAsDataTableAsync()
        {
            return await clsProductTypeData.GetAllAsync();
        }


        public static async Task<clsProductType?> FindTypeAsync(int TypeID)
        {
            var data = await clsProductTypeData.FindByIDAsync(TypeID);

            return new clsProductType(TypeID, (string)data[1]) ?? null;
        }

        public async Task<bool> SaveAsync()
        {
            if (_mode == enMode.AddNew)
            {
                var newId = await clsProductTypeData.AddAsync(TypeName);
                if (newId.HasValue)
                {
                    Id = newId.Value;
                    _mode = enMode.Update;
                    return true;
                }
                else
                    return false;
            }
            else
                return await clsProductTypeData.UpdateAsync(this.Id, this.TypeName);
        }

        

    }
}
