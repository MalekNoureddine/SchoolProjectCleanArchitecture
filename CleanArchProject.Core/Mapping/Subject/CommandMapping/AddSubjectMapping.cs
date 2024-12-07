using CleanArchProject.Core.Featurs.Subject.Commands.Modles;
using CleanArchProject.Core.Featurs.Subject.Queries.Responses;
using CleanArchProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Mapping.Subject
{
    public partial class SubjectProfile
    {
        private void AddSubjectMapping()
        {
            CreateMap<AddSubjectCommand, Subjects>()
                   .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.SubjectName))
                   .ForMember(dest => dest.SubjectNameAr, opt => opt.MapFrom(src => src.SubjectArabicName))
                   .ForMember(dest => dest.Period, opt => opt.MapFrom(src => src.Period));
        }
    }
}
