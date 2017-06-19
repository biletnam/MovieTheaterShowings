using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class DataValidator
    {
        public static bool is_null_empty_or_zero(Object[] testValues)
        {
            bool output = false;
            foreach(Object value in testValues){
                if (value.GetType() == typeof(string))
                {
                    if (string.IsNullOrEmpty(value as string)) {
                        output = true;
                    }
                }
                else if (value.GetType() == typeof(int))
                {
                    if ((int)value == 0)
                    {
                        output = true;
                    }
                }
                else if (value.GetType() == typeof(DateTime))
                {
                    if ((DateTime)value == DateTime.MinValue) {
                        output = true;
                    }
                }
            }
            return output;
        }
    }
}
