using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models.Patients
{
    using Data;
    using System.ComponentModel.DataAnnotations;
    public class CreateNewPatient
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public GenderOption Gender { get; set; }

        
       [ScaffoldColumn(false)]
        public string ImagePath { get; set; }

        [Required]
        public string Condition { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public StatusOption Status { get; set; }

        [Required]
        public int Room { get; set; }

    }
}