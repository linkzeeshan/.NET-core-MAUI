using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeApp
{
    public static class Global
    {
        public static List<string> sTags = new List<string>();
        public static int iValidCode;

        public static Int32 ValidCode(Int32 Code)
        {
            Code = Code / 2;
            Code = (Code + 587123) / 3;
            return Code;
        }
    }
}
