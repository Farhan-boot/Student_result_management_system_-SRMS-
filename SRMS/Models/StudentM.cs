using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using SRMS.Data;
using System.ComponentModel.DataAnnotations;

namespace SRMS.Models
{
    public class StudentM
    {
        public int Id { get; set; }
        [DisplayName("Student Name")]
        [Required(ErrorMessage ="This Name Fild Is Required !")]
        public string Name { get; set; }
        [DisplayName("Email Account")]
        [Required(ErrorMessage = "This Email Fild Is Required !")]
        public string Email { get; set; }
        [DisplayName("Roll Number")]
        [Required(ErrorMessage = "This Roll Fild Is Required !")]
        public string Roll { get; set; }
        [DisplayName("Account Number")]
        [Required(ErrorMessage = "This Account ID Fild Is Required !")]
        public string AccountID { get; set; }
        [DisplayName("Student Batch")]
        [Required(ErrorMessage = "This Student Batch Fild Is Required !")]
        public string Batch { get; set; }
        [DisplayName("Student Semester")]
        [Required(ErrorMessage = "This Student Semester Fild Is Required !")]
        public string Semester { get; set; }
        [DisplayName("Department of Student")]
        [Required(ErrorMessage = "This Department of Student Fild Is Required !")]
        public string Dept { get; set; }
        [DisplayName("Gender of Student")]
        [Required(ErrorMessage = "This Gender of Student Fild Is Required !")]
        public string Sex { get; set; }
        [DisplayName("Student Mobile Number")]
        [Required(ErrorMessage = "This Student Mobile Number Fild Is Required !")]
        public string StdMobile { get; set; }
        [DisplayName("SSC Result Of Student")]
        [Required(ErrorMessage = "This SSC Result Of Student Fild Is Required !")]
        public string SSCresult { get; set; }
        [DisplayName("HSC Result Of Student")]
        [Required(ErrorMessage = "This HSC Result Of Student Fild Is Required !")]
        public string HSCresult { get; set; }
        [DisplayName("Date Of Birth Student")]
        [Required(ErrorMessage = "This Date Of Birth Student Fild Is Required !")]
        public string DOB { get; set; }
        [DisplayName("Picture Of Student")]
        [Required(ErrorMessage = "This Picture Of Student Fild Is Required !")]
        public string Picture { get; set; }
        [DisplayName("Fathers Name")]
        [Required(ErrorMessage = "This Fathers Name Fild Is Required !")]
        public string FathersName { get; set; }
        [DisplayName("Fathers Mobile Number")]
        [Required(ErrorMessage = "This Fathers Mobile Number Fild Is Required !")]
        public string FathersMobile { get; set; }
        [DisplayName("Mothers Name")]
        [Required(ErrorMessage = "This Mothers Name Fild Is Required !")]
        public string MothersName { get; set; }
        [DisplayName("Mothers Mobile Number")]
        [Required(ErrorMessage = "This Mothers Mobile Number Fild Is Required !")]
        public string MothersMobile { get; set; }
        [DisplayName("Fathers Profession")]
        [Required(ErrorMessage = "This Fathers Profession Fild Is Required !")]
        public string FathersProfession { get; set; }
        [DisplayName("Mothers Profession")]
        [Required(ErrorMessage = "This Mothers Profession Fild Is Required !")]
        public string MothersProfession { get; set; }
        [DisplayName("Student Status")]
        [Required(ErrorMessage = "This Student Status Fild Is Required !")]
        public string Status { get; set; }
        [DisplayName("Current Garedian")]
        [Required(ErrorMessage = "This Current Garedian Fild Is Required !")]
        public string CurrGaredian { get; set; }
        [DisplayName("Current Garedian Mobile Number")]
        [Required(ErrorMessage = "This Current Garedian Mobile Number Fild Is Required !")]
        public string CurrGaredianMobile { get; set; }
        [DisplayName("Current Garedian Address")]
        [Required(ErrorMessage = "This Current Garedian Address Fild Is Required !")]
        public string CurrGaredianAddress { get; set; }
        [DisplayName("Present Address")]
        [Required(ErrorMessage = "This Present Address Fild Is Required !")]
        public string PresentAddress { get; set; }
        [DisplayName("Permanent Address")]
        [Required(ErrorMessage = "This Permanent Address Fild Is Required !")]
        public string PermanentAddress { get; set; }
        [DisplayName("User Name")]
        [Required(ErrorMessage = "This User Name Fild Is Required !")]
        public string UserName { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "This Password Fild Is Required !")]
        public string Password { get; set; }
        //for image
        public HttpPostedFileBase ImageFile { get; set; }
        public List<Gender> GenderList { get; set; }

    }

}