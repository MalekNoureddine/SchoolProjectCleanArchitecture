using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace CleanArchProject.Core.Featurs.Students.Queries.Response
{
    public class GetStudentsListPagiatedResponse
    {
        public GetStudentsListPagiatedResponse(int studID, string name, string address, string phone, string? departmenName)
        {
            StudID = studID;
            Name = name;
            Address = address;
            Phone = phone;
            DepartmenName = departmenName;
        }

        public int StudID { get; set; }
        //public string Name { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? DepartmenName { get; set; }
    }
}
