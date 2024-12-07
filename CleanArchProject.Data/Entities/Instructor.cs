using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchProject.Data.Common;

namespace CleanArchProject.Data.Entities
{
    public class Instructor : GeneralLocalizableEntity
    {
        public Instructor()
        {
            SupervisedInstructors = new List<Instructor>();
            Subj_Instructors = new List<Subj_Instructor>();
            Subjects = new List<Subjects>();
            Dept_Instructors = new List<Dept_Instructor>();
            Departments = new List<Department>();
        }

        public int InsId { get; set; }
        public string ENameAr { get; set; }
        public string EName { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }
        public string? Image { get; set; }

        public Department? ManagedDepartment { get; set; }
        public Instructor? Supervisor { get; set; }
        public virtual ICollection<Instructor>? SupervisedInstructors { get; set; }
        public virtual ICollection<Subj_Instructor>? Subj_Instructors { get; set; }
        public virtual ICollection<Department>? Departments { get; set; }
        public virtual ICollection<Subjects>? Subjects { get; set; }
        public virtual ICollection<Dept_Instructor>? Dept_Instructors { get; set; }
    }
}
