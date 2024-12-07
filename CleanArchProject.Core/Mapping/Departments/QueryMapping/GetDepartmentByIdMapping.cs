using CleanArchProject.Core.Featurs.Departments.Queries.Response;
using CleanArchProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        private void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdResponse>()
            .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.GetLocalized(src.Manager.EName, src.Manager.ENameAr)))
            .ForMember(d => d.DepartmentID, opt => opt.MapFrom(d => d.DID))
            .ForMember(d => d.ManagerId, opt => opt.MapFrom(d => d.InsId))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.GetLocalized(src.DName, src.DNameAr)))
            .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects))
            //.ForMember(dest => dest.StudentList, opt => opt.MapFrom(src => src.Students))
            .ForMember(dest => dest.InstructorList, opt => opt.MapFrom(src => src.Instructors));

            CreateMap<DepartmetSubject, SubjectResponse>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.GetLocalized(src.Subject.SubjectNameAr, src.Subject.SubjectName)));

            //CreateMap<Student, StudentResponse>()
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
            //     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.NameAr, src.Name)));

            CreateMap<Instructor, InstructorResponse>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.ENameAr, src.EName)));

        }
    }
}
