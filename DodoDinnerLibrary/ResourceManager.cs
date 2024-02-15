using DodoDinnerLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodoDinnerLibrary
{
    public class ResourceManager
    {
        public static Icon Favicon
        {
            get
            {
                return Resources.Favicon;
            }
        }

        public static string PathDB = "DodoDatabase.db";
    }
}
