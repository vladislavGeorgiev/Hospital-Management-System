

namespace HospitalManagementSystem.Data
{

    using System.Data.Entity;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Doctor : IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        public Doctor()
        {
            this.Patients = new HashSet<Patient>();
        }

        public virtual ICollection<Patient> Patients { get; set;}

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Doctor> manager)
        {
            
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
           
            return userIdentity;
        }
       

    }

  
}