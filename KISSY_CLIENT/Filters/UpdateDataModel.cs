using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ERP_API.Filters
{
    public static class UpdateDataModel
    {
        public static object UpdateData(object objold, object objnew)
        {
            foreach (var propertyInfo in objold.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                string propertyName = propertyInfo.Name;
                var value = propertyInfo.GetValue(objold, null);
                if (propertyName.ToUpper() != "ID")
                {
                    foreach (var propertyInfo2 in objnew.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        string propertyName2 = propertyInfo2.Name;
                        var value3 = propertyInfo2.GetValue(objnew, null);
                        if ((propertyName == propertyName2) && (value != value3))// Thỏa 2 điều kiện, cùng tên Fiel và khác dữ liệu
                        {
                            propertyInfo.SetValue(objold, value3);
                        }
                    }
                }


            }
            return objold;
        }
        public static object CreateData(object objold, object objnew)
        {
            foreach (var propertyInfo in objold.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                string propertyName = propertyInfo.Name;
                var value = propertyInfo.GetValue(objold, null);
                foreach (var propertyInfo2 in objnew.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    string propertyName2 = propertyInfo2.Name;
                    var value3 = propertyInfo2.GetValue(objnew, null);
                    if ((propertyName == propertyName2) && (value != value3))// Thỏa 2 điều kiện, cùng tên Fiel và khác dữ liệu
                    {
                        propertyInfo.SetValue(objold, value3);
                    }
                }


            }
            return objold;
        }
    }
}