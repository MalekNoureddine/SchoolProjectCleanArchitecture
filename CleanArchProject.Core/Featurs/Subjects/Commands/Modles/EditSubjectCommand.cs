﻿using CleanArchProject.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Subject.Commands.Modles
{
    public class EditSubjectCommand : IRequest<Response<string>>
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string SubjectArabicName { get; set; }
        public int Period { get; set; }
    }
}
