using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HospitalManagementSystem.Controllers
{
    using Data;
    using Microsoft.AspNet.Identity;
    using Models.Patients;
    using System.IO;
    using System.Web.Mvc;

    public class PatientsController :Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateNewPatient PatientModel,HttpPostedFileBase image)
        {
            if(PatientModel!=null && this.ModelState.IsValid)
            {
                var doctorId = this.User.Identity.GetUserId();
                var patient = new Patient
                {
                    Name = PatientModel.Name,
                    Age = PatientModel.Age,
                    Gender = PatientModel.Gender,
                    Condition = PatientModel.Condition,
                    Status = PatientModel.Status,
                    Room = PatientModel.Room,
                    DoctorId = doctorId
                };

                if (image != null)
                {
                    var allowedContentTypes = new[] { "image/jpeg", "image/jpg", "image/png" };

                    if (allowedContentTypes.Contains(image.ContentType))
                    {
                        var imagesPath = "/Content/Images/";

                        var fileName = image.FileName;
                        var uploadPath =imagesPath+fileName;
                        var physicalPath =Server.MapPath(uploadPath);

                        image.SaveAs(physicalPath);
                        patient.ImagePath = uploadPath;
                    }
                }
                var db = new PatientsDbContext();
                db.Patients.Add(patient);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = patient.Id });

            }
            return View(PatientModel);
        }
        
    }
}