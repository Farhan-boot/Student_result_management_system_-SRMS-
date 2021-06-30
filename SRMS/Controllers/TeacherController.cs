using SRMS.Data;
using SRMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRMS.Controllers
{
    public class TeacherController : Controller
    {
        SRMSDbContext db;
        public int CountCourse = 0;
        // GET: Teacher
        // [Route("Uoda/Teacher{id}")]
        public ActionResult Teacher()
        {
            string name = Convert.ToString(Session["FullName"]);
            string specallity = Convert.ToString(Session["Specallity"]);
            db = new SRMSDbContext();
            var curseassin = db.AssinCourses.Where(x => x.TeacherName == name && x.Spciallity == specallity);
            this.CountCourse = curseassin.Count();

            Session["CountCourse"] = this.CountCourse.ToString();

            return View();
        }
        public ActionResult Details(int id)
        {
            db = new SRMSDbContext();
            Staff_Teacher detStf = db.Staff_Teacher.Where(x => x.Type == "Faculty").SingleOrDefault(x => x.Id == id);
            return View(detStf);
        }


        //add edit method


        [HttpGet]
        public ActionResult AddStaff(int? id)
        {
            StaffM obStaff = new StaffM();
            db = new SRMSDbContext();
            var gender = db.Genders.Select(x => x.GenderName).ToList();
            if (id == null)
            {

                ViewBag.GenderList = gender.AsEnumerable();
                return View();
            }
            else
                ViewBag.GenderList = gender.AsEnumerable();
            var EditStaff = db.Staff_Teacher.SingleOrDefault(x => x.Id == id);
            obStaff.Name = EditStaff.Name;
            obStaff.DOB = EditStaff.DOB;
            obStaff.Sex = EditStaff.Sex;
            obStaff.Mobile = EditStaff.Mobile;
            obStaff.Address = EditStaff.Address;
            obStaff.Degicnetion = EditStaff.Degicnetion;
            obStaff.Spciallity = EditStaff.Spciallity;
            obStaff.Salary = EditStaff.Salary;
            obStaff.FatherName = EditStaff.FatherName;
            obStaff.MotherName = EditStaff.MotherName;
            obStaff.FathersNumber = EditStaff.FathersNumber;
            obStaff.MothersNumber = EditStaff.MothersNumber;
            obStaff.PermanentAddress = EditStaff.PermanentAddress;
            obStaff.Type = EditStaff.Type;
            obStaff.Email = EditStaff.Email;
            obStaff.ImagePath = EditStaff.ImagePath;
            obStaff.UserName = EditStaff.UserName;
            obStaff.Password = EditStaff.Password;
            obStaff.YearOfTheService = EditStaff.YearOfTheService;

            return View(obStaff);
        }


        [HttpPost]
        public ActionResult AddStaff(StaffM M, int? id)
        {
            if (id == null)
            {
                if (M.Sex == null || M.Mobile == null || M.UserName == null || M.Password == null || M.Email == null)
                {
                    return RedirectToAction("AddStaff", "Teacher");
                }
                else
                    db = new SRMSDbContext();
                //Image Save into Project
                string fileName = Path.GetFileNameWithoutExtension(M.ImageFile.FileName);
                string fileExtention = Path.GetExtension(M.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + fileExtention;
                M.ImagePath = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                M.ImageFile.SaveAs(fileName);
                //Get Gender in View
                var gender = db.Genders.Select(x => x.GenderName).ToList();
                ViewBag.GenderList = gender.AsEnumerable();
                //Add Database in Staff
                Staff_Teacher obStf = new Staff_Teacher();
                obStf.Name = M.Name;
                obStf.DOB = M.DOB;
                obStf.Sex = M.Sex;
                obStf.Mobile = M.Mobile;
                obStf.Address = M.Address;
                obStf.YearOfTheService = DateTime.Now;
                obStf.Degicnetion = M.Degicnetion;
                obStf.Spciallity = M.Spciallity;
                obStf.Salary = M.Salary;
                obStf.FatherName = M.FatherName;
                obStf.MotherName = M.MotherName;
                obStf.FathersNumber = M.FathersNumber;
                obStf.MothersNumber = M.MothersNumber;
                obStf.PermanentAddress = M.PermanentAddress;
                obStf.Type = M.Type;
                obStf.Email = M.Email;
                obStf.UserName = M.UserName;
                obStf.Password = M.Password;
                //Image Save In Database
                obStf.ImagePath = M.ImagePath;
                db.Staff_Teacher.Add(obStf);

                db.SaveChanges();
                return View();
            }
            else
                db = new SRMSDbContext();
            var editStaf = db.Staff_Teacher.SingleOrDefault(y => y.Id == id);
            var dbStf = db.Staff_Teacher.SingleOrDefault(x => x.Id == id);


            editStaf.Name = dbStf.Name;
            editStaf.DOB = dbStf.DOB;
            editStaf.Sex = M.Sex;
            editStaf.Mobile = M.Mobile;
            editStaf.Address = dbStf.Address;
            editStaf.Degicnetion = dbStf.Degicnetion;
            editStaf.Spciallity = dbStf.Spciallity;
            editStaf.Salary = dbStf.Salary;
            editStaf.FatherName = dbStf.FatherName;
            editStaf.MotherName = dbStf.MotherName;
            editStaf.MothersNumber = dbStf.MothersNumber;
            editStaf.PermanentAddress = dbStf.PermanentAddress;
            editStaf.Type = dbStf.Type;
            editStaf.Email = M.Email;
            editStaf.ImagePath = editStaf.ImagePath;
            editStaf.UserName = M.UserName;
            editStaf.Password = M.Password;
            editStaf.YearOfTheService = editStaf.YearOfTheService;
            ViewBag.GenderList = db.Genders.ToList();

            if (M.ImageFile != null)
            {
                //Image Save into Project
                string fileName = Path.GetFileNameWithoutExtension(M.ImageFile.FileName);
                string fileExtention = Path.GetExtension(M.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + fileExtention;
                M.ImagePath = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                M.ImageFile.SaveAs(fileName);
                editStaf.ImagePath = M.ImagePath;
            }


            db.SaveChanges();
            return RedirectToAction("Details", "Teacher", new { id = id });

        }



        public ActionResult AssinCourse()
        {
            var id = Session["Id"];
            string name = Convert.ToString(Session["FullName"]);
            string specallity = Convert.ToString(Session["Specallity"]);
            db = new SRMSDbContext();
            var curseassin = db.AssinCourses.Where(x => x.TeacherName == name && x.Spciallity == specallity);

            return View(curseassin.ToList());
        }

        public ActionResult Students(int id)
        {
            db = new SRMSDbContext();
            var stdList = db.AssinCourses.SingleOrDefault(x => x.Id == id);
            var allStd = db.Students.Where(x => x.Batch == stdList.Batch && x.Semester == stdList.Semester && x.Dept == stdList.Dept && x.Status == "True");

            return View(allStd.ToList());
        }


        public ActionResult MidAndFinalMark()
        {
            var id = Session["Id"];
            string name = Convert.ToString(Session["FullName"]);
            string specallity = Convert.ToString(Session["Specallity"]);
            db = new SRMSDbContext();
            var curseassin = db.AssinCourses.Where(x => x.TeacherName == name && x.Spciallity == specallity);

            return View(curseassin.ToList());
        }

        [HttpGet]
        public ActionResult MidAndFinal(int id)
        {
            ClassTestPresentationController.StdUid = id;

            db = new SRMSDbContext();

            var stdList = db.AssinCourses.SingleOrDefault(x => x.Id == id);

            ClassTestPresentationController.StdCourse = stdList.CourseTitle;
            //Assin for delete mark
            ClassTestPresentationController.StdBatch = stdList.Batch;
            ClassTestPresentationController.StdSemester = stdList.Semester;
            ClassTestPresentationController.StdDept = stdList.Dept;

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


        public ActionResult Message()
        {

            return RedirectToAction("MidAndFinal", "Teacher", new { id = ClassTestPresentationController.StdUid });
        }


        public ActionResult MidTerm(string Pid, string MidMark, string StdId, string CourseName, string CourseCode)
        {
            if (MidMark!="")
            {
                if (Convert.ToDouble(MidMark) < 0 || Convert.ToDouble(MidMark) > 25)
                {
                    return Message();
                }
            }
           


            db = new SRMSDbContext();
            int id = Convert.ToInt32(StdId);
            var std = db.Students.SingleOrDefault(x => x.ID == id);

            MidTermMark mtm = new MidTermMark();
            mtm.StdDBId = Convert.ToString(StdId);
            mtm.AcId = std.AccountID;
            mtm.Batch = std.Batch;
            mtm.CourseCode = CourseCode;
            mtm.CourseName = CourseName;
            mtm.Dept = std.Dept;
            mtm.Roll = std.Roll;
            mtm.Semester = std.Semester;
            mtm.StdMobile = std.StdMobile;
            mtm.StdName = std.Name;
            if (string.IsNullOrEmpty(MidMark))
            {
                mtm.MidMark = 0;
            }
            else
            {
                mtm.MidMark = Convert.ToDouble(MidMark);
            }

            int myPid = Convert.ToInt32(Pid);
            var arether = db.MidTermMarks.Where(x => x.CourseName == mtm.CourseName && x.CourseCode == mtm.CourseCode && x.AcId == mtm.AcId && x.Batch == mtm.Batch && x.Semester == mtm.Semester && x.Dept == mtm.Dept && x.Roll == mtm.Roll && x.StdName == mtm.StdName).ToList();
            if (arether.Count == 0)
            {
                db.MidTermMarks.Add(mtm);
                db.SaveChanges();
                return RedirectToAction("MidAndFinal", "Teacher", new { id = ClassTestPresentationController.StdUid });
            }
            return RedirectToAction("MidAndFinal", "Teacher", new { id = ClassTestPresentationController.StdUid });
        }



        public JsonResult DeleteMtm(int id)
        {
            db = new SRMSDbContext();
            string name = Convert.ToString(Session["FullName"]);
            var assinCourse = db.AssinCourses.Where(x => x.TeacherName == name);

            var orgCourse = assinCourse.SingleOrDefault(x => x.CourseTitle == ClassTestPresentationController.StdCourse&&x.Batch== ClassTestPresentationController.StdBatch&&x.Semester== ClassTestPresentationController.StdSemester&&x.Dept== ClassTestPresentationController.StdDept);

            var deletePtMark = db.MidTermMarks.Where(x => x.StdDBId == id.ToString());

            var orgDelete = deletePtMark.SingleOrDefault(x => x.CourseName == orgCourse.CourseTitle);

            if (deletePtMark != null && orgDelete != null)
            {
                db.MidTermMarks.Remove(orgDelete);
                db.SaveChanges();
                return Json(new { success = true, responseText = "Presentetion Mark Successfuly Delete!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Presentetion Mark Not Found!" }, JsonRequestBehavior.AllowGet);
            }


        }


        public ActionResult Final(int id)
        {
            ClassTestPresentationController.StdUid = id;

            db = new SRMSDbContext();

            var stdList = db.AssinCourses.SingleOrDefault(x => x.Id == id);

            ClassTestPresentationController.StdCourse = stdList.CourseTitle;
            //Delete final mark 
            ClassTestPresentationController.StdBatch = stdList.Batch;
            ClassTestPresentationController.StdSemester = stdList.Semester;
            ClassTestPresentationController.StdDept = stdList.Dept;

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

  

        public ActionResult FinalMessage()
        {
           return RedirectToAction("Final", "Teacher", new { id = ClassTestPresentationController.StdUid });
        }

        public ActionResult FinalTerm(string Pid, string FinalMark, string StdId, string CourseName, string CourseCode)
        {

            if (FinalMark != "")
            {
                if (Convert.ToDouble(FinalMark) < 0 || Convert.ToDouble(FinalMark) > 50)
                {
                    return FinalMessage();
                }
            }




            db = new SRMSDbContext();
            int id = Convert.ToInt32(StdId);
            var std = db.Students.SingleOrDefault(x => x.ID == id);

            FinalTermMark ftm = new FinalTermMark();
            ftm.StdDBId = Convert.ToString(StdId);
            ftm.AcId = std.AccountID;
            ftm.Batch = std.Batch;
            ftm.CourseCode = CourseCode;
            ftm.CourseName = CourseName;
            ftm.Dept = std.Dept;
            ftm.Roll = std.Roll;
            ftm.Semester = std.Semester;
            ftm.StdMobile = std.StdMobile;
            ftm.StdName = std.Name;
            if (string.IsNullOrEmpty(FinalMark))
            {
                ftm.FinalMark = 0;
            }
            else
            {
                ftm.FinalMark = Convert.ToDouble(FinalMark);
            }

            int myPid = Convert.ToInt32(Pid);
            var arether = db.FinalTermMarks.Where(x => x.CourseName == ftm.CourseName && x.CourseCode == ftm.CourseCode && x.AcId == ftm.AcId && x.Batch == ftm.Batch && x.Semester == ftm.Semester && x.Dept == ftm.Dept && x.Roll == ftm.Roll && x.StdName == ftm.StdName).ToList();
            if (arether.Count == 0)
            {
                db.FinalTermMarks.Add(ftm);
                db.SaveChanges();
                return RedirectToAction("Final", "Teacher", new { id = ClassTestPresentationController.StdUid });
                //return Json(new { success = true, responseText = "Final Term Mark Not Found!" }, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Final", "Teacher", new { id = ClassTestPresentationController.StdUid });
        }


        public JsonResult DeleteFtm(int id)
        {
            db = new SRMSDbContext();
            string name = Convert.ToString(Session["FullName"]);
            var assinCourse = db.AssinCourses.Where(x => x.TeacherName == name);

            var orgCourse = assinCourse.SingleOrDefault(x => x.CourseTitle == ClassTestPresentationController.StdCourse&&x.Batch== ClassTestPresentationController.StdBatch&&x.Semester== ClassTestPresentationController.StdSemester&&x.Dept== ClassTestPresentationController.StdDept);

            var deletePtMark = db.FinalTermMarks.Where(x => x.StdDBId == id.ToString());

            var orgDelete = deletePtMark.SingleOrDefault(x => x.CourseName == orgCourse.CourseTitle);

            if (deletePtMark != null && orgDelete != null)
            {
                db.FinalTermMarks.Remove(orgDelete);
                db.SaveChanges();
                return Json(new { success = true, responseText = "Final Term Mark Successfuly Delete!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Final Term Mark Not Found!" }, JsonRequestBehavior.AllowGet);
            }


        }



    }
}