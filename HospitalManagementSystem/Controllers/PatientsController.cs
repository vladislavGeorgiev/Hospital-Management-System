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
    using System.Net;
    using System.Web.Mvc;

    public class PatientsController :Controller
    {
        public ActionResult AllPatients()
        {
            var db = new PatientsDbContext();
            var patients=db.Patients
                .Select(c=>new PatientsListingModel
                {
                    Id=c.Id,
                    ImagePath=c.ImagePath

            })
            .ToList();
            return View(patients);
        }


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
                    DoctorId = doctorId,
                    Id=PatientModel.Id
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
        
        public ActionResult Details(int id)
        {
            

            var db = new PatientsDbContext();
            var patient = db.Patients.Where(c => c.Id == id)
                .Select(c => new PatientsDetailsModel
                {
                    Name = c.Name,
                    Age = c.Age,
                    Gender = c.Gender,
                    Condition = c.Condition,
                    Status = c.Status,
                    Room = c.Room,
                    ImagePath=c.ImagePath,
                    Doctor=c.Doctor.FullName,
                    Id=c.Id

            })
            .FirstOrDefault();

            if(patient==null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(int id)
        {
           
            using (var db = new PatientsDbContext())
            {
                var patient = db.Patients.Find(id);
               
                    

                if(patient==null)
                {
                    return HttpNotFound();
                }
                return View(patient);
            }
        }

        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
          
            using (var db = new PatientsDbContext())
            {
                var patient = db.Patients.Find(id);
                   
                if(patient==null)
                {
                    return HttpNotFound();
                }
                db.Patients.Remove(patient);
                db.SaveChanges();
                return RedirectToAction("AllPatients");
            }
            
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var db = new PatientsDbContext())
            {
                var patient = db.Patients.Find(id);

                var patientEditModel = new PatientsEditModel
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Gender = patient.Gender,
                    Condition = patient.Condition,
                    Status = patient.Status,
                    Room = patient.Room,
                    DoctorId = patient.DoctorId
                };
                return View(patientEditModel);
            }
        }
        public ActionResult Edit(PatientsEditModel model)
        {
            if(ModelState.IsValid)
            {
                using (var db = new PatientsDbContext())
                {
                    var patient =db.Patients.Find(model.Id);

                    patient.Id = model.Id;
                    patient.ImagePath = model.ImagePath;
                    patient.Room = model.Room;
                    patient.Status = model.Status;
                    patient.Gender = model.Gender;
                    patient.Condition = model.Condition;


                }
                return RedirectToAction("Details", new { id = model.Id });
            }
            return View(model);
        }

    }
}