using SRMS.Data;
using SRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRMS.Controllers
{
    public class CourseSettingController : Controller
    {
        SRMSDbContext db;
        public CourseSettingController()
        {
            db = new SRMSDbContext();

        }
        // GET: CourseSetting
        public ActionResult Index()
        {
            return View();
        }

        class CombineCourseAssin
        {
            public List<Student> Student { get; set; }
            public List<Staff_Teacher> Teacher { get; set; }
            public List<Cariculam> Cariculam { get; set; }
        }
         
        public JsonResult ListOfCourse()
        {
            List<CourseSettingM> allCourse = new List<CourseSettingM>();
            var std = db.Students.ToList();
            var tech = db.Staff_Teacher.Where(x => x.Type == "Faculty").ToList();
            var course = db.Cariculams.ToList();

            CombineCourseAssin obj = new CombineCourseAssin();
            obj.Student= std.ToList();


            foreach (var item in tech)
            {
                item.ImagePath = item.ImagePath.Replace("~", "");
            }

            obj.Teacher = tech;
            obj.Cariculam = course;

            return Json(obj,JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult AddEdit()
        {
            ListOfCourse();
            return View();
        }

   
       
        [HttpPost]
        public ActionResult AddEdit(CourseSettingM m)
        {
            if (m!=null)
            {
              //assin on object
                AssinCourse assinCourse = new AssinCourse();
                assinCourse.Batch = m.Batch;
                assinCourse.CourseCode = m.CourseCode;
                assinCourse.CourseTitle = m.CourseTitle;
                assinCourse.Degicnetion = m.Degicnetion;
                assinCourse.Dept = m.Dept;
                assinCourse.Semester = m.Semester;
                assinCourse.Spciallity = m.Spciallity;
                assinCourse.TeacherName = m.TeacherName;
              //save to database
                db.AssinCourses.Add(assinCourse);
                db.SaveChanges();

                return RedirectToAction("AssinList", "CourseSetting");

            }
            else
            {
                    
            }

            return View();
        }


        public ActionResult AssinList()
        {
           
                var assinList = db.AssinCourses.ToList();
                return View(assinList);

        }

        public ActionResult Delete(int id)
        {
            var deleteAssinCorse = db.AssinCourses.SingleOrDefault(x=>x.Id==id);
            db.AssinCourses.Remove(deleteAssinCorse);
            db.SaveChanges();

            return RedirectToAction("AssinList", "CourseSetting");
        }

        public ActionResult Students(int id)
        {
            var stdList = db.AssinCourses.SingleOrDefault(x=>x.Id==id);
            var allStd = db.Students.Where(x => x.Batch == stdList.Batch && x.Semester == stdList.Semester && x.Dept==stdList.Dept && x.Status=="True");


            return View(allStd.ToList());
        }



    }
}