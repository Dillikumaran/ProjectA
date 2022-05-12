using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectA.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ProjectA.DALclinic;
using System.ComponentModel.DataAnnotations;


namespace ProjectA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult UserLogin()
        {
            return View();
        }
        [Route("Home/Menu")]
        public IActionResult Menu()
        {
            return View();
        }
        [Route("Home/Newdoctor")]
        public IActionResult NewDoctor()
        {
            return View();
        }
        public IActionResult Adddoc(Doctor doc)
        {
            if (ModelState.IsValid)
            {
                ClinicDal aobj = new ClinicDal();
                int result = aobj.Insertdoc(doc);
                if (result == 1)
                {
                    TempData["msg"] = "<script>alert('Visit Doctor Added Successfully!!!');</script>";
                    return View("Menu");
                }
                else
                    return View("NewDoctor");
            }
            return View("NewDoctor");
        }
        [Route("Home/Newpatient")]
        public IActionResult NewPatient()
        {
            return View();
        }
        public IActionResult Addpat(Patient pat)
        {
            if (ModelState.IsValid)
            {
                ClinicDal aobj = new ClinicDal();
                int result = aobj.Insertpat(pat);
                if (result == 1)
                {
                    TempData["msg"] = "<script>alert('New Patient Added Successfully!!!');</script>";
                    return View("Menu");
                }
                else
                    return View("NewPatient");
            }
            return View("NewPatient");
        }
        [Route("Home/FixAppointment")]
        public IActionResult FixAppointment()
        {
            return View();
        }
        public IActionResult Addapp(Appointments appoint)
        {
            if (ModelState.IsValid)
            {
                ClinicDal aobj = new ClinicDal();
                int result = aobj.Insertappoint(appoint);
                if (result == 1)
                {
                    TempData["msg"] = "<script>alert('Appointment scheduled Successfully!!!');</script>";
                    return View("Menu");
                }
                else
                    return View("FixAppointment");
            }
            return View("FixAppointment");
        }
        public IActionResult CancelAppointment()
        {
            return View();
        }
        public IActionResult Editing(Appointments appoint)
        {
            ClinicDal eobj = new ClinicDal();
            int result = eobj.Updateappoint(appoint);
            if (result == 1)

                return RedirectToAction("Index");
            else
                return RedirectToAction("Edit");
        }
        public IActionResult Deleting(int id)
        {
            ClinicDal dobj = new ClinicDal();
            int result = dobj.Cancelappointment(id);
            if (result == 1)
            {
                TempData["msg"] = "<script>alert('Appointment Deleted Successfully!!!');</script>";
                return View("Menu");
            }
            else
                return View("Menu");
        }
        public IActionResult Cancelapp(Appointments capp)
        {
            ClinicDal sobj = new ClinicDal();
            List<Appointments> appointlist = new List<Appointments>();
            appointlist = sobj.GetappointmentByID(capp);
            return View(appointlist);
        }
        public IActionResult Validate(UserLogin use) 
        {
            if (ModelState.IsValid)
            {
                ClinicDal chkobj = new ClinicDal();
                string result = chkobj.CheckUse(use);
                if (result == use.PassWord)
                {
                    TempData["msg"] = "<script>alert('logged in successfully!!!');</script>";
                    return View("Menu");
                }
                else
                {
                    TempData["msg"] = "<script>alert('you've entered an incorrect username or password');</script>";
                    return View("UserLogin");
                }
                   
            }
            else
                return View("UserLogin");
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
