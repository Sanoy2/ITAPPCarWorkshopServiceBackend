using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.Authorization
{
    public static class DateTimeManager
    {
        public static DateTime FormatDatetimeReceived(string s)
        {
            DateTime datetime = new DateTime(
                Int32.Parse(s.Substring(10)),
                Int32.Parse(s.Substring(8, 2)),
                Int32.Parse(s.Substring(6, 2)),
                Int32.Parse(s.Substring(4, 2)),
                Int32.Parse(s.Substring(2, 2)),
                Int32.Parse(s.Substring(0, 2))
                );

            return datetime;
        }

        public static string FormatDatetimeToSend(DateTime dt)
        {
            string result = "";
            result = "00";
            result += CheckDateTimeFormatToSend(dt.Minute.ToString());
            result += CheckDateTimeFormatToSend(dt.Hour.ToString());
            result += CheckDateTimeFormatToSend(dt.Day.ToString());
            result += CheckDateTimeFormatToSend(dt.Month.ToString());
            result += dt.Year.ToString();

            return result;
        }

        private static string CheckDateTimeFormatToSend(string s)
        {
            string result = s;

            if (s.Length == 1)
            {
                result = "0" + result;
            }

            return result;
        }
    }
}