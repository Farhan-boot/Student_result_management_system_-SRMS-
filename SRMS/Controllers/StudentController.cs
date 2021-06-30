using SRMS.Data;
using SRMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using OfficeOpenXml;
using System.Drawing;

namespace SRMS.Controllers
{
    public class StudentController : Controller
    {
        SRMSDbContext db;

        public StudentController()
        {
            db = new SRMSDbContext();

        }
        // Post: Student
        [HttpPost]
        public ActionResult AddStd(StudentM M)
        {
            //db = new SRMSDbContext();
            ViewBag.Gender = db.Genders.Select(x => x.GenderName);
            if (M.Name == null || M.AccountID == null || M.Batch == null || M.CurrGaredian == null || M.CurrGaredianAddress == null || M.CurrGaredianMobile == null || M.Dept == null || M.DOB == null || M.Email == null || M.FathersMobile == null || M.FathersName == null || M.FathersProfession == null || M.HSCresult == null || M.MothersMobile == null || M.MothersName == null || M.MothersProfession == null || M.Password == null || M.PermanentAddress == null || M.PresentAddress == "" || M.Roll == null || M.Semester == null || M.Sex == null || M.SSCresult == null || M.Status == null || M.StdMobile == null || M.UserName == null)
            {
                ViewBag.ErrorMess = "Plese Enter  the Fileds";
            }
            else
            {

                string fileName = Path.GetFileNameWithoutExtension(M.ImageFile.FileName);
                string fileExtention = Path.GetExtension(M.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + fileExtention;
                M.Picture = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                M.ImageFile.SaveAs(fileName);
                Student obStd = new Student();
                obStd.Name = M.Name;
                obStd.Email = M.Email;
                obStd.Roll = M.Roll;
                obStd.AccountID = M.AccountID;
                obStd.Batch = M.Batch;
                obStd.Semester = M.Semester;
                obStd.Dept = M.Dept;
                obStd.Sex = M.Sex;
                obStd.StdMobile = M.StdMobile;
                obStd.SSCresult = M.SSCresult;
                obStd.HSCresult = M.HSCresult;
                obStd.DOB = Convert.ToDateTime(M.DOB);
                obStd.FathersName = M.FathersName;
                obStd.FathersMobile = M.FathersMobile;
                obStd.MothersName = M.MothersName;
                obStd.MothersMobile = M.MothersMobile;
                obStd.FathersProfession = M.FathersProfession;
                obStd.MothersProfession = M.MothersProfession;
                obStd.Status = M.Status;
                obStd.CurrGaredian = M.CurrGaredian;
                obStd.CurrGaredianMobile = M.CurrGaredianMobile;
                obStd.CurrGaredianAddress = M.CurrGaredianAddress;
                obStd.PresentAddress = M.PresentAddress;
                obStd.ParmanentAddress = M.PresentAddress;
                obStd.UserName = M.UserName;
                obStd.Password = M.Password;
                //Image path Set
                obStd.Picture = M.Picture;
                db.Students.Add(obStd);
                db.SaveChanges();

            }


           return Json(new { success = true, responseText = "Student successfuly Save!" }, JsonRequestBehavior.AllowGet);

           // return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "AddStd", Active()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("StdList", "Student");
        }
        [HttpGet]
        public ActionResult AddStd()
        {

            db = new SRMSDbContext();
            //ViewBag.Gender = db.Genders.Select(x => x.GenderName);

            StudentM stdObj = new StudentM();
            stdObj.GenderList = db.Genders.ToList();
            //Pass the list in view
            return View(stdObj);
        }
        [HttpGet]
        public ActionResult Edit(int id)

        {
            db = new SRMSDbContext();
            var editStd = db.Students.SingleOrDefault(x => x.ID == id);

            return View(editStd);
        }

        [HttpPost]
        public ActionResult Edit(StudentM m, int id)
        {
            db = new SRMSDbContext();
            var editStd = db.Students.SingleOrDefault(x => x.ID == id);

            editStd.Name = m.Name;
            editStd.Email = m.Email;
            editStd.Roll = m.Roll;
            editStd.AccountID = m.AccountID;
            editStd.Batch = m.Batch;
            editStd.Semester = m.Semester;
            editStd.Dept = m.Dept;
            editStd.Sex = m.Sex;
            editStd.StdMobile = m.StdMobile;
            editStd.SSCresult = m.SSCresult;
            editStd.HSCresult = m.HSCresult;
            editStd.DOB = Convert.ToDateTime(m.DOB);
            editStd.FathersName = m.FathersName;
            editStd.FathersMobile = m.FathersMobile;
            editStd.MothersName = m.MothersName;
            editStd.MothersMobile = m.MothersMobile;
            editStd.FathersProfession = m.FathersProfession;
            editStd.MothersProfession = m.MothersProfession;
            editStd.Status = m.Status;
            editStd.CurrGaredian = m.CurrGaredian;
            editStd.CurrGaredianMobile = m.CurrGaredianMobile;
            editStd.CurrGaredianAddress = m.CurrGaredianAddress;
            editStd.PresentAddress = m.PresentAddress;
            // editStd.ParmanentAddress = M.PresentAddress;
            editStd.UserName = m.UserName;
            editStd.Password = m.Password;
            //Image path Set
            if (m.ImageFile != null)
            {
                //Image Save into Project
                string fileName = Path.GetFileNameWithoutExtension(m.ImageFile.FileName);
                string fileExtention = Path.GetExtension(m.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + fileExtention;
                m.Picture = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                m.ImageFile.SaveAs(fileName);
                editStd.Picture = m.Picture;
            }
            db.SaveChanges();

            return View();
        }

        public ActionResult StdList(string searchByAcRoll)
        {

            //var roll = Request["searchByAcRoll"];
            List<SRMS.Data.Student> stdList = null;
            if (string.IsNullOrEmpty(searchByAcRoll))
            {
                stdList = db.Students.OrderBy(x => x.Name).ToList();
            }
            else
            {
                stdList = db.Students.Where(x => x.Roll == searchByAcRoll || x.AccountID == searchByAcRoll).ToList();
            }

            return View(stdList);
        }

      public JsonResult GetStdList()
        {
            db = new SRMSDbContext();
            var stdList = db.Students.OrderBy(x => x.Name).ToList();
            return Json(stdList, JsonRequestBehavior.AllowGet);
        }




        public JsonResult Delete(int id=0)
        {
            db = new SRMSDbContext();
            var del = db.Students.SingleOrDefault(x => x.ID == id);
            db.Students.Remove(del);
            db.SaveChanges();
       


            return Json(new { success = true, responseText = "Student Successfuly Delete!" }, JsonRequestBehavior.AllowGet);

            //return true;
            //return Json(new { success = true, responseText = "Delete successfuly !" }, JsonRequestBehavior.AllowGet);
            // return Json(GetStdList(), JsonRequestBehavior.AllowGet);
            // return RedirectToAction("StdList", "Student");
        }

        public ActionResult Details(int id)
        {
            db = new SRMSDbContext();
            var Detels = db.Students.SingleOrDefault(x => x.ID == id);

            return View(Detels);
        }

        public ActionResult Active()
        {
            var actStd = from actAlis in db.Students where actAlis.Status == "True" select actAlis;
            return View(actStd);
        }

        public ActionResult Deactive()
        {
            var deactStd = from deactAlis in db.Students where deactAlis.Status == "False" select deactAlis;
            return View(deactStd);
        }

        public ActionResult Exal()
        {
            db = new SRMSDbContext();
            var result = db.Students.Where(x => x.Status == "True").ToList();

            if (result.Count() > 0)
            {
                var entityLst = result.ToList();

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"Name\",\"Department\",\"Roll\",\"AccountID\",\"Email\"");
                Response.ClearContent();
                string fileName = "Sample_" + DateTime.UtcNow.ToString("dd-MM-yyyy");
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
                Response.ContentType = "text/csv";
                if (entityLst.Count > 0)
                {
                    foreach (var line in entityLst)
                    {
                        sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
                                                   line.Name,
                                                   line.Dept,
                                                   line.Roll,
                                                   line.AccountID,
                                                   line.Email
                                                   ));
                    }
                }
                Response.Write(sw.ToString());
                Response.End();
            }

