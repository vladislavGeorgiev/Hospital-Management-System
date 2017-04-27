namespace HospitalManagementSystem.Migrations
{
    using Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    

    public sealed class Configuration : DbMigrationsConfiguration<PatientsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
         
        }

        protected override void Seed(PatientsDbContext context)
        {
           
        }
    }
}
