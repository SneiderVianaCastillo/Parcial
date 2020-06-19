using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Covid
{
    public static class ConfigConnection
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static string ProviderName = ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName;
    }
}
