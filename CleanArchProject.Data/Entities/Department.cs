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
    public partial class Department : GeneralLocalizableEntity
    {
        public Department()
        {
            Students = new List<Student>();
            DepartmentSubjects = new List<DepartmetSubject>();
            Instructors = new List<Instructor>();
            Dept_Instructors = new List<Dept_Instructor>();
        }
        public int DID { get; set; }
        public string DName { get; set; }

        public string DNameAr { get; set; }
        public int InsId { get; set; }
        public Instructor Manager { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<DepartmetSubject> DepartmentSubjects { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Dept_Instructor> Dept_Instructors { get; set; }
    }
}
