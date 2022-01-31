using DesafioSemanal.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSemanal.Context
{
    public class UserContext : IdentityDbContext<UserIdentity>
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }
    }
}
