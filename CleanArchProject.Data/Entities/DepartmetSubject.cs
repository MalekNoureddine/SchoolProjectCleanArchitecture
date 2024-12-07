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
    public class DepartmetSubject : GeneralLocalizableEntity
    {
        public int DID { get; set; }
        public int SubID { get; set; }

        public virtual Department Department { get; set; }

        public virtual Subjects Subject { get; set; }
    }
}
