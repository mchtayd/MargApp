using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Shared
{
    public static class Helper
    {
        public static int ConInt(this object param)
        {
            if (int.TryParse(param.ToString(), out _))
            {
                return Convert.ToInt32(param);
            }
            return 0;
        }

        public static int ConInt(this string param)
        {
            if (int.TryParse(param, out _))
            {
                return int.Parse(param);
            }
            return 0;
        }

        public static DateTime ConDate(this object param)
        {
            if (DateTime.TryParse(param.ToString(), out _))
            {
                return Convert.ToDateTime(param);
            }
            return DateTime.Now.ToLocalTime();
        }

        public static double ConDouble(this object param)
        {
            if (double.TryParse(param.ToString(), out _))
            {
                return Convert.ToDouble(param);
            }
            return 0;
        }

        public static bool ConBool(this object param)
        {
            if (bool.TryParse(param.ToString(), out _))
            {
                return Convert.ToBoolean(param);
            }
            return false;
        }
            
        public static string GetFormattedTimeValue(string timeValue)
        {
            //If exists '.' character, it means including day info
            if (timeValue.Contains('.'))
            {
                int spaceIndex = timeValue.IndexOf(".");
                int day = timeValue.Substring(0, spaceIndex).ConInt();
                int hour = timeValue.Substring(spaceIndex + 1, 2).ConInt();
                hour += day * 24;
                timeValue = timeValue.Replace(timeValue.Substring(0, spaceIndex + 3), hour.ToString());
            }
            return timeValue;
        }

        public static DataTable ToDataTable<T>(this List<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
