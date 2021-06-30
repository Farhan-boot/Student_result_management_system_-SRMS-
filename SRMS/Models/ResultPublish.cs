using SRMS.Data;
using SRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRMS.Models
{
    public class ResultPublish
    {
        //main
        public List<Cariculam> CariculamList{ get; set; }
        //assin
        public List<AssinCourse> AssinCourseList { get; set; }
        //submit
        public List<MidTermMark> MidTermMarkList { get; set; }
    }
}