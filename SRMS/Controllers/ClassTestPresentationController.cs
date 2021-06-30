using SRMS.Data;
using SRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRMS.Controllers
{
    public class ClassTestPresentationController : Controller
    {
        SRMSDbContext db;
        // GET: ClassTestPresentation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClassTest()
        {
            // var id = Session["Id"];
            string name = Convert.ToString(Session["FullName"]);
            string specallity = Convert.ToString(Session["Specallity"]);
            db = new SRMSDbContext();
            var curseassin = db.AssinCourses.Where(x => x.TeacherName == name && x.Spciallity == specallity);

            return View(curseassin.ToList());
        }



        public ActionResult Assignment(int id)
        {
           // Session["id"] = id;
            db = new SRMSDbContext();
            var stdList = db.AssinCourses.SingleOrDefault(x => x.Id == id);
            StdCourse = stdList.CourseTitle;
            //Assin for delete mid-mark
            StdBatch = stdList.Batch;
            StdSemester = stdList.Semester;
            StdDept = stdList.Dept;

            var allStd = db.Students.Where(x => x.Batch == stdList.Batch && x.Semester == stdList.Semester && x.Dept == stdList.Dept && x.Status == "True");
            string name = Convert.ToString(Session["FullName"]);
            string specallity = Convert.ToString(Session["Specallity"]);
            var curseassin = db.AssinCourses.Where(x => x.TeacherName == name && x.Spciallity == specallity);
            List<AttendanceM> attendanceM = new List<AttendanceM>();
            foreach (var item in allStd)
            {
                AttendanceM m = new AttendanceM();
                m.Id = item.ID;
                m.StdName = item.Name;
                m.Roll = item.Roll;
                m.AccountID = item.AccountID;
                m.Batch = item.Batch;
                m.Semester = item.Semester;
                m.TeacherName = name;
                m.Date = DateTime.Now;
                m.boolAttn = false;
                m.CourseName = stdList.CourseTitle;
                m.CourseCode = stdList.CourseCode;
                m.StdAtten = 0;
                m.TypeOfAtten = "Regular";
                attendanceM.Add(m);
            }
            ViewBag.AllAttendance = attendanceM.ToList();
            return View(attendanceM.ToList());
        }




        public static int StdUid;
        public static string StdCourse;
        //for delete mid-mark
        public static string StdBatch;
        public static string StdSemester;
        public static string StdDept;
        public ActionResult AllStudent(int id)
        {
            StdUid = id;
            db = new SRMSDbContext();
            var stdList = db.AssinCourses.SingleOrDefault(x => x.Id == id);
            StdCourse = stdList.CourseTitle;
            //Assin for delete mid-mark
            StdBatch = stdList.Batch;
            StdSemester = stdList.Semester;
            StdDept = stdList.Dept;

         var allStd = db.Students.Where(x => x.Batch == stdList.Batch && x.Semester == stdList.Semester && x.Dept == stdList.Dept && x.Status == "True");
            string name = Convert.ToString(Session["FullName"]);
            string specallity = Convert.ToString(Session["Specallity"]);
            var curseassin = db.AssinCourses.Where(x => x.TeacherName == name && x.Spciallity == specallity);
            List<AttendanceM> attendanceM = new List<AttendanceM>();
            foreach (var item in allStd)
            {
                AttendanceM m = new AttendanceM();
                m.Id = item.ID;
                m.StdName = item.Name;
                m.Roll = item.Roll;
                m.AccountID = item.AccountID;
                m.Batch = item.Batch;
                m.Semester = item.Semester;
                m.TeacherName = name;
                m.Date = DateTime.Now;
                m.boolAttn = false;
                m.CourseName = stdList.CourseTitle;
                m.CourseCode = stdList.CourseCode;
                m.StdAtten = 0;
                m.TypeOfAtten = "Regular";
                attendanceM.Add(m);
            }
            ViewBag.AllAttendance = attendanceM.ToList();
            return View(attendanceM.ToList());
        }


        public ActionResult ClasstestStd(string Pid,string StdId, string CourseName, string CourseCode, string classtest1, string classtest2, string classtest3, string classtest4, string classtest5)
        {
            db = new SRMSDbContext();
            int id = Convert.ToInt32(StdId);
            var std = db.Students.SingleOrDefault(x => x.ID == id);
      
            Class_Test ct = new Class_Test();
            ct.StdDBId = Convert.ToInt32(StdId);
            ct.ACid = std.AccountID;
            ct.Batch = std.Batch;
            ct.CourseCode = CourseCode;
            ct.CourseName = CourseName;
            ct.Dept = std.Dept;
            ct.Roll = std.Roll;
            ct.Semester = std.Semester;
            ct.StdMobile = std.StdMobile;
            ct.StdName = std.Name;
            if (string.IsNullOrEmpty(classtest1))
            {
                ct.CTone = 0;
            }
            else
            {
                ct.CTone = Convert.ToDouble(classtest1);
            }
            if (string.IsNullOrEmpty(classtest2))
            {
                ct.CTtwo = 0;
            }
            else
            {
                ct.CTtwo = Convert.ToDouble(classtest2);
            }
            if (string.IsNullOrEmpty(classtest3))
            {
                ct.CTthree = 0;
            }
            else
            {
                ct.CTthree = Convert.ToDouble(classtest3);
            }
            if (string.IsNullOrEmpty(classtest4))
            {
                ct.CTFour = 0;
            }
            else
            {
                ct.CTFour = Convert.ToDouble(classtest4);
            }
            if (string.IsNullOrEmpty(classtest5))
            {
                ct.CTfive = 0;
            }
            else
            {
                ct.CTfive = Convert.ToDouble(classtest5);
            }


            var arether = db.Class_Test.Where(x => x.CourseName == ct.CourseName && x.CourseCode == ct.CourseCode && x.ACid == ct.ACid && x.Batch == ct.Batch && x.Semester == ct.Semester && x.Dept == ct.Dept && x.Roll == ct.Roll && x.StdName == ct.StdName).ToList();
            if (arether.Count == 0)
            {
                db.Class_Test.Add(ct);
                db.SaveChanges();
            }

            int ai =Convert.ToInt32(Pid);


          
           return RedirectToAction("AllStudent", "ClassTestPresentation",new {id= StdUid});


            //  return Json(std, JsonRequestBehavior.AllowGet);
         // return Json(new { success = true, responseText = "Class Test Mark Successfuly Adding!" }, JsonRequestBehavior.AllowGet);

        }


        public JsonResult Delete(int id)
        {
            db = new SRMSDbContext();
            string name = Convert.ToString(Session["FullName"]);
            var assinCourse = db.AssinCourses.Where(x => x.TeacherName == name);
            var orgCourse = assinCourse.SingleOrDefault(x=>x.CourseTitle== StdCourse);

            var deleteClassTestMark = db.Class_Test.Where(x => x.StdDBId == id);

            var orgDelete = deleteClassTestMark.SingleOrDefault(x=>x.CourseName== orgCourse.CourseTitle);




            if (deleteClassTestMark != null && orgDelete!=null)
            {
                db.Class_Test.Remove(orgDelete);
                db.SaveChanges();
                return Json(new { success = true, responseText = "Class Test Mark Successfuly Delete!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Class Test Mark Not Found!" }, JsonRequestBehavior.AllowGet);
            }


        }

      


        //start Presentation section
        public ActionResult Presentation(int id)
        {
            StdUid = id;

            db = new SRMSDbContext();

            var stdList = db.AssinCourses.SingleOrDefault(x => x.Id == id);

            StdCourse = stdList.CourseTitle;
            //Assin for delete mid-mark
            StdBatch = stdList.Batch;
            StdSemester = stdList.Semester;
            StdDept = stdList.Dept;

            var allStd = db.Students.Where(x => x.Batch == stdList.Batch && x.Semester == stdList.Semester && x.Dept == stdList.Dept && x.Status == "True");
            string name = Convert.ToString(Session["FullName"]);
            string specallity = Convert.ToString(Session["Specallity"]);
            var curseassin = db.AssinCourses.Where(x => x.TeacherName == name && x.Spciallity == specallity);
            List<AttendanceM> attendanceM = new List<AttendanceM>();
            foreach (var item in allStd)
            {
                AttendanceM m = new AttendanceM();
                m.Id = item.ID;
                m.StdName = item.Name;
                m.Roll = item.Roll;
                m.AccountID = item.AccountID;
                m.Batch = item.Batch;
                m.Semester = item.Semester;
                m.TeacherName = name;
                m.Date = DateTime.Now;
                m.boolAttn = false;
                m.CourseName = stdList.CourseTitle;
                m.CourseCode = stdList.CourseCode;
                m.StdAtten = 0;
                m.TypeOfAtten = "Regular";
                attendanceM.Add(m);
            }
            ViewBag.AllAttendance = attendanceM.ToList();
            return View(attendanceM.ToList());
        }

         [HttpPost]
        public ActionResult PresentationMark(string Pid, string PresentationMark,string StdId,string CourseName,string CourseCode)
        {


            db = new SRMSDbContext();
            int id = Convert.ToInt32(StdId);
            var std = db.Students.SingleOrDefault(x => x.ID == id);

            Presentation pt = new Presentation();
            pt.StdDBId = Convert.ToInt32(StdId);
            pt.AcId = std.AccountID;
            pt.Batch = std.Batch;
            pt.CourseCode = CourseCode;
            pt.CourseName = CourseName;
            pt.Dept = std.Dept;
            pt.Roll = std.Roll;
            pt.Semester = std.Semester;
            pt.StdMobile = std.StdMobile;
            pt.StdName = std.Name;
            if (string.IsNullOrEmpty(PresentationMark))
            {
                pt.PresentationMark = 0;
            }
            else
            {
                pt.PresentationMark = Convert.ToDouble(PresentationMark);
            }

            int myPid =Convert.ToInt32(Pid);
            var arether = db.Presentations.Where(x => x.CourseName == pt.CourseName && x.CourseCode == pt.CourseCode && x.AcId == pt.AcId && x.Batch == pt.Batch && x.Semester == pt.Semester && x.Dept == pt.Dept && x.Roll == pt.Roll && x.StdName == pt.StdName).ToList();
            if (arether.Count == 0)
            {
                db.Presentations.Add(pt);
                db.SaveChanges();

                
               return RedirectToAction("Presentation", "ClassTestPresentation", new { id = StdUid });
               // Json(new { success = true, responseText = "Presentetion Mark Saved" }, JsonRequestBehavior.AllowGet);

            }
             return RedirectToAction("Presentation", "ClassTestPresentation", new { id = StdUid });
        }



        public JsonResult DeletePt(int id)
        {
            db = new SRMSDbContext();
            string name = Convert.ToString(Session["FullName"]);
            var assinCourse = db.AssinCourses.Where(x => x.TeacherName == name);

            var orgCourse = assinCourse.SingleOrDefault(x => x.CourseTitle == StdCourse);

            var deletePtMark = db.Presentations.Where(x => x.StdDBId == id);

            var orgDelete = deletePtMark.SingleOrDefault(x => x.CourseName == orgCourse.CourseTitle);




            if (deletePtMark != null && orgDelete != null)
            {
                db.Presentations.Remove(orgDelete);
                db.SaveChanges();
                return Json(new { success = true, responseText = "Presentetion Mark Successfuly Delete!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Presentetion Mark Not Found!" }, JsonRequestBehavior.AllowGet);
            }


        }

      
       public ActionResult AssignmentStd(string Pid, string StdId, string CourseName, string CourseCode,string assin1,string assin2,string assin3,string assin4,string assin5,string assin6,string assin7,string assin8,string assin9,string assin10)
        {


            db = new SRMSDbContext();
            int id = Convert.ToInt32(StdId);
            var std = db.Students.SingleOrDefault(x => x.ID == id);

            Assignment ass = new Assignment();
            ass.StdDBId = Convert.ToString(StdId);
            ass.ACid = std.AccountID;
            ass.Batch = std.Batch;
            ass.CourseCode =CourseCode;
            ass.CourseName = CourseName;
            ass.Dept = std.Dept;
            ass.Roll = std.Roll;
            ass.Semester = std.Semester;
            ass.StdMobile = std.StdMobile;
            ass.StdName = std.Name;
            if (string.IsNullOrEmpty(assin1))
            {
                ass.ASone = 0;
            }
            else
            {
                ass.ASone = Convert.ToDouble(assin1);
            }
            if (string.IsNullOrEmpty(assin2))
            {
                ass.AStwo = 0;
            }
            else
            {
                ass.AStwo = Convert.ToDouble(assin2);
            }
            if (string.IsNullOrEmpty(assin3))
            {
                ass.ASthree = 0;
            }
            else
            {
                ass.ASthree = Convert.ToDouble(assin3);
            }
            if (string.IsNullOrEmpty(assin4))
            {
                ass.ASfour = 0;
            }
            else
            {
                ass.ASfour = Convert.ToDouble(assin4);
            }
            if (string.IsNullOrEmpty(assin5))
            {
                ass.ASfive = 0;
            }
            else
            {
                ass.ASfive = Convert.ToDouble(assin5);
            }

            if (string.IsNullOrEmpty(assin6))
            {
                ass.ASsix = 0;
            }
            else
            {
                ass.ASsix = Convert.ToDouble(assin6);
            }
            if (string.IsNullOrEmpty(assin7))
            {
                ass.ASseven = 0;
            }
            else
            {
                ass.ASseven = Convert.ToDouble(assin7);
            }
            if (string.IsNullOrEmpty(assin8))
            {
                ass.ASeight = 0;
            }
            else
            {
                ass.ASeight = Convert.ToDouble(assin8);
            }
            if (string.IsNullOrEmpty(assin9))
            {
                ass.ASnine = 0;
            }
            else
            {
                ass.ASnine = Convert.ToDouble(assin9);
            }
            if (string.IsNullOrEmpty(assin10))
            {
                ass.ASten = 0;
            }
            else
            {
                ass.ASten = Convert.ToDouble(assin10);
            }



            var arether = db.Assignments.Where(x => x.CourseName == ass.CourseName && x.CourseCode == ass.CourseCode && x.ACid == ass.ACid && x.Batch == ass.Batch && x.Semester == ass.Semester && x.Dept == ass.Dept && x.Roll == ass.Roll && x.StdName == ass.StdName).ToList();
            if (arether.Count == 0)
            {
                db.Assignments.Add(ass);
                db.SaveChanges();
            }

            int ai = Convert.ToInt32(Pid);
            return RedirectToAction("Assignment", "ClassTestPresentation", new { id = StdUid });
    

        }

        



        public JsonResult ASDelete(int id)
        {
            db = new SRMSDbContext();
            string name = Convert.ToString(Session["FullName"]);
            var assinCourse = db.AssinCourses.Where(x => x.TeacherName == name);
            var orgCourse = assinCourse.SingleOrDefault(x => x.CourseTitle == StdCourse);

           // int intStdDBId =Convert.ToInt32(StdDBId);
            var deleteClassTestMark = db.Assignments.Where(x => x.StdDBId == id.ToString());

            var orgDelete = deleteClassTestMark.SingleOrDefault(x => x.CourseName == orgCourse.CourseTitle);

            if (deleteClassTestMark != null && orgDelete != null)
            {
                db.Assignments.Remove(orgDelete);
                db.SaveChanges();
                return Json(new { success = true, responseText = "Class Test Mark Successfuly Delete!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Class Test Mark Not Found!" }, JsonRequestBehavior.AllowGet);
            }


        }




    }
}