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
        private void GetInstructorsListMapping()
        {
            CreateMap<Instructor, GetInstructorsListResponse>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.EName, src.ENameAr)));
        }
    }
}
