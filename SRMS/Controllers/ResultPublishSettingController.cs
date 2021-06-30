using SRMS.Data;
using SRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using SRMS.BusinessLayer;

namespace SRMS.Controllers
{
    public class ResultPublishSettingController : Controller
    {
        SRMSDbContext db;
        // GET: ResultPublishSetting For Mid-Term
        public JsonResult ResultPublish()
        {
            List<ResultPublish> resultPublish = new List<Models.ResultPublish>();
            ResultPublish obj = new Models.ResultPublish();
            db = new SRMSDbContext();
            var CariculamCourse = db.Cariculams.ToList();
            var AssinCourse = db.AssinCourses.ToList();
            var SubmitCourse = db.MidTermMarks.ToList();
            //adding process
            obj.CariculamList = CariculamCourse;
            obj.AssinCourseList = AssinCourse;
            obj.MidTermMarkList = SubmitCourse.DistinctBy(x => x.CourseName).ToList();
            resultPublish.Add(obj);


            return Json(resultPublish, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SemesterList()
        {
            List<string> semester = new List<string> {
                "Semester 1","Semester 2",
                "Semester 3","Semester 4",
                "Semester 5","Semester 6",
                "Semester 7","Semester 8",
                "Semester 9","Semester 10",
                "Semester 11","Semester 12"
            };

            return Json(semester, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResultList()
        {
            return View();
        }


        public JsonResult PublishMidMark(List<MidTermMark> data)
        {
            if (data != null)
            {
                db = new SRMSDbContext();
                // dynamic result;
                List<MidTermMark> result = new List<MidTermMark>();
                List<AssinCourse> Assinresult = new List<AssinCourse>();

                foreach (var item in data)
                {
                    var obj = db.MidTermMarks.Where(x => x.Semester == item.Semester && x.CourseName == item.CourseName && x.CourseCode == item.CourseCode && x.Dept == item.Dept).ToList();
                    result.AddRange(obj);
                }
                //Assinresult loop
                foreach (var item in data)
                {
                    var obj = db.AssinCourses.Where(x => x.Semester == item.Semester && x.Dept == item.Dept).ToList();
                    Assinresult.AddRange(obj);
                }

                var SubmitresultCount = result.DistinctBy(x => x.CourseName).Count();
                var AssinResultCount = Assinresult.DistinctBy(x => x.CourseTitle).Count();
                if (AssinResultCount > SubmitresultCount)
                {
                    return Json("Total Assin Course " + AssinResultCount + " But Submit " + SubmitresultCount + " So Result Can,t Process !", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Code init
                    Publish_Mid_Marks pmm = new Publish_Mid_Marks();
                    string[] chackBatch = result.Select(x => x.Batch).Distinct().ToArray();
                    string[] chackDept = result.Select(x => x.Dept).Distinct().ToArray();
                    string chackBatchM = chackBatch[0].ToString();
                    string chackDeptM = chackDept[0].ToString();
                    var chackDB = db.Publish_Mid_Marks.Where(x => x.Batch == chackBatchM && x.Dept == chackDeptM).ToList();

                    if (chackDB.Count != 0) { return Json("Result Already Published ! Batch: " + chackBatchM, JsonRequestBehavior.AllowGet); }
                    else
                        foreach (var onebyone in result)
                        {
                            pmm.StdName = onebyone.StdName;
                            pmm.Roll = onebyone.Roll;
                            pmm.AcId = onebyone.AcId;
                            pmm.Dept = onebyone.Dept;
                            pmm.Semester = onebyone.Semester;
                            pmm.Batch = onebyone.Batch;
                            pmm.CourseName = onebyone.CourseName;
                            pmm.CourseCode = onebyone.CourseCode;
                            pmm.MidMark = onebyone.MidMark;
                            db.Publish_Mid_Marks.Add(pmm);
                            db.SaveChanges();
                        }

                    return Json("Result Process Complicated", JsonRequestBehavior.AllowGet);
                }


            }

            if (data == null)
            {
                return Json("This Data is Invelid !", JsonRequestBehavior.AllowGet);
            }
            return Json("This Data is Not Invelid !", JsonRequestBehavior.AllowGet);
        }

        //View Result
        public static string Search;
        [HttpPost]
        public ActionResult SearchBy(string SearchByBatch)
        {
            Search = SearchByBatch;
            return RedirectToAction("MidTermView");
        }
        public JsonResult MidMarkList()
        {
            db = new SRMSDbContext();
            var mid = db.Publish_Mid_Marks.OrderBy(x => x.StdName).Where(x => x.Batch == Search).ToList();
            //List<CourseInfo> courseList = new List<CourseInfo>();

            var courseList = mid.DistinctBy(x => x.CourseName).Select(x => x.CourseName).OrderBy(x => x).ToList();
            var batchList = mid.DistinctBy(x => x.Batch).Select(x => x.Batch).OrderBy(x => x).ToList();
            var semesterList = mid.DistinctBy(x => x.Semester).Select(x => x.Semester).OrderBy(x => x).ToList();

            var depetmentList = mid.DistinctBy(x => x.Dept).Select(x => x.Dept).OrderBy(x => x).ToList();

            var courseCodeList = mid.DistinctBy(x => x.CourseCode).Select(x => x.CourseCode).OrderBy(x => x).ToList();

            var studentList = mid.DistinctBy(x => x.Roll).Select(x => x.Roll).OrderBy(x => x).ToList();
            List<StudentResult> studentResultList = new List<StudentResult>();

            foreach (var roll in studentList)
            {
                var studentCourseList = mid.Where(x => x.Roll == roll).DistinctBy(x => x.CourseName).ToList();
                var studentResult = StudentResult.GetNew();

                studentResult.StudentName = studentCourseList[0].StdName;
                studentResult.Roll = studentCourseList[0].Roll;
                studentResult.Batch = studentCourseList[0].Batch;
                studentResult.Semester = studentCourseList[0].Semester;

                foreach (var course in courseList)
                {
                    //string marks = "-";
                    var courseMark = new CourseMark();
                    courseMark.Mark = "-";
                    courseMark.CourseCode = course;
                    var courseresult = studentCourseList.SingleOrDefault(x => x.CourseName == course);
                    if (courseresult != null)
                    {
                        courseMark.Mark = courseresult.MidMark.ToString();
                    }
                    studentResult.CourseMarkList.Add(courseMark);
                }
                studentResultList.Add(studentResult);

            }  
            MidResultJson obj = new MidResultJson();
            obj.StudentResultList = studentResultList;
            obj.CourseList = courseList;
            obj.Batch = batchList;
            obj.Semester = semesterList;
            obj.Depetment = depetmentList;
            obj.CourseCodeList = courseCodeList;

            // GetStdName();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MidTermView()
        {
            db = new SRMSDbContext();
            var BatchList = db.Publish_Mid_Marks.Select(x => x.Batch).Distinct().ToList();
            ViewBag.BatchListForddl = BatchList;
            return View();
        }

        public class MidResultJson
        {
            public List<StudentResult> StudentResultList { get; set; }
            public List<string> CourseList { get; set; }
            public List<string> Batch { get; set; }
            public List<string> Semester { get; set; }
            public List<string> Depetment { get; set; }

            public List<string> CourseCodeList { get; set; }
        }

        public class StudentResult
        {
            public string Batch { get; set; }
            public string Semester { get; set; }
            public string StudentName { get; set; }
            public string Roll { get; set; }
            public List<CourseMark> CourseMarkList { get; set; }

            public static StudentResult GetNew()
            {
                var obj = new StudentResult();
                obj.Batch = "";
                obj.Semester = "";
                obj.StudentName = "";
                obj.Roll = "";
                obj.CourseMarkList = new List<CourseMark>();
                return obj;

            }

        }

        public class CourseMark
        {
            public string CourseCode { get; set; }
            public string Mark { get; set; }

        }

        //public JsonResult GetStdName()
        //{
        //    db = new SRMSDbContext();
        //    var mid = db.Publish_Mid_Marks.OrderBy(x => x.StdName).ToList();
        //    var StudentName = mid.Select(x => x.StdName).Distinct().ToList();

        //    return Json(StudentName, JsonRequestBehavior.AllowGet);
        //}
        // GET: ResultPublishSetting For Final-Term

        public JsonResult GetPublishMidList()
        {
            db = new SRMSDbContext();
            var listOfMidResult = db.Publish_Mid_Marks.ToList();
            var listOfUnickBatch = listOfMidResult.Select(x => x.Batch).Distinct().ToList();

            return Json(listOfUnickBatch, JsonRequestBehavior.AllowGet);
        }


        public ActionResult publishFinalMarkSettings()
        {
            return View();
        }

       public JsonResult ListOfMark()
        {
            db = new SRMSDbContext();
            FinalMarkSectionList obj = new FinalMarkSectionList();
            var midMark = db.Publish_Mid_Marks.DistinctBy(x=>x.CourseName).OrderBy(x=>x.CourseName).ToList();
            var handMark = db.Handmarks.DistinctBy(x => x.CourseName).OrderBy(x => x.CourseName).ToList();
            var finalMark = db.FinalTermMarks.DistinctBy(x => x.CourseName).OrderBy(x => x.CourseName).ToList();

            obj.MidMark=midMark;
            obj.HandMark=handMark;
            obj.FinalMark=finalMark;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public class FinalMarkSectionList
        {
            public List<Handmark> HandMark { get; set; }
            public List<Publish_Mid_Marks> MidMark { get; set; }
            public List<FinalTermMark> FinalMark { get; set; }

        }

        // Publish Final Marks Setting
        public JsonResult PublisFinalMark(List<FinalTermMark> data)
        {
            db = new SRMSDbContext();
            Publish_Final_Marks publish_Final_Marks = new Publish_Final_Marks();
            var batch = data.Select(x => x.Batch).Distinct();
            var semester = data.Select(x => x.Semester).Distinct();
            var subjectName = data.Select(x=>x.CourseName);
            //string chack
            string stringBatch ="" ;
            string stringSemester = "";
            foreach (var bat in batch)
            {
                stringBatch = bat;
            }
            foreach (var sem in semester)
            {
                stringSemester = sem;
            }
            List<Publish_Final_Marks> dbChack=null;
            foreach (var sub in subjectName)
            {
                dbChack = db.Publish_Final_Marks.Where(x => x.CourseName == sub && x.Semester == stringSemester && x.Batch == stringBatch).ToList();
            }

            if ((dbChack == null) || (dbChack.Count()==0))
            { 


                if (data != null)
            {
              var courseName = data.Select(x=>x.CourseName).Distinct().ToList();
              var batchName = data.Select(x=>x.Batch).Distinct().ToList();
              var dept = data.Select(x => x.Dept).Distinct().ToList();
                //string Convare
                var stringBatchName="";
                var stringDept="";
                foreach (var item in batchName)
                {
                    stringBatchName = item.ToString();
                }
                foreach (var item in dept)
                {
                    stringDept = item.ToString();
                }
                List<string> countMid = new List<string>();
                List<string> countFinal = new List<string>();
                List<string> countHand = new List<string>();

                foreach (var course in courseName)
                {
                    //for mid
                    var midResult = db.Publish_Mid_Marks.Where(x => x.CourseName == course && x.Batch == stringBatchName && x.Dept == stringDept).ToList();
                    if (midResult.Count() != 0)
                    {
                        countMid.Add(midResult.Select(x => x.CourseName).Distinct().ToString());
                    }
                    //for final
                    var finalResult = db.FinalTermMarks.Where(x => x.CourseName == course && x.Batch == stringBatchName && x.Dept == stringDept).ToList();
                    if (finalResult.Count() != 0)
                    {
                      countFinal.Add(finalResult.Select(x => x.CourseName).Distinct().ToString());
                    }
                    //for Hand
                    var handResult = db.Handmarks.Where(x => x.CourseName == course && x.Batch == stringBatchName && x.Dept == stringDept).ToList();
                    if (handResult.Count() != 0)
                    {
                        countHand.Add(handResult.Select(x => x.CourseName).Distinct().ToString());
                    }
                }
               //count chack
                int midC = countMid.Count();
                int finalC = countFinal.Count();
                int handC = countHand.Count();
                if (midC==finalC&&midC==handC)
                {
                    List<Publish_Mid_Marks> publishMidMarks = new List<Publish_Mid_Marks>();

                    //code init
                    foreach (var course in courseName)
                    {
                        var midResult = db.Publish_Mid_Marks.Where(x => x.CourseName == course && x.Batch == stringBatchName && x.Dept == stringDept).ToList();
                        var finalResult = db.FinalTermMarks.Where(x => x.CourseName == course && x.Batch == stringBatchName && x.Dept == stringDept).ToList();
                        var handResult = db.Handmarks.Where(x => x.CourseName == course && x.Batch == stringBatchName && x.Dept == stringDept).ToList();

                        foreach (var item in midResult)
                        {
                            var result = from mid in midResult
                                         from finl in finalResult
                                         from hand in handResult
                                         where mid.Batch == item.Batch && mid.StdName == item.StdName && mid.Roll == item.Roll && mid.Dept == item.Dept &&
                                               finl.Batch == item.Batch && finl.StdName == item.StdName && finl.Roll == item.Roll && finl.Dept == item.Dept &&
                                               hand.Batch == item.Batch && hand.StdName == item.StdName && hand.StdId == item.Roll && hand.Dept == item.Dept
                                         select new {item.StdName,item.Semester,item.Batch,item.Roll,item.AcId,item.Dept,item.CourseName,item.CourseCode, mid.MidMark,finl.FinalMark,hand.AttendanceMarks,hand.OthersMarks };
                            var total=  result.Select(x => x.MidMark + x.FinalMark + x.AttendanceMarks + x.OthersMarks);
                            double myTotal = 0.00;
                            foreach (var point in total)
                            {
                                myTotal = Convert.ToDouble(point);
                            }
                           var greadPoint= GradePolicy.GreadPoint(Convert.ToDouble(myTotal));
                       
                            foreach (var myItem in result)
                            {
                                publish_Final_Marks.AcId = myItem.AcId;
                                publish_Final_Marks.Batch = myItem.Batch;
                                publish_Final_Marks.CourseCode = myItem.CourseCode;
                                publish_Final_Marks.CourseName = myItem.CourseName;
                                publish_Final_Marks.Dept = myItem.Dept;
                                publish_Final_Marks.Roll = myItem.Roll;
                                publish_Final_Marks.Semester = myItem.Semester;
                                publish_Final_Marks.StdName = myItem.StdName;
                                publish_Final_Marks.TotalMark = myTotal;
                                foreach (var points in greadPoint)
                                {
                                    publish_Final_Marks.Gp = Convert.ToDouble(points.Gp);
                                    publish_Final_Marks.Lg = Convert.ToString(points.Lg);
                                }
                                 //chack police
     
                                db.Publish_Final_Marks.Add(publish_Final_Marks);
                                db.SaveChanges();

                            }
                        }

                    }

                    return Json("This Data is Not Invelid !", JsonRequestBehavior.AllowGet);
                }
                else
                {       
                    return Json("This Data is Invelid !", JsonRequestBehavior.AllowGet);
                }
            }
            else
            return Json("This Data is Invelid !", JsonRequestBehavior.AllowGet);
        }
            else
            {
                return Json("The Result is Alrady Publish", JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult FinalTermView()
        {
            db = new SRMSDbContext();
            var BatchList = db.Publish_Final_Marks.Select(x => x.Batch).Distinct().ToList();
            ViewBag.FinalBatchListForddl = BatchList;
            return View();
        }

        public class FinalResultJson
        {
            public List<StudentFinalResult> StudentResultList { get; set; }
            public List<string> CourseList { get; set; }
            public List<string> Batch { get; set; }
            public List<string> Semester { get; set; }
            public List<string> Depetment { get; set; }

            public List<string> CourseCodeList { get; set; }
        }


        public static string FinalSearch;
        [HttpPost]
        public ActionResult FinalSearchBy(string SearchByBatch)
        {
            FinalSearch = SearchByBatch;
           // FinalMarkList();
            return RedirectToAction("FinalTermView");
        }



        public JsonResult FinalMarkList()
        {
            db = new SRMSDbContext();
            var myFinal = db.Publish_Final_Marks.OrderBy(x => x.StdName).Where(x => x.Batch == FinalSearch).ToList();
            //List<CourseInfo> courseList = new List<CourseInfo>();

            var courseList = myFinal.DistinctBy(x => x.CourseName).Select(x => x.CourseName).OrderBy(x => x).ToList();
            var batchList = myFinal.DistinctBy(x => x.Batch).Select(x => x.Batch).OrderBy(x => x).ToList();
            var semesterList = myFinal.DistinctBy(x => x.Semester).Select(x => x.Semester).OrderBy(x => x).ToList();

            var depetmentList = myFinal.DistinctBy(x => x.Dept).Select(x => x.Dept).OrderBy(x => x).ToList();

            var courseCodeList = myFinal.DistinctBy(x => x.CourseCode).Select(x => x.CourseCode).OrderBy(x => x).ToList();

            var studentList = myFinal.DistinctBy(x => x.Roll).Select(x => x.Roll).OrderBy(x => x).ToList();
            List<StudentFinalResult> studentResultList = new List<StudentFinalResult>();

            foreach (var roll in studentList)
            {
                var studentCourseList = myFinal.Where(x => x.Roll == roll).DistinctBy(x => x.CourseName).ToList();
                var studentResult = StudentFinalResult.GetNew();

                studentResult.StudentName = studentCourseList[0].StdName;
                studentResult.Roll = studentCourseList[0].Roll;
                studentResult.Batch = studentCourseList[0].Batch;
                studentResult.Semester = studentCourseList[0].Semester;

                foreach (var course in courseList)
                {
                    //string marks = "-";
                    var courseMark = new FinalCourseMark();
                    courseMark.TotalMark = "-";
                    courseMark.Gp = "-";
                    courseMark.Lg = "-";

                    courseMark.CourseCode = course;
                    var courseresult = studentCourseList.SingleOrDefault(x => x.CourseName == course);
                    if (courseresult != null)
                    {
                        courseMark.TotalMark = courseresult.TotalMark.ToString();
                        courseMark.Gp = courseresult.Gp.ToString();
                        courseMark.Lg = courseresult.Lg.ToString();
                    }
                    studentResult.CourseMarkList.Add(courseMark);
                }
                studentResultList.Add(studentResult);

            }
            FinalResultJson obj = new FinalResultJson();
            obj.StudentResultList = studentResultList;
            obj.CourseList = courseList;
            obj.Batch = batchList;
            obj.Semester = semesterList;
            obj.Depetment = depetmentList;
            obj.CourseCodeList = courseCodeList;

            // GetStdName();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public class StudentFinalResult
        {
            public string Batch { get; set; }
            public string Semester { get; set; }
            public string StudentName { get; set; }
            public string Roll { get; set; }
            public List<FinalCourseMark> CourseMarkList { get; set; }

            public static StudentFinalResult GetNew()
            {
                var obj = new StudentFinalResult();
                obj.Batch = "";
                obj.Semester = "";
                obj.StudentName = "";
                obj.Roll = "";
                obj.CourseMarkList = new List<FinalCourseMark>();
                return obj;

            }

        }

        public class FinalCourseMark
        {
            public string CourseCode { get; set; }
            public string TotalMark { get; set; }
            public string Gp { get; set; }
            public string Lg { get; set; }

        }






    }
}