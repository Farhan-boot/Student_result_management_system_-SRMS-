using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRMS.Models
{
    public class AttendanceM
    {
        public int Id { get; set; }
        public string StdName { get; set; }
        public string Roll { get; set; }
        public string AccountID { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string TeacherName { get; set; }
        public int StdAtten { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string TypeOfAtten { get; set; }
        public bool boolAttn { get; set; }

    }

 

}