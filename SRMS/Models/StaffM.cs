using SRMS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRMS.Models
{
    public class StaffM
    {
        [DisplayName("Staff Name")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Date Of Birth")]
        [Required]
        public Nullable<System.DateTime> DOB { get; set; }
        [DisplayName("Gender")]
        [Required]
        public string Sex { get; set; }
        [DisplayName("Mobile Number")]
        [Required]
        public string Mobile { get; set; }
        [DisplayName("Address")]
        [Required]
        public string Address { get; set; }
        [DisplayName("Year Of The Service")]
        [Required]
        public Nullable<System.DateTime> YearOfTheService { get; set; }
        [DisplayName("Staff Degicnetion")]
        [Required]
        public string Degicnetion { get; set; }
        [DisplayName("Staff Spciallity")]
        [Required]
        public string Spciallity { get; set; }
        [DisplayName("Salary")]
        [Required]
        public Nullable<double> Salary { get; set; }
        [DisplayName("Father Name")]
        [Required]
        public string FatherName { get; set; }
        [DisplayName("Mother Name")]
        [Required]
        public string MotherName { get; set; }
        [DisplayName("Fathers Number")]
        [Required]
        public string FathersNumber { get; set; }
        [DisplayName("Mothers Number")]
        [Required]
        public string MothersNumber { get; set; }
        [DisplayName("Permanent Address")]
        [Required]
        public string PermanentAddress { get; set; }
        [DisplayName("Type")]
        [Required]
        public string Type { get; set; }
        [DisplayName("Image")]
        [Required]
        public string ImagePath { get; set; }
        [DisplayName("User Name")]
        [Required]
        public string UserName { get; set; }
        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        //for Image
        public HttpPostedFileBase ImageFile { get; set; }


    }
}