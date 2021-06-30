using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRMS.BusinessLayer
{
    public class Point
    {
        public double Gp { get; set; }
        public string Lg { get; set; }
    }
    public class GradePolicy
    {

        public static List<Point> GreadPoint(double mark)
        {
            List<Point> point = new List<Point>();
            Point objPoint = new Point();
            if (mark <= 100 && mark >= 0)
            {

                if (100 <= mark || 80 <= mark)
                {
                    objPoint.Gp = 4.00;
                    objPoint.Lg = "A+";
                    point.Add(objPoint);
                }
                else if (79 <= mark || 75 <= mark)
                {
                    objPoint.Gp = 3.75;
                    objPoint.Lg = "A";
                    point.Add(objPoint);
                }
                else if (74 <= mark || 70 <= mark)
                {
                    objPoint.Gp = 3.50;
                    objPoint.Lg = "A-";
                    point.Add(objPoint);
                }
                else if (69 <= mark || 65 <= mark)
                {
                    objPoint.Gp = 3.25;
                    objPoint.Lg = "B+";
                    point.Add(objPoint);
                }
                else if (64 <= mark || 60 <= mark)
                {
                    objPoint.Gp = 3.00;
                    objPoint.Lg = "B";
                    point.Add(objPoint);
                }
                else if (59 <= mark || 55 <= mark)
                {
                    objPoint.Gp = 2.75;
                    objPoint.Lg = "B-";
                    point.Add(objPoint);
                }
                else if (55 <= mark || 50 <= mark)
                {
                    objPoint.Gp = 2.50;
                    objPoint.Lg = "C+";
                    point.Add(objPoint);
                }
                else if (49 <= mark || 45 <= mark)
                {
                    objPoint.Gp = 2.25;
                    objPoint.Lg = "C";
                    point.Add(objPoint);
                }
                else if (40 <= mark || 44 <= mark)
                {
                    objPoint.Gp = 2.00;
                    objPoint.Lg = "D";
                    point.Add(objPoint);
                }
                else
                {
                    objPoint.Gp = 0.00;
                    objPoint.Lg = "F";
                    point.Add(objPoint);
                }
            }
            return point;
        }

    }
  
}