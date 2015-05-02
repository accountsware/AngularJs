
using System;
using Angular.Common;
using Angular.Core.Modals;
using Angular.Core.Modals.Base;
using Angular.Core.Modals.Identity;
using Angular.Data;
using Angular.Data.Context;
using Angular.Data.Repository;
using Angular.Data.Repository.@base;
using Angular.Services;
using Angular.Services.Base;
using BrockAllen.MembershipReboot;


namespace Angular.Console
{
    class Program
    {


        static void Main(string[] args)
        {
            var db = new AngularContext();
            var uow = new UnitOfWork(db);
            var igenuser = new Repository<UserAccount>(db, uow);

            var repo = new UserAccountRepository(igenuser,uow);
            var mo = new MembershipRebootConfiguration(new SecuritySettings());
           // var serv = new Service<UserAccount>(repo);
            var email = "user" + Guid.NewGuid().ToString("n") + "@yourme.com";

            var usrv = new UserAccountService(mo,repo);

            usrv.CreateAccount("default", email, "mypass", email);

       //    uow.SaveChanges();

            var user = usrv.GetByEmail("default",email);
            System.Console.WriteLine(user.ID.ToString("P"));

            System.Console.ReadLine();




        }
    }
}
