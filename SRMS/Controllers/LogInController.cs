using SRMS.Data;
using SRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRMS.Controllers
{
    public class AdminController : Controller
    {
        SRMSDbContext db;
        // Admin login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogInM m)
        {
            if (m.UseName == null || m.Password == null)
            {
                return View();
            }
            else
                //Write login code and intrarection to database
                db = new SRMSDbContext();
            var admin = db.Staff_Teacher.SingleOrDefault(x => x.UserName == m.UseName && x.Password == m.Password);

            if (admin!=null)
            {
                if (admin.Spciallity == "Admin")
                {
                    Session["Id"] = admin.Id;
                    Session["FullName"] = admin.Name;
                    Session["UserName"] = admin.UserName;
                    Session["Img"] = admin.ImagePath;
                    Session["Specallity"] = admin.Spciallity;
                    if (Session["FullName"]!=null && Session["UserName"] != null&& Session["Img"]!=null && Session["Specallity"]!=null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        return RedirectToAction("Login", "Admin");

                }
                else
                    return View();
            }
            else
                return View();


        }


        public ActionResult LogOut()
        {
            Session["FullName"] = null;
            Session["UserName"] = null;
            Session["Img"] = null;
            Session["Specallity"] = null;
            return RedirectToAction("Login","Admin");
        }
        
        public ActionResult Pro()
        {
            var idpro = Session["Id"];
            return RedirectToAction("Details", "Staff", new { @id = idpro });
        }

        public ActionResult TeacherPro()
        {
            var idpro = Session["Id"];
            return RedirectToAction("Details", "Teacher", new { @id = idpro });
        }

   
        //Teacher Login
        public ActionResult TeacherLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TeacherLogin(LogInM m)
        {
            if (m.UseName == null || m.Password == null)
            {
                return View();
            }
            else
            db = new SRMSDbContext();
            var teacher = db.Staff_Teacher.SingleOrDefault(x=>x.UserName==m.UseName&&x.Password==m.Password);

            if (teacher != null)
            {
                if (teacher.Type == "Faculty")
                {
                    Session["Id"] = teacher.Id;
                    Session["FullName"] = teacher.Name;
                    Session["UserName"] = teacher.UserName;
                    Session["Img"] = teacher.ImagePath;
                    Session["Specallity"] = teacher.Spciallity;
                    if (Session["FullName"] != null && Session["UserName"] != null && Session["Img"] != null && Session["Specallity"] != null)
                    {
                        return RedirectToAction("Teacher", "Teacher");
                    }
                    else
                        return RedirectToAction("TeacherLogin", "Admin");
                }
                else
                    return View();

            }
            else
                return View();


        }


        public ActionResult TeacherLogOut()
        {
            Session["FullName"] = null;
            Session["UserName"] = null;
            Session["Img"] = null;
            Session["Specallity"] = null;
            return RedirectToAction("TeacherLogin", "Admin");
        }




        //Student Login
        public ActionResult StudentLogin()
        {
            return View();
        }


        public static string StdRoll;
        public static string StdName;

        [HttpPost]
        public ActionResult StudentLogin(LogInM m)
        {
            if (m.UseName == null || m.Password == null)
            {
                return View();
            }
            else
                db = new SRMSDbContext();
            var student = db.Students.SingleOrDefault(x => x.UserName == m.UseName && x.Password == m.Password);


            if (student != null)
            {
                if (student.Status == "False")
                {
                    return RedirectToAction("StatusFalse", "StudentResult");
                }

                if (student.Status == "True")
                {
                    
                    Session["Id"] = student.ID;
                    Session["FullName"] = student.Name;
                    Session["UserName"] = student.UserName;
                    Session["Img"] = student.Picture;
                    Session["Specallity"] = student.Roll;
                    //Assin for Result View
                    StdRoll = student.Roll;
                    StdName= student.Name;


                    if (Session["FullName"] != null && Session["UserName"] != null && Session["Img"] != null && Session["Specallity"] != null)
                    {
                        return RedirectToAction("Index", "StudentResult");
                    }
                    else
                        return RedirectToAction("StudentLogin", "Admin");
                }
                else
                    return View();

            }
            else
                return View();

        }

        public ActionResult StudentLogOut()
        {
            Session["FullName"] = null;
            Session["UserName"] = null;
            Session["Img"] = null;
            Session["Specallity"] = null;
            return RedirectToAction("StudentLogin", "Admin");
        }



        public ActionResult StudentPro()
        {
            var idpro = Session["Id"];
            return RedirectToAction("Details", "StudentResult", new { @id = idpro });
        }


    }
}