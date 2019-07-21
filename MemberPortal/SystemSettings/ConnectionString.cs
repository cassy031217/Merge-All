using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MemberPortal.SystemSettings
{
    public sealed class ConnectionStrings
    {
        public static string Portal_CONNECTIONSTRING = ConfigurationManager.ConnectionStrings["Portal_CONNECTIONSTRING"].ToString();

    }
}