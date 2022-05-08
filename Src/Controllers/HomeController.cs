using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectA.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ProjectA.Models;
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
        public IActionResult Index()
        {
            ClinicDal clinicobj = new ClinicDal();
            List<Appointments> appointlist = new List<Appointments>();
            appointlist = clinicobj.Showappointments();
            return View(appointlist);
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
                return RedirectToAction("Index");
            else
                return RedirectToAction("Delete");
        }
        [Route("Home/Searchappointment")]
        public IActionResult Searchappointment(int Id)
        {
            ClinicDal sobj = new ClinicDal();
            List<Appointments> appointlist = new List<Appointments>();
            appointlist = sobj.GetappointmentByID(Id);
            return View(appointlist);
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
