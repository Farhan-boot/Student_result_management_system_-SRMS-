using SRMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRMS.Controllers
{

    public class Sem
    {
        public string SemesterName { get; set; }
    }

    public class StudentResultController : Controller
    {
        SRMSDbContext db;
        // GET: StudentResult
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StatusFalse()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            db = new SRMSDbContext();
            Student detStd = db.Students.Where(x => x.Status == "True").SingleOrDefault(x => x.ID == id);
            return View(detStd);
        }



        [HttpPost]
        public JsonResult ListOfMidResultStd(Sem sem)
        {

            db = new SRMSDbContext();
            var result = db.Publish_Mid_Marks.Where(x => x.Semester == sem.SemesterName && x.Roll == AdminController.StdRoll && x.StdName == AdminController.StdName).ToList();
            StudentMidMark objMid = new StudentMidMark();
            objMid.Name = AdminController.StdName;
            objMid.Roll = AdminController.StdRoll;
            objMid.PublishMidMarks = result;

            return Json(objMid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MidResultStdView()
        {
            // ListOfMidResultStd("Semester 0");
            return View();
        }


        // ListOfFinalResultStd

        [HttpPost]
        public JsonResult ListOfFinalResultStd(Sem sem)
        {

            db = new SRMSDbContext();
            var result = db.Publish_Final_Marks.Where(x => x.Semester == sem.SemesterName && x.Roll == AdminController.StdRoll && x.StdName == AdminController.StdName).ToList();
            StudentFinalMark objMid = new StudentFinalMark();
            objMid.Name = AdminController.StdName;
            objMid.Roll = AdminController.StdRoll;
            objMid.PublishFinalMarks = result;

            return Json(objMid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FinalResultStdView()
        {
            
            return View();
        }



    }

    public class StudentMidMark
    {
        public string Name { get; set; }
        public string Roll { get; set; }
        public List<Publish_Mid_Marks> PublishMidMarks { get; set; }
    }

    public class StudentFinalMark
    {
        public string Name { get; set; }
        public string Roll { get; set; }
        public List<Publish_Final_Marks> PublishFinalMarks { get; set; }
    }

}