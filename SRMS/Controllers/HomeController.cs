using SRMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRMS.Controllers
{
    public class HomeController : Controller
    {
        SRMSDbContext db;
        public ActionResult Index()
        {
            if (Session["FullName"] != null && Session["UserName"] != null && Session["Img"] != null && Session["Specallity"] != null)
            {
                db = new SRMSDbContext();
                Session["AllStudent"] = db.Students.Count().ToString();
                Session["AllFaculty"]  = db.Staff_Teacher.Where(x=>x.Type== "Faculty").Count().ToString();
                Session["AllStaff"] = db.Staff_Teacher.Where(x => x.Type == "Staff").Count().ToString();

                return View();
            }
            else
                return RedirectToAction("Login", "Admin");

        }
        public ActionResult AllStd()
        {
            return RedirectToAction("StdList", "Student");
        }

        public ActionResult AllFaculty()
        {
            return RedirectToAction("StaffList", "Staff");
        }



        

    }
}


                     
