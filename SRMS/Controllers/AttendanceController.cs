using SRMS.Data;
using SRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRMS.Controllers
{
    public class AttendanceController : Controller
    {
        SRMSDbContext db;
        public ActionResult Atten()
        {
            var id = Session["Id"];
            string name = Convert.ToString(Session["FullName"]);
            string specallity = Convert.ToString(Session["Specallity"]);
            db = new SRMSDbContext();
            var curseassin = db.AssinCourses.Where(x => x.TeacherName == name && x.Spciallity == specallity);
            return View(curseassin.ToList());
        }
         public static int atId=0;
        public ActionResult SetAttendance(int id)
        {
          

            atId = id;
            Session["AtdId"] = id;
            db = new SRMSDbContext();
            var stdList = db.AssinCourses.SingleOrDefault(x => x.Id == id);
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
        public ActionResult SetAttendance()
        {
            return View();
        }
        public ActionResult AttenProcess(int id)
        {
            Session["AtdId"] = id;
            db = new SRMSDbContext();
            var stdList = db.AssinCourses.SingleOrDefault(x => x.Id == atId);
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
                m.StdAtten = 1;
                m.TypeOfAtten = "Regular";
                attendanceM.Add(m);
            }
            var result = attendanceM.ToList().SingleOrDefault(x=>x.Id== id);
            var dateList = db.Attendances.ToList();
            var chackAt = dateList.Where(x =>x.Batch == result.Batch && x.Semester == result.Semester && x.CourseCode == result.CourseCode && x.CourseName == result.CourseName && x.TeacherName == result.TeacherName&&x.StdId==result.Roll&&x.StdAcId==result.AccountID&&x.DateOfAttendance.Date==result.Date.Value.Date).ToList();
            if (chackAt.Count()!=0)
            {
                return View();
            }

            Attendance at = new Attendance();
            at.StdName = result.StdName;
            at.StdId = result.Roll;
            at.StdAcId = result.AccountID;
            at.Batch = result.Batch;
            at.Semester = result.Semester;
            at.CourseName = result.CourseName;
            at.CourseCode = result.CourseCode;
            at.TeacherName = result.TeacherName;
            at.DateOfAttendance =Convert.ToDateTime(result.Date);
            at.TypeOfAttendance = result.TypeOfAtten;
            at.StdAttendance = result.StdAtten;
            db.Attendances.Add(at);

            db.SaveChanges();
            
            //return RedirectToAction("SetAttendance", "Attendance", new { id = atId });
            return View();
        }

        [HttpPost]
        public ActionResult AttenProcess()
        {
            return RedirectToAction("SetAttendance", "Attendance", new {id= Session["AtdId"]});
        }
  


       public ActionResult EditAttendance(int id)
        {
           
            db = new SRMSDbContext();
            var assinCourses = db.AssinCourses.SingleOrDefault(x => x.Id == id);
            var attendance = db.Attendances.Where(x => x.TeacherName == assinCourses.TeacherName && x.Batch == assinCourses.Batch && x.Semester == assinCourses.Semester && x.CourseName == assinCourses.CourseTitle && x.CourseCode == assinCourses.CourseCode);




            return View(attendance.ToList());
        }


        public ActionResult Delete(int id)
        {
            db = new SRMSDbContext();
            var deleteAttenStd = db.Attendances.SingleOrDefault(x=>x.Id==id);

            db.Attendances.Remove(deleteAttenStd);
            db.SaveChanges();

            return RedirectToAction("Atten", "Attendance");
        }


    }
 
}