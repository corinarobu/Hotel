﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> Roles { get; set; } = [];
    }
}
