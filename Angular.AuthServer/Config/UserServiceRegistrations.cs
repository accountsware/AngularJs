
using Angular.AuthInfrastructure.Configuration;
using Angular.AuthInfrastructure.Services;
using Angular.AuthServer.UserManagementExtension;
using Angular.Core.IDataService;
using Angular.Core.IRepository.Base;
using Angular.Core.Modals.Identity;
using Angular.Data.Context;
using Angular.Data.Repository.@base;
using BrockAllen.MembershipReboot;

namespace Angular.AuthServer.Config
{
    public static class UserServiceRegistrations
    {
        public static void ConfigureCustomUserService(IdentityServerServiceFactory factory, string connString)
        {
            


        }

    }
}