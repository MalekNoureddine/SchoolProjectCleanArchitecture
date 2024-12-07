using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CleanArchProject.Data.Common;

namespace CleanArchProject.Data.Entities
{
    public class StudentSubject : GeneralLocalizableEntity
    {
        
        public int StudID { get; set; }
        public int SubID { get; set; }
        public double?  Degree { get; set; }

        public virtual Student? Student { get; set; }

        public virtual Subjects? Subject { get; set; }

    }
}
