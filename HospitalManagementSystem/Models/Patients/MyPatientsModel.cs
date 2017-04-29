using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Patients
{
    public class MyPatientsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Condition { get; set; }
        public int Room { get; set; }

    }
}