using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;

namespace ShuttleOffServiceAjax
{
    public class UserDetails
    {

        public int UserID { get; set; }
        public string EmailAdd { get; set; }
        public string UserPW { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public DateTime? DateCreated { get; set; }
    }

    public static class DBUtils   // database fetch usable methods (for nullable rows)
    {
        public static string SafeGetString(this SqlDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
                return reader.GetString(index);
            return string.Empty;
        }

        public static int SafeGetInt16(this SqlDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
                return reader.GetInt16(index);
            return 0;
        }

        public static int SafeGetByte(this SqlDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
                return reader.GetByte(index);
            return 0;
        }

        public static int SafeGetInt32(this SqlDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
                return reader.GetInt32(index);
            return 0;
        }

        public static TimeSpan? SafeGetTime(this SqlDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
                return reader.GetTimeSpan(index);
            return null;
        }

        public static DateTime SafeGetDateTime(this SqlDataReader reader, int index)
        {
            DateTime dat = new DateTime();

            if (!reader.IsDBNull(index))
                return reader.GetDateTime(index);
            return dat;
        }

        public static object ToDbParameter<T>(this T? value)
        where T : struct
        {
            object dbValue = value;
            if (dbValue == null)
            {
                dbValue = DBNull.Value;
            }
            return dbValue;
        }
        public static object ToDbParameter(this object value)
        {
            object dbValue = value;
            if (dbValue == null)
            {
                dbValue = DBNull.Value;
            }
            return dbValue;
        }
    }
}