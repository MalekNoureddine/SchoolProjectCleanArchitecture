using System.ComponentModel.DataAnnotations;

namespace CleanArchProject.Data.Enums
{
    public enum enInstructorsOrderingEnum
    {
        InsId =0,
        ENameAr,
        EName,    
        Phone ,   
        Email ,
        Address ,
        SupervisorId,
        Salary,
        NumberOfDepartments,
        NumberOfSubjects
        
    }
    public enum enInstructorViewOrderingEnum
    {
        InsId = 0,
        EName,
        Email,
    }
}
