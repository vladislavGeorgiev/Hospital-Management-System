
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class PatientsDbContext : IdentityDbContext<Doctor>
    {
        public PatientsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Patient> Patients { get; set; }

        public static PatientsDbContext Create()
        {
            return new PatientsDbContext();
        }
    }
}