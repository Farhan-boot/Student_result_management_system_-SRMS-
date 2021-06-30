using SRMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRMS.Controllers
{
    public class NoticeBoardController : Controller
    {
        SRMSDbContext db = new SRMSDbContext();
        // GET: NoticeBoard
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Notice()
        {

            //  GetNoticeData();
            return View();
        }

        [HttpGet]
        public JsonResult GetNoticeData()
        {
            string name = Convert.ToString(Session["FullName"]);
            string specallity = Convert.ToString(Session["Specallity"]);
            NoticeBoard notice = new NoticeBoard();
            var Note = db.AssinCourses.Where(x => x.TeacherName == name && x.Spciallity == specallity).ToList();

            return Json(Note, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Notice(NoticeBoard notice)
        {
            if (notice.Batch != null && notice.CourseCode != null && notice.CourseName != null && notice.Dept != null && notice.NoticeBody != null && notice.NoticeTitle != null && notice.Semester != null)
            {
                NoticeBoard note = new NoticeBoard();
                string name = Convert.ToString(Session["FullName"]);

                if (name != null || name != "")
                {
                    note.Batch = notice.Batch;
                    note.CourseCode = notice.CourseCode;
                    note.CourseName = notice.CourseName;
                    note.Date = DateTime.Now;
                    note.Dept = notice.Dept;
                    note.NoticeBody = notice.NoticeBody;
                    note.NoticeTitle = notice.NoticeTitle;
                    note.Semester = notice.Semester;
                    note.TeacherName = name.ToString();

                    SRMSDbContext db = new SRMSDbContext();
                    db.NoticeBoards.Add(note);
                    db.SaveChanges();
                    return View();
                }

            }

            return View();
        }

        [HttpGet]
        public JsonResult ViewBatchResult()
        {
            db = new SRMSDbContext();
            //for search student table
            var Name = AdminController.StdName;
            var Roll = AdminController.StdRoll;
            var stdDetelse = db.Students.SingleOrDefault(x => x.Name == Name && x.Roll == Roll);

            var batchResult = db.NoticeBoards.Where(x => x.Batch == stdDetelse.Batch && x.Semester == stdDetelse.Semester && x.Dept == stdDetelse.Dept).ToList();

            var noticeFont = batchResult.Reverse<NoticeBoard>().ToList().Take(4).ToList();


            return Json(noticeFont, JsonRequestBehavior.AllowGet);

        }

        public class MyNotice
        {
            public string id { get; set; }
        }
        public static int sid;
        [HttpPost]
        public JsonResult NoticeDeteles(MyNotice myId)
        {
            SRMSDbContext db = new SRMSDbContext();
            sid = Convert.ToInt32(myId.id);
            var pageNoticeDeteles = from det in db.NoticeBoards where det.ID == sid select det;
            return Json(pageNoticeDeteles, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ViewAllBatchNotice()
        {
            SRMSDbContext db = new SRMSDbContext();

            //for search student table
            var Name = AdminController.StdName;
            var Roll = AdminController.StdRoll;
            var stdDetelse = db.Students.SingleOrDefault(x => x.Name == Name && x.Roll == Roll);

            var allNotice = db.NoticeBoards.Where(x => x.Batch == stdDetelse.Batch && x.Semester == stdDetelse.Semester && x.Dept == stdDetelse.Dept).ToList();



            return View(allNotice.Reverse<NoticeBoard>().ToList());
        }




        public ActionResult Details(int id)
        {

            SRMSDbContext db = new SRMSDbContext();
            var det = db.NoticeBoards.SingleOrDefault(x => x.ID ==id);

            return View(det);
        }

  


    }
}