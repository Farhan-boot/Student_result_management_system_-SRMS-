//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SRMS.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attendance
    {
        public int Id { get; set; }
        public string StdName { get; set; }
        public string StdId { get; set; }
        public string StdAcId { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string TeacherName { get; set; }
        public int StdAttendance { get; set; }
        public System.DateTime DateOfAttendance { get; set; }
        public string TypeOfAttendance { get; set; }
    }
}