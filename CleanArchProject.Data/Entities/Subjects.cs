using CleanArchProject.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchProject.Data.Entities
{

    public class Subjects : GeneralLocalizableEntity
    {
        public Subjects()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
            DepartmetSubjects = new HashSet<DepartmetSubject>();
            Instructors = new HashSet<Instructor>();
            Subj_Instructors = new HashSet<Subj_Instructor>();
        }
        [Key]
        public int SubID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        public string SubjectNameAr { get; set; }
        public int Period { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
        public virtual ICollection<Subj_Instructor> Subj_Instructors { get; set; }
        public virtual ICollection<DepartmetSubject> DepartmetSubjects { get; set; }
    }
}
