using CleanArchProject.Core.Featurs.Instructors.Commands.Models;
using CleanArchProject.Core.Featurs.Instructors.Queries.Response;
using CleanArchProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Mapping.Instructors
{
    public partial class InstructorProfile
    {
        private void AddInstructorMapping()
        {
            CreateMap<AddInstructorCommand, Instructor>()
                    .ForMember(dest => dest.EName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.ENameAr, opt => opt.MapFrom(src => src.NameInArabic));
        }
    }
}
