﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityWpf
{
    internal class User
    {
        public string   Username { get; set; }
        public string Password { get; set; }

        public User(string u, string p)
        {
            Username = u;
            Password = p;
        }
    }
}
