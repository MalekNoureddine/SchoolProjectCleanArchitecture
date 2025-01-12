using CleanArchProject.Core.Featurs.Instructors.Queries.Response.View;
using CleanArchProject.Data.Entities.Views;
using CleanArchProject.Infrastracture.Repositories.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Mapping.Instructors
{
    public partial class InstructorProfile
    {
        private void InstructorViewQueryResponseMapping()
        {
            CreateMap<InstructorsView, InstructorViewResponse>();
        }
    }
}
