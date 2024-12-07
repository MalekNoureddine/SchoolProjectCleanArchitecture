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
        private void GetSubjectsListMapping()
        {
            CreateMap<Subjects, GetSubjectListResponse>()
                   .ForMember(dest => dest.SubjectID, opt => opt.MapFrom(src => src.SubID))
                   .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.GetLocalized(src.SubjectName, src.SubjectNameAr)))
                   .ForMember(dest => dest.Period, opt => opt.MapFrom(src => src.Period));
        }
    }
}
