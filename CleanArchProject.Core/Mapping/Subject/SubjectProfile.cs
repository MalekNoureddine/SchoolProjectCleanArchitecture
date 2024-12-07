using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Mapping.Subject
{
    public partial class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            GetSubjectByIdMapping();
            GetSubjectsListMapping();
            AddSubjectMapping();
            EditSubjectMapping();
        }
    }
}
