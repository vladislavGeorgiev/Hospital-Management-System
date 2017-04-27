using Microsoft.Owin;
using HospitalManagementSystem.Migrations;
using HospitalManagementSystem.Models;
using Owin;
using System.Data.Entity;
using HospitalManagementSystem.Data;

[assembly: OwinStartupAttribute(typeof(HospitalManagementSystem.Startup))]
namespace HospitalManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PatientsDbContext, Configuration>());


            ConfigureAuth(app);
        }
    }
}
