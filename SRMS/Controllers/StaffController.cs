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
    public class StaffController : Controller
    {
        SRMSDbContext db;
        [HttpGet]
        public ActionResult AddStaff(int? id)
        {
            StaffM obStaff = new StaffM();
            db = new SRMSDbContext();
          var gender = db.Genders.Select(x => x.GenderName).ToList();
            if (id==null)
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
        public ActionResult AddStaff(StaffM M,int? id)
        {
            if (id == null)
            {
                if (M.Name == null || M.DOB == null || M.Sex == null || M.Mobile == null || M.Address == null || M.Degicnetion == null || M.Spciallity == null || M.Salary == null || M.FatherName == null || M.MotherName == null || M.FathersNumber == null || M.MothersNumber == null || M.PermanentAddress == null || M.Type == null || M.UserName == null || M.Password == null||M.Email==null)
                {
                    return RedirectToAction("AddStaff", "Staff");
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
                return Json(new { success = true, responseText = "Staff/Faculty successfuly Save!" }, JsonRequestBehavior.AllowGet);
               // return View();
            }
            else
            db = new SRMSDbContext();
            var editStaf = db.Staff_Teacher.SingleOrDefault(y => y.Id == id);
            editStaf.Name = M.Name;
            editStaf.DOB = M.DOB;
            editStaf.Sex = M.Sex;
            editStaf.Mobile = M.Mobile;
            editStaf.Address = M.Address;
            editStaf.Degicnetion = M.Degicnetion;
            editStaf.Spciallity = M.Spciallity;
            editStaf.Salary = M.Salary;
            editStaf.FatherName = M.FatherName;
            editStaf.MotherName = M.MotherName;
            editStaf.MothersNumber = M.MothersNumber;
            editStaf.PermanentAddress = M.PermanentAddress;
            editStaf.Type = M.Type;
            editStaf.Email = M.Email;
            editStaf.ImagePath = editStaf.ImagePath;
            editStaf.UserName = M.UserName;
            editStaf.Password = M.Password;
            editStaf.YearOfTheService = editStaf.YearOfTheService;
            ViewBag.GenderList = db.Genders.ToList();

            if (M.ImageFile!=null)
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
            return View();

        }

        public ActionResult StaffList()
        {
            db = new SRMSDbContext();
            string search = Request["searchByNameMobile"];
            if (string.IsNullOrEmpty(search))
            {
                var gender = db.Staff_Teacher.ToList();
                return View(gender);
            }
            else
            {
                var listOfStaf = db.Staff_Teacher.Where(m => m.Name.ToLower() == search.ToLower() || m.Mobile == search).ToList();
                return View(listOfStaf);
            }

        }



        public ActionResult Delete(int id)
        {
            db = new SRMSDbContext();
            Staff_Teacher deletStf = db.Staff_Teacher.SingleOrDefault(x => x.Id == id);
            db.Staff_Teacher.Remove(deletStf);
            db.SaveChanges();
            return RedirectToAction("StaffList", "Staff");
        }

        public ActionResult Details(int id)
        {
            db = new SRMSDbContext();
            Staff_Teacher detStf = db.Staff_Teacher.SingleOrDefault(x=>x.Id==id);
            return View(detStf);
        }








    }
}