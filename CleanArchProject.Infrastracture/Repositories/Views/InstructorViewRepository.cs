using CleanArchProject.Data.Entities.Views;
using CleanArchProject.Infrastracture.Data;
using CleanArchProject.Infrastracture.InfrastractureBases__generics_;
using CleanArchProject.Infrastracture.Interfaces.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture.Repositories.Views
{
    public class InstructorViewRepository : GenericRepositoryAsync<InstructorsView>, IViewRepository<InstructorsView>
    {
        #region Fields
        private DbSet<InstructorsView> _instructorsView;
        #endregion

        #region Constructors
        public InstructorViewRepository(AppDbContext dbContext) : base(dbContext)
        {
            _instructorsView = dbContext.Set<InstructorsView>();
        }
        # endregion
        #region Hanle functions
        //Handle functions
        #endregion
    }
}
