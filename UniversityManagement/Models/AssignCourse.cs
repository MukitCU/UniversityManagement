//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UniversityManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AssignCourse
    {
        public int AssignCourseId { get; set; }
        public int DepartmentId { get; set; }
        public int TeacherId { get; set; }
        public double TakenCredit { get; set; }
        public double RemainingCredit { get; set; }
        public int CourseId { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Department Department { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
