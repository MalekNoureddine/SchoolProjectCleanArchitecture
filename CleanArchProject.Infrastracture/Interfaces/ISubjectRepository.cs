﻿using CleanArchProject.Data.Entities;
using CleanArchProject.Infrastracture.InfrastractureBases__generics_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture.Interfaces
{
    public interface ISubjectRepository : IGenericRepositoryAsync<Subjects>
    {
        public Task<List<Subjects>> GetSubjectsAsync(bool includeEntities = false);
    }
}
