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
            //List<Doctor> listdoc = new List<Doctor>();
            //List<Doctor> listtime = new List<Doctor>();
            //ClinicDal dobj = new ClinicDal();
            //listdoc = dobj.Showdoc(app);
            //listtime = dobj.Showtime(id);
            //return View(listdoc,listtime,app);
            return View();
        }
        public IActionResult FixDoctor()
        {
            ClinicDal sobj = new ClinicDal();
            List<Doctor> appointlist = new List<Doctor>();
            appointlist = sobj.Showdoctor();
            return View(appointlist);
        }
        public IActionResult Addapp(Appointments appoint)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                    ClinicDal aobj = new ClinicDal();
                int result = aobj.Insertappoint(appoint);
                if (result == 1)
                {
                    TempData["msg"] = "<script>alert('Appointment scheduled Successfully!!!');</script>";
                    return View("Menu");
                }
                else
                    return View("FixAppointment");
                //}
                //catch
                //{
                //    TempData["msg"] = "<script>alert('Patient does not exist');</script>";
                //    return RedirectToAction("FixAppointment");
                //}
            }
            return View("FixAppointment");
        }
        public IActionResult CancelAppointment()
        {
            return View();
        }
        public IActionResult Updatedoctor(Appointments appoint)
        {
            ClinicDal eobj = new ClinicDal();
            int result = eobj.Updateappointment(appoint);
            if (result == 1)
            {
                TempData["msg"] = "<script>alert('Appointment scheduled Successfully!!!');</script>";
                return View("Menu");
            }
            else
                return RedirectToAction("Menu");
        }
        public IActionResult Deleting(int id)
        {
            ClinicDal dobj = new ClinicDal();
            int result = dobj.Cancelappointment(id);
            if (result == 1)
            {
                TempData["msg"] = "<script>alert('Appointment Deleted Successfully!!!');</script>";
                return RedirectToAction("Menu");
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
        //public ActionResult Showdoc(string spec)
        //{
        //    List<Doctor> listdoc = new List<Doctor>();
        //    ClinicDal dobj = new ClinicDal();
        //    listdoc = dobj.Showdoc(spec);
        //    return PartialView(listdoc);
        //}
        //public List<Doctor> Showavailtime()
        //{
        //    List<Doctor> listtime = new List<Doctor>();
        //    ClinicDal dobj = new ClinicDal();
        //    listtime = dobj.Showtime();
        //    return listtime;
        //}
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
