using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new PatientsDbContext();

            var patients = db.Patients
                .OrderByDescending(c => c.Id)
                .Take(3)
                .Select(c => new PatientsListingModel
                {
                    Id = c.Id,
                   ImagePath=c.ImagePath
                })
                .ToList();

            return View(patients);
        }

        
    }
}