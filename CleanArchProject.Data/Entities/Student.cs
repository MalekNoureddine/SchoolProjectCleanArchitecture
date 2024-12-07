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
    public class Student : GeneralLocalizableEntity
    {
        public int StudID { get; set; }

        public string Name { get; set; }

        public string NameAr { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
        public int? DID { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<StudentSubject> Stud_Subjects { get; set; } = new List<StudentSubject>();
    }
}
