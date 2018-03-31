using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService
{
    public static class StringAdjustment
    {
        /// <summary>
        /// Makes all letters lowercase but the first letter uppercase
        /// </summary>
        /// <param name="StringToBeModified"></param>
        /// <returns></returns>
        public static string MakeFirstLetterUppercaseTheRestLowercase(string StringToBeModified)
        {
            if (StringToBeModified.Length == 0)
            {
                return StringToBeModified;
            }

            string newString = StringToBeModified;

            newString = newString.ToLower();

            string firstLetter = newString.Substring(0, 1);
            firstLetter = firstLetter.ToUpper();

            newString = newString.Replace(newString[0], firstLetter[0]);

            return newString;
        }
        /// <summary>
        /// Remove spaces before the first char and after the last char.
        /// </summary>
        /// <param name="StringToBeModified"></param>
        /// <returns></returns>
        public static string RemoveSpaces(string StringToBeModified)
        {
            string newString;

            newString = RemoveSpacesFromTheBegining(StringToBeModified);
            newString = RemoveSpacesFromTheEnd(newString);

            return newString;
        }

        private static string RemoveSpacesFromTheBegining(string StringToBeModified)
        {
            string newString;
            newString = StringToBeModified;

            while (newString.StartsWith(" ") && newString.Length > 0)
            {
                newString = newString.Substring(1);
            }

            return newString;
        }

        private static string RemoveSpacesFromTheEnd(string StringToBeModified)
        {
            string newString;
            newString = StringToBeModified;

            while (newString.EndsWith(" ") && newString.Length > 0)
            {
                newString = newString.Substring(0, newString.Length - 1);
            }

            return newString;
        }
    }
}