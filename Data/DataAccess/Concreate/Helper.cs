using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concreate
{
    public static class Helper
    {
        static int outValue2 = 0;
        static DateTime outValue3 = new DateTime(2000, 01, 01);
        public static int ConInt(this object param)
        {
            return Convert.ToInt32(param);
        }

        public static int ConInt(this string param)
        {
            if (!int.TryParse(param.ToString(), out outValue2))
            {
                return 0;
            }
            return int.Parse(param);
        }

        public static bool ConBool(this object param)
        {

            return Convert.ToBoolean(param);
        }

        public static DateTime ConDate(this object param)
        {
            if (DateTime.TryParse(param.ToString(), out _))
            {
                return Convert.ToDateTime(param);
            }
            return DateTime.Now;
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
