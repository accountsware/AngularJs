using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrockAllen.MembershipReboot;

namespace Angular.AuthServer.UserManagementSettings
{
    public class UserManagementConfigurations : MembershipRebootConfiguration
    {
        public static readonly UserManagementConfigurations Config;

        static UserManagementConfigurations()
        {
            Config = new UserManagementConfigurations();
            Config.PasswordHashingIterationCount = 50000;
            Config.RequireAccountVerification = false;
            Config.EmailIsUsername = true;
            Config.DefaultTenant = "default";
            Config.EmailIsUsername = true;
        }

    }
}
