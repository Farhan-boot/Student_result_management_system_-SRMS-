using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRMS.Models
{
    public class MyAttendanceM
    {
        public List<int> Id { get; set; }
        public List<string> StdName { get; set; }
        public List<string> Roll { get; set; }
        public List<string> AccountID { get; set; }
        public List<string> Batch { get; set; }
        public List<string> Semester { get; set; }
        public List<string> CourseName { get; set; }
        public List<string> CourseCode { get; set; }
        public List<string> TeacherName { get; set; }
        public List<int> StdAtten { get; set; }
        public List<Nullable<System.DateTime>> Date { get; set; }
        public List<string> TypeOfAtten { get; set; }
        public List<bool> boolAttn { get; set; }

    }
}