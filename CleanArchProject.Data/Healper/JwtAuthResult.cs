﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Data.Healper
{
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string TokenString { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}