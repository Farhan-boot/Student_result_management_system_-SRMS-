using SRMS.Data;
using SRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRMS.Controllers
{
    public class CariculamController : Controller
    {
        SRMSDbContext db;
        public ActionResult List()
        {
            db = new SRMSDbContext();
            string search = Request["searchByYearSemester"];
            if (string.IsNullOrEmpty(search))
            {
                List<Cariculam> cariList = db.Cariculams.ToList();
                return View(cariList);
            }
            else
            {
                var searchCari = db.Cariculams.Where(x => x.Year.ToLower() == search.ToLower() || x.Semester.ToLower() == search.ToLower()).ToList();
                return View(searchCari);
            }
    

        }

        public ActionResult Delete(int id)
        {
            db = new SRMSDbContext();
            var cariList = db.Cariculams.SingleOrDefault(x=>x.Id==id);
            db.Cariculams.Remove(cariList);
            db.SaveChanges();
            return RedirectToAction("List", "Cariculam");
        }


        [HttpGet]
        public ActionResult AddEdit(int? id,Cariculam m)
        {
            if (id==null)
            {
                return View();
            }
            else
            db = new SRMSDbContext();
            var editCari = db.Cariculams.SingleOrDefault(x=>x.Id==id);
            CariculamM EditM = new CariculamM();
            EditM.Year = editCari.Year;
            EditM.Semester = editCari.Semester;
            EditM.CourseTitle = editCari.CourseTitle;
            EditM.CourseCode = editCari.CourseCode;

            return View(EditM);
        }

        [HttpPost]
        public ActionResult AddEdit(CariculamM m, int? id)
        {
            if (m.Year==null||m.Semester==null||m.CourseTitle==null||m.CourseCode==null)
            {

            }
             else {
                if (id == null)
                {
                    db = new SRMSDbContext();
                    Cariculam obCari = new Cariculam();
                    //Assin Object
                    obCari.Year = m.Year;
                    obCari.Semester = m.Semester;
                    obCari.CourseTitle = m.CourseTitle;
                    obCari.CourseCode = m.CourseCode;
                    //Panding Stet to save database
                    db.Cariculams.Add(obCari);
                    //Save to Database
                    db.SaveChanges();
                    return View();
                }

                else
                    //Edit codding
                    db = new SRMSDbContext();
                    var editCari = db.Cariculams.SingleOrDefault(x=>x.Id==id);
                //Edit Section
                editCari.Year = m.Year;
                editCari.Semester = m.Semester;
                editCari.CourseTitle = m.CourseTitle;
                editCari.CourseCode = m.CourseCode;
                db.SaveChanges();
                return View();
            }
            return View();
        }
    }
}