            return View();
        }


        public ActionResult PrintDetails(int id)
        {
            db = new SRMSDbContext();
            var printDetels = db.Students.SingleOrDefault(x => x.ID == id);
              
            return View(printDetels);
        }
        public ActionResult DownloadPDF(int id)
        {
            
            return new ActionAsPdf("PrintDetails", new { Id = id })
            {
                
                FileName = "Details.pdf"
            };


           // return RedirectToAction("PrintDetails", "Student", new {id=id}); ;
        }


        public void ExportToExcel()
        {
            List<StudentM> stdlist = db.Students.Select(x => new StudentM
            {
                //about Student Info
                Name = x.Name,
                Roll = x.Roll,
                AccountID = x.AccountID,
                Batch = x.Batch,
                Semester = x.Semester,
                Dept = x.Dept,
                Email = x.Email,
                StdMobile = x.StdMobile,
                DOB = x.DOB.ToString(),
                Sex = x.Sex,
                //Others Info of Student
                SSCresult = x.SSCresult,
                HSCresult = x.HSCresult,
                FathersName = x.FathersName,
                FathersMobile = x.FathersMobile,
                MothersName = x.MothersName,
                MothersMobile = x.MothersMobile,
                FathersProfession = x.FathersProfession,
                MothersProfession = x.MothersProfession,
                CurrGaredian = x.CurrGaredian,
                CurrGaredianMobile = x.CurrGaredianMobile,
                CurrGaredianAddress=x.CurrGaredianAddress,
                Status=x.Status,
                PresentAddress=x.PresentAddress,
                PermanentAddress=x.PresentAddress,
                UserName=x.UserName

            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            //Start Bold Process
            ws.Cells["A1"].Style.Font.Bold = true;
            ws.Cells["B1"].Style.Font.Bold = true;
            ws.Cells["A2"].Style.Font.Bold = true;
            ws.Cells["B2"].Style.Font.Bold = true;
            ws.Cells["A3"].Style.Font.Bold = true;
            ws.Cells["B3"].Style.Font.Bold = true;
            //End Bold Process
            ws.Cells["A1"].Value = "Student Details Info";
            ws.Cells["B1"].Value = "Report";
            ws.Cells["A2"].Value = "Student List";
            ws.Cells["B2"].Value = "About Student Info";
            ws.Cells["A3"].Value = "Download Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            //about Student Info
            ws.Cells["A6"].Value = "Student Name";
            ws.Cells["A6"].Style.Font.Bold = true;
            ws.Cells["B6"].Value = "Roll";
            ws.Cells["B6"].Style.Font.Bold = true;
            ws.Cells["C6"].Value = "Account ID";
            ws.Cells["C6"].Style.Font.Bold = true;
            ws.Cells["D6"].Value = "Batch";
            ws.Cells["D6"].Style.Font.Bold = true;
            ws.Cells["E6"].Value = "Semester";
            ws.Cells["E6"].Style.Font.Bold = true;
            ws.Cells["F6"].Value = "Department";
            ws.Cells["F6"].Style.Font.Bold = true;
            ws.Cells["G6"].Value = "Email";
            ws.Cells["G6"].Style.Font.Bold = true;
            ws.Cells["H6"].Value = "Student Mobile";
            ws.Cells["H6"].Style.Font.Bold = true;
            ws.Cells["I6"].Value = "Date Of Birth";
            ws.Cells["I6"].Style.Font.Bold = true;
            ws.Cells["J6"].Value = "Sex";
            ws.Cells["J6"].Style.Font.Bold = true;
            //Others Info of Student
            ws.Cells["K6"].Value = "SSC Result";
            ws.Cells["K6"].Style.Font.Bold = true;
            ws.Cells["L6"].Value = "HSC Result";
            ws.Cells["L6"].Style.Font.Bold = true;
            ws.Cells["M6"].Value = "Fathers Name";
            ws.Cells["M6"].Style.Font.Bold = true;
            ws.Cells["N6"].Value = "Fathers Mobile";
            ws.Cells["N6"].Style.Font.Bold = true;
            ws.Cells["O6"].Value = "Mothers Name";
            ws.Cells["O6"].Style.Font.Bold = true;
            ws.Cells["P6"].Value = "Mothers Mobile";
            ws.Cells["P6"].Style.Font.Bold = true;
            ws.Cells["Q6"].Value = "Fathers Profession";
            ws.Cells["Q6"].Style.Font.Bold = true;
            ws.Cells["R6"].Value = "Mothers Profession";
            ws.Cells["R6"].Style.Font.Bold = true;
            ws.Cells["S6"].Value = "Current Garedian";
            ws.Cells["S6"].Style.Font.Bold = true;
            ws.Cells["T6"].Value = "Current Garedian Mobile";
            ws.Cells["T6"].Style.Font.Bold = true;
            ws.Cells["U6"].Value = "Current Garedian Address";
            ws.Cells["U6"].Style.Font.Bold = true;
            ws.Cells["V6"].Value = "Status";
            ws.Cells["V6"].Style.Font.Bold = true;
            ws.Cells["W6"].Value = "Present Address";
            ws.Cells["W6"].Style.Font.Bold = true;
            ws.Cells["X6"].Value = "Parmanent Address";
            ws.Cells["X6"].Style.Font.Bold = true;
            ws.Cells["Y6"].Value = "User Name";
            ws.Cells["Y6"].Style.Font.Bold = true;

            int rowStart = 7;
            foreach (var item in stdlist)
            {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                //about Student Info
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Name;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Roll;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.AccountID;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Batch;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Semester;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Dept;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.StdMobile;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.DOB;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.Sex;
                //Others Info of Student
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.SSCresult;
                ws.Cells[string.Format("L{0}", rowStart)].Value = item.HSCresult;
                ws.Cells[string.Format("M{0}", rowStart)].Value = item.FathersName;
                ws.Cells[string.Format("N{0}", rowStart)].Value = item.FathersMobile;
                ws.Cells[string.Format("O{0}", rowStart)].Value = item.MothersName;
                ws.Cells[string.Format("P{0}", rowStart)].Value = item.MothersMobile;
                ws.Cells[string.Format("Q{0}", rowStart)].Value = item.FathersProfession;
                ws.Cells[string.Format("R{0}", rowStart)].Value = item.MothersProfession;
                ws.Cells[string.Format("S{0}", rowStart)].Value = item.CurrGaredian;
                ws.Cells[string.Format("T{0}", rowStart)].Value = item.CurrGaredianMobile;
                ws.Cells[string.Format("U{0}", rowStart)].Value = item.CurrGaredianAddress;
                ws.Cells[string.Format("V{0}", rowStart)].Value = item.Status;
                ws.Cells[string.Format("W{0}", rowStart)].Value = item.PresentAddress;
                ws.Cells[string.Format("X{0}", rowStart)].Value = item.PermanentAddress;
                ws.Cells[string.Format("Y{0}", rowStart)].Value = item.UserName;
 
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();    
        }












    }
}