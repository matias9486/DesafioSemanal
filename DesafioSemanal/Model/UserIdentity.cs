using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSemanal.Model
{
    public class UserIdentity:IdentityUser
    {
        public bool IsActive { get; set; }
    }
}
