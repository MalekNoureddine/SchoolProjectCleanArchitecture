using CleanArchProject.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Data.Entities
{
    public class Dept_Instructor : GeneralLocalizableEntity
    {// please configure this entity , dont forget to do that....
        public int InsId {  get; set; }
        public int DID {  get; set; }

        public Instructor Instructor { get; set; } = null!;
        public Department Department { get; set; } = null!;

    }
}
