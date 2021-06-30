using Rotativa;
using SRMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IronPdf;
namespace SRMS.Controllers
{
    public class MarkProcessController : Controller
    {
        SRMSDbContext db;
        // GET: MarkProcess
        public ActionResult MidMarkProcess()
        {
            //var id = Session["Id"];
            string name = Convert.ToString(Session["FullName"]);
            string specallity = Convert.ToString(Session["Specallity"]);
            db = new SRMSDbContext();
            var curseassin = db.AssinCourses.Where(x => x.TeacherName == name && x.Spciallity == specallity);

            return View(curseassin.ToList());
        }

        public ActionResult FinalMarkProcess()
        {
            //var id = Session["Id"];
            string name = Convert.ToString(Session["FullName"]);
            string specallity = Convert.ToString(Session["Specallity"]);
            db = new SRMSDbContext();
            var curseassin = db.AssinCourses.Where(x => x.TeacherName == name && x.Spciallity == specallity);
            return View(curseassin.ToList());
        }



        public ActionResult HandMark(string Pid, string ClassTest, string Assignment)
        {
            db = new SRMSDbContext();
            int myId = Convert.ToInt32(Pid);
            //help
            var assinCourse = db.AssinCourses.SingleOrDefault(x => x.Id == myId);
            var stdList = db.Students.Where(x => x.Batch == assinCourse.Batch && x.Dept == assinCourse.Dept && x.Semester == assinCourse.Semester).ToList();
            var idList = stdList.Select(x => x.ID);
            //Presentations mark

            List<Presentation> PresentationMarks = new List<Presentation>();
            foreach (var itemId in idList)
            {
                var Marks = db.Presentations.Where(x => x.StdDBId == itemId && x.CourseCode == assinCourse.CourseCode && x.CourseName == assinCourse.CourseTitle).ToList();
                PresentationMarks.AddRange(Marks);
            }

            //Attendance mark
            var RollList = stdList.Select(x => x.Roll);
            var ACList = stdList.Select(x => x.AccountID);
            var BarchList = stdList.Select(x => x.Batch);
            var SemesList = stdList.Select(x => x.Semester);

            //ByStdId
            List<Attendance> AttendanceMarksByStdId = new List<Attendance>();
            //AttendanceCount Class
            List<AttendanceCount> attendanceCount = new List<AttendanceCount>();

            var TotalDate = AttendanceMarksByStdId.Select(x => x.DateOfAttendance).Distinct();
            int TotalDateCount = 0;

            foreach (var itemId in RollList)
            {
                var AttMarks = db.Attendances.Where(x => x.StdId == itemId && x.CourseCode == assinCourse.CourseCode && x.CourseName == assinCourse.CourseTitle).ToList();
                AttendanceMarksByStdId.AddRange(AttMarks);
            }

            //total date attendance count loop
            foreach (var item in TotalDate)
            {
                TotalDateCount++;
            }

            foreach (var item in PresentationMarks)
            {
                var attenCount = AttendanceMarksByStdId.Where(x => x.StdName == item.StdName && x.Semester == item.Semester && x.StdId == item.Roll).Count();
                AttendanceCount obj = new AttendanceCount();
                obj.CourseName = item.CourseName;
                obj.CourseCode = item.CourseCode;
                obj.Ac = item.AcId;
                obj.Batch = item.Batch;
                obj.Semester = item.Semester;
                obj.Dept = item.Dept;
                obj.Roll = item.Roll;
                obj.StdName = item.StdName;
                obj.StdDBId = item.StdDBId;
                //add attendance Count in 10% algorithom 
                double persent = (double)attenCount / TotalDateCount;
                if (Double.IsNaN(persent))
                {
                    persent = 0;
                }

                // double persent = (double) 15 /20;
                double hundatpersent = (double)persent * 100;
                double tenpersent = (double)hundatpersent * 10;
                double nettenper = (double)tenpersent / 100;
                obj.Atten = nettenper;
                attendanceCount.Add(obj);

            }


            //ClassTest process
            List<Class_Test> cls = new List<Class_Test>();
            foreach (var itemId in PresentationMarks)
            {
                var classTest = db.Class_Test.Where(x => x.StdDBId == itemId.StdDBId && x.CourseCode == assinCourse.CourseCode && x.CourseName == assinCourse.CourseTitle).ToList();
                cls.AddRange(classTest);
            }

            List<CT> ct = new List<CT>();
            double CT = 0;
            foreach (var item in cls)
            {
                CT = Convert.ToDouble(item.CTone + item.CTtwo + item.CTthree + item.CTFour + item.CTfive);
                //assin
                CT o = new CT();
                o.numberCt = CT;
                o.StdDBId = item.StdDBId.ToString();
                ct.Add(o);
            }


            int ctOp = Convert.ToInt32(ClassTest);
            List<CT> ctAV = new List<CT>();
            if (ctOp == 0)
            {
                double d = 0;
                foreach (var item in ct)
                {
                    d = (double)item.numberCt / 5;
                    CT o = new CT();
                    o.numberCt = d;
                    o.StdDBId = item.StdDBId;
                    ctAV.Add(o);
                }


            }
            else if (ctOp == 1)
            {

                double bigNum = 0;
                foreach (var item in cls)
                {

                    if (item.CTone >= item.CTtwo && item.CTone >= item.CTthree && item.CTone >= item.CTFour && item.CTone >= item.CTfive)
                    {
                        bigNum = Convert.ToDouble(item.CTone);
                    }
                    else if (item.CTtwo >= item.CTone && item.CTtwo >= item.CTthree && item.CTtwo >= item.CTFour && item.CTtwo >= item.CTfive)
                    {
                        bigNum = Convert.ToDouble(item.CTtwo);
                    }
                    else if (item.CTthree >= item.CTone && item.CTthree >= item.CTtwo && item.CTthree >= item.CTFour && item.CTthree >= item.CTfive)
                    {
                        bigNum = Convert.ToDouble(item.CTthree);
                    }
                    else if (item.CTFour >= item.CTone && item.CTFour >= item.CTtwo && item.CTFour >= item.CTthree && item.CTFour >= item.CTfive)
                    {
                        bigNum = Convert.ToDouble(item.CTFour);
                    }
                    else
                    {
                        bigNum = Convert.ToDouble(item.CTfive);
                    }

                    //assin
                    CT o = new CT();
                    o.numberCt = bigNum;
                    o.StdDBId = item.StdDBId.ToString();
                    ctAV.Add(o);

                }

            }
            else
            {

            }




            //Assinment process
            List<Assignment> assin = new List<Assignment>();
            foreach (var itemId in PresentationMarks)
            {
                var AssinMent = db.Assignments.Where(x => x.StdDBId == itemId.StdDBId.ToString() && x.CourseCode == assinCourse.CourseCode && x.CourseName == assinCourse.CourseTitle).ToList();
                assin.AddRange(AssinMent);
            }

            List<AssinCls> assinCls = new List<AssinCls>();
            double AssinCount = 0;
            foreach (var item in assin)
            {
                AssinCount = Convert.ToDouble(item.ASone + item.AStwo + item.ASthree + item.ASfour + item.ASfive + item.ASsix + item.ASseven + item.ASeight + item.ASnine + item.ASten);
                //assin
                AssinCls o = new AssinCls();
                o.number = AssinCount;
                o.StdDBId = item.StdDBId.ToString();
                assinCls.Add(o);
            }


            int assignmentNum = Convert.ToInt32(Assignment);
            List<AssinCls> asAV = new List<AssinCls>();
            if (assignmentNum == 0)
            {
                double d = 0;
                foreach (var item in assinCls)
                {
                    d = (double)item.number / 10;
                    AssinCls o = new AssinCls();
                    o.number = d;
                    o.StdDBId = item.StdDBId;
                    asAV.Add(o);
                }


            }
            else if (assignmentNum == 1)
            {

                double bigNum = 0;
                foreach (var item in assin)
                {

                    if (item.ASone >= item.AStwo && item.ASone >= item.ASthree && item.ASone >= item.ASfour && item.ASone >= item.ASfive && item.ASone >= item.ASsix && item.ASone >= item.ASseven && item.ASone >= item.ASeight && item.ASone >= item.ASnine && item.ASone >= item.ASten)
                    {
                        bigNum = Convert.ToDouble(item.ASone);
                    }
                    else if (item.AStwo >= item.ASone && item.AStwo >= item.ASthree && item.AStwo >= item.ASfour && item.AStwo >= item.ASfive && item.AStwo >= item.ASsix && item.AStwo >= item.ASseven && item.AStwo >= item.ASeight && item.AStwo >= item.ASnine && item.AStwo >= item.ASten)
                    {
                        bigNum = Convert.ToDouble(item.AStwo);
                    }
                    else if (item.ASthree >= item.ASone && item.ASthree >= item.AStwo && item.ASthree >= item.ASfour && item.ASthree >= item.ASfive && item.ASthree >= item.ASsix && item.ASthree >= item.ASseven && item.ASthree >= item.ASeight && item.ASthree >= item.ASnine && item.ASthree >= item.ASten)
                    {
                        bigNum = Convert.ToDouble(item.ASthree);
                    }
                    else if (item.ASfour >= item.ASone && item.ASfour >= item.AStwo && item.ASfour >= item.ASthree && item.ASfour >= item.ASfive && item.ASfour >= item.ASsix && item.ASfour >= item.ASseven && item.ASfour >= item.ASeight && item.ASfour >= item.ASnine && item.ASfour >= item.ASten)
                    {
                        bigNum = Convert.ToDouble(item.ASfour);
                    }

                    else if (item.ASfive >= item.ASone && item.ASfive >= item.AStwo && item.ASfive >= item.ASthree && item.ASfive >= item.ASfour && item.ASfive >= item.ASsix && item.ASfive >= item.ASseven && item.ASfive >= item.ASeight && item.ASfive >= item.ASnine && item.ASfive >= item.ASten)
                    {
                        bigNum = Convert.ToDouble(item.ASfive);
                    }


                    else if (item.ASsix >= item.ASone && item.ASsix >= item.AStwo && item.ASsix >= item.ASthree && item.ASsix >= item.ASfour && item.ASsix >= item.ASfive && item.ASsix >= item.ASseven && item.ASsix >= item.ASeight && item.ASsix >= item.ASnine && item.ASsix >= item.ASten)
                    {
                        bigNum = Convert.ToDouble(item.ASsix);
                    }

                    else if (item.ASseven >= item.ASone && item.ASseven >= item.AStwo && item.ASseven >= item.ASthree && item.ASseven >= item.ASfour && item.ASseven >= item.ASfive && item.ASseven >= item.ASsix && item.ASseven >= item.ASeight && item.ASseven >= item.ASnine && item.ASseven >= item.ASten)
                    {
                        bigNum = Convert.ToDouble(item.ASseven);
                    }

                    else if (item.ASeight >= item.ASone && item.ASeight >= item.AStwo && item.ASeight >= item.ASthree && item.ASeight >= item.ASfour && item.ASeight >= item.ASfive && item.ASeight >= item.ASsix && item.ASeight >= item.ASseven && item.ASeight >= item.ASnine && item.ASeight >= item.ASten)
                    {
                        bigNum = Convert.ToDouble(item.ASeight);
                    }

                    else if (item.ASnine >= item.ASone && item.ASnine >= item.AStwo && item.ASnine >= item.ASthree && item.ASnine >= item.ASfour && item.ASnine >= item.ASfive && item.ASnine >= item.ASsix && item.ASnine >= item.ASseven && item.ASnine >= item.ASeight && item.ASnine >= item.ASten)
                    {
                        bigNum = Convert.ToDouble(item.ASnine);
                    }


                    else
                    {
                        bigNum = Convert.ToDouble(item.ASten);
                    }

                    //assin
                    AssinCls o = new AssinCls();
                    o.number = bigNum;
                    o.StdDBId = item.StdDBId.ToString();
                    asAV.Add(o);

                }

            }
            else
            {

            }



            // var result= PresentationMarks.Select(x=>x.)


            var result = from als in PresentationMarks
                         from als2 in asAV
                         from als3 in ctAV
                         from als4 in attendanceCount
                         where als.StdDBId.ToString() == als2.StdDBId && als3.StdDBId == als.StdDBId.ToString() && als4.StdDBId.ToString() == als2.StdDBId.ToString()
                         select new { als.StdDBId, als.PresentationMark, als3.numberCt, als2.number, als4.Atten };



            var FinalResult = from als in result
                              from data in PresentationMarks
                              where als.StdDBId == data.StdDBId
                              select new { data.StdName, data.Roll, data.AcId, data.Batch, data.Semester, data.CourseName, data.CourseCode, data.Dept, DateTime.Now, als.StdDBId, als.Atten, (als.numberCt + als.number + als.PresentationMark).Value };

            //condition loop by insert
            List<Handmark> findResultDB = new List<Handmark>();
            foreach (var dbR in FinalResult)
            {
                findResultDB = db.Handmarks.Where(x => x.StdAc == dbR.AcId && x.Batch == dbR.Batch && x.CourseCode == dbR.CourseCode && x.CourseName == dbR.CourseName && x.Dept == dbR.Dept && x.StdId == dbR.Roll && x.Semester == dbR.Semester && x.StdDBId == dbR.StdDBId.ToString() && x.StdName == dbR.StdName).ToList();
            }

            if (findResultDB.Count == 0)
            {
                Handmark objs = new Handmark();
                foreach (var results in FinalResult)
                {
                    objs.StdName = results.StdName;
                    objs.StdId = results.Roll;
                    objs.StdAc = results.AcId;
                    objs.Batch = results.Batch;
                    objs.Semester = results.Semester;
                    objs.CourseName = results.CourseName;
                    objs.CourseCode = results.CourseCode;
                    objs.Date = DateTime.Now;
                    objs.StdDBId = results.StdDBId.ToString();
                    objs.Dept = results.Dept;
                    objs.AttendanceMarks = results.Atten;
                    objs.OthersMarks = results.Value;
                    if (objs != null)
                    {
                        db.Handmarks.Add(objs);
                    }
                    db.SaveChanges();
                }
            }



            return RedirectToAction("Teacher", "Teacher");
        }

