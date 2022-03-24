using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Utilities
{
   public class Function
    {
        public static int? GetInt(object _object)
        {
            int? result = null;
            if ((_object == null) || (_object == DBNull.Value))
                return result;
            try
            {
                result = Convert.ToInt32(_object);
            }
            catch
            {
                result = null;
            }
            return result;
        }

        public static double NVL_NUM_DOUBLE_NEW(object str)
        {
            if ((str != System.DBNull.Value) && (str != null))
            {
                if (str.ToString().Trim().Equals("") == false)
                {
                    return double.Parse(str.ToString());
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }


        public static decimal NVL_NUM_DECIMAL_NEW(object str)
        {
            if ((str != System.DBNull.Value) && (str != null))
            {
                if (str.ToString().Trim().Equals("") == false)
                {
                    return decimal.Parse(str.ToString());
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }


        public static int NVL_NUM_NT_NEW(object str)
        {
            if ((str != System.DBNull.Value) && (str != null))
            {
                if (str.Equals("") == false)
                {
                    return int.Parse(str.ToString());
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }


    }
}
