using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonFramework.Helper
{
    public partial class StringConvert
    {

        public static List<string> StringToList(string charSeparatedList, char separator)
        {
            List<string> result = new List<string>();

            string[] resultArr = charSeparatedList.Split(separator);
            result = resultArr.ToList<string>();
            return result;

        }


    }
}
