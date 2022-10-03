using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityWpf
{
    internal static class MyEnvironment
    {
        internal static string GetBaseUrl()
        {
            return "https://localhost:7080/api";
            //return "https://pekan-dotnet-university.herokuapp.com/api";
        }
    }
}
