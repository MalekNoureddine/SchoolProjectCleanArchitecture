using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Mapping.Instructors
{
    public partial class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            GetInstructorsByIdMapping();
            GetInstructorsListMapping();
            EditInstructorMapping();
            AddInstructorMapping();
        }
    }
}
