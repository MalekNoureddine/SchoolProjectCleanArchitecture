using CleanArchProject.Core.Featurs.Departments.Queries.Response;
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
        private void GetInstructorsByIdMapping()
        {
            CreateMap<Instructor, GetInstructorByIdResponse>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.EName, src.ENameAr)))
                    //.ForMember(dest => dest.Superviseds, opt => opt.MapFrom(src => src.SupervisedInstructors))
                    .ForMember(dest => dest.supervisor, opt => opt.MapFrom(src => src.Supervisor));

            CreateMap<Instructor, Supervisor>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
                 .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.EName, src.ENameAr)));
            
            //CreateMap<Instructor, Supervised>()
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.EName, src.ENameAr)));
        }
    }
}