        //custom class
        internal class AttendanceCount
        {
            public string Ac { get; set; }
            public string Batch { get; set; }
            public string CourseCode { get; set; }
            public string CourseName { get; set; }
            public string Dept { get; set; }
            public string Roll { get; set; }
            public string Semester { get; set; }
            public string StdName { get; set; }

            public int StdDBId { get; set; }
            public double Atten { get; set; }

        }
        internal class CT
        {
            public string StdDBId { get; set; }
            public double numberCt { get; set; }
        }
        internal class AssinCls
        {
            public string StdDBId { get; set; }
            public double number { get; set; }
        }
        // DeletePubHandMark
        public ActionResult DeletePubHandMark(int id)
        {
            db = new SRMSDbContext();
            var resultAssin = db.AssinCourses.SingleOrDefault(x => x.Id == id);
            var resultHandMark = db.Handmarks.Where(x => x.Dept == resultAssin.Dept && x.Batch == resultAssin.Batch && x.Semester == resultAssin.Semester && x.CourseName == resultAssin.CourseTitle && x.CourseCode == resultAssin.CourseCode).ToList();
            //Delete loop
            if (resultHandMark.Count != 0)
            {
                foreach (var myReselt in resultHandMark)
                {
                    db.Handmarks.Remove(myReselt);
                    db.SaveChanges();
                }
                return Json(new { success = true, responseText = "All Heand-Mark Successfuly Delete!" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { success = true, responseText = "Heand-Mark Not Submit !" }, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult ViewResult(int id)
        {
            Session["ViewResultId"] = id;
            db = new SRMSDbContext();
            var resultAssin = db.AssinCourses.SingleOrDefault(x => x.Id == id);
            var resultHandMark = db.Handmarks.Where(x => x.Dept == resultAssin.Dept && x.Batch == resultAssin.Batch && x.Semester == resultAssin.Semester && x.CourseName == resultAssin.CourseTitle && x.CourseCode == resultAssin.CourseCode).ToList<Handmark>();
            //Delete loop
            if (resultHandMark.Count != 0 && resultHandMark != null || resultHandMark.Any())
            {
                return View(resultHandMark);
            }
            else
                return RedirectToAction("Teacher", "Teacher");
        }
        //Download PDF
        public ActionResult DownloadPDF(int id)
        {


            return new ActionAsPdf("ViewResult", new { id = id })
            {
                FileName = "Results.pdf"
            };

        }




        // GET: Mid-term Mark Process
        public ActionResult MidTermMarkProcess()
        {
            //var id = Session["Id"];
            string name = Convert.ToString(Session["FullName"]);
            string specallity = Convert.ToString(Session["Specallity"]);
            db = new SRMSDbContext();
            var curseassin = db.AssinCourses.Where(x => x.TeacherName == name && x.Spciallity == specallity);

            return View(curseassin.ToList());
        }

         //for mid view
        public ActionResult MidViewResult(int id)
        {
          
           // Session["ViewResultId"] = id;
            db = new SRMSDbContext();
            var resultAssin = db.AssinCourses.SingleOrDefault(x => x.Id == id);
            List<MidTermMark> resultMidMark = new List<MidTermMark>();
            resultMidMark = db.MidTermMarks.Where(x => x.Dept == resultAssin.Dept && x.Batch == resultAssin.Batch && x.Semester == resultAssin.Semester && x.CourseName == resultAssin.CourseTitle && x.CourseCode == resultAssin.CourseCode).ToList();
            //Chack and pass the view loop
            if (resultMidMark.Count != 0)
            {
                return View(resultMidMark.ToList());
            }
            else
                return RedirectToAction("Teacher", "Teacher"); ;
        }


         //for final view
        public ActionResult FinalViewResult(int id)
        {

            // Session["ViewResultId"] = id;
            db = new SRMSDbContext();
            var resultAssin = db.AssinCourses.SingleOrDefault(x => x.Id == id);
            List<FinalTermMark> resultFinalMark = new List<FinalTermMark>();
            resultFinalMark = db.FinalTermMarks.Where(x => x.Dept == resultAssin.Dept && x.Batch == resultAssin.Batch && x.Semester == resultAssin.Semester && x.CourseName == resultAssin.CourseTitle && x.CourseCode == resultAssin.CourseCode).ToList();
            //Chack and pass the view loop
            if (resultFinalMark.Count != 0)
            {
                return View(resultFinalMark.ToList());
            }
            else
                return RedirectToAction("Teacher", "Teacher"); ;
        }







    }
